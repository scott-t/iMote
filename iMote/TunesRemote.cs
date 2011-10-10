/*
 * iMote
 * Copyright (C) 2010-2011 Scott Thomas <scott_t@users.sourceforge.net>
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace iMote
{
  enum MsgType
  {
    MSG_VOTE,
    MSG_STATUS,
    MSG_GETVOL,
    MSG_SETVOL,
    MSG_PLAYPAUSE,
    MSG_SKIP_NEXT,
    MSG_SKIP_PREV,
    MSG_GET_ART,
    MSG_GET_PLAYLIST,
  }

  class TunesMsg
  {
    public MsgType ReqType;
    public Dictionary<string, object> data = new Dictionary<string,object>();

    public TunesMsg(MsgType t)
    {
      ReqType = t;
    }

    public object this [string v]
    {
      get{ return data[v]; }
      set{ data[v] = value;}
    }
      
  }

  class Playlist
  {
    public string name;
    public int id;
    public override string ToString()
    {
      return name;
    }
  }

  class TunesRemote
  {
    Queue<TunesMsg> msgQueue = new Queue<TunesMsg>();
    MainForm parent;
    System.Threading.Thread iTunesThread;

    const string URL = "http://192.168.4.69:3689/";

    DAAP.ContentCodeBag bag;

    int SessID = 0;
    int dbID = 0;
    int musicDB = 0;
    int djDB = 0;

    int revId = 1;

    public int DJID { get { return djDB; } }// djDB; } }   //  6430 = not recently played smart list
    public int MusicID { get { return musicDB; } }

    public List<Playlist> playlists = new List<Playlist>();
    
    public TunesRemote(MainForm parentForm) 
    {
      parent = parentForm;
      iTunesThread = new System.Threading.Thread(new System.Threading.ThreadStart(commsThread));
      iTunesThread.IsBackground = true;
      iTunesThread.Start();
    }

    ~TunesRemote()
    {
      iTunesThread.Abort();
      GetDAAP("logout?session-id=" + SessID.ToString());
    }

    private void commsThread()
    {
      try
      {
        while (true)
        {
          while (msgQueue.Count == 0)
            System.Threading.Thread.Sleep(250);

          // :)
          TunesMsg msg;
          lock (msgQueue)
          {
            msg = msgQueue.Dequeue();
          }

          System.Diagnostics.Debug.WriteLine("comms request: " + System.Enum.GetName(msg.ReqType.GetType(), msg.ReqType));

          switch (msg.ReqType)
          {
            case MsgType.MSG_GET_PLAYLIST:
              msg["plist"] = threadGetPlaylist(msg);
              break;

            case MsgType.MSG_GETVOL:
              msg["vol"] = threadGetVolume();
              break;

            case MsgType.MSG_SETVOL:
              threadSetVolume(msg);
              break;

            case MsgType.MSG_SKIP_NEXT:
              threadSkipNext();
              break;

            case MsgType.MSG_SKIP_PREV:
              threadSkipPrev();
              break;

            case MsgType.MSG_STATUS:
              msg["status"] = threadStatus();
              break;

            case MsgType.MSG_VOTE:
              threadVoteSong(msg);
              break;

            case MsgType.MSG_PLAYPAUSE:
              threadPlayPause();
              break;

            default:
              System.Diagnostics.Debug.WriteLine("Unknown request type: " + System.Enum.GetName(msg.ReqType.GetType(), msg.ReqType));
              break;
          }
          parent.TunesHandleEvent(msg);
        }
      }
      catch (System.Threading.ThreadAbortException ex)
      {
        // Thread ending
      }
    }
    
    public void Login()
    {
      PrepBag();

      string url = "login?pairing-guid=0x";
      byte[] codez = Convert.FromBase64String(Properties.Settings.Default.GUID);
      for (int i = 0; i < 8; ++i)
        url += codez[i].ToString("x2");
      
      DAAP.ContentNode node = GetDAAP(url);

      SessID = (int)node.GetChild("dmap.sessionid").Value;

      node = GetDAAP("databases");
      dbID = (int)node.GetChild("dmap.itemid").Value;

      node = GetDAAP("databases/" + dbID + "/containers");
      node = node.GetChild("dmap.listing");
      foreach (DAAP.ContentNode n in (DAAP.ContentNode[])node.Value)
      {
        string name = (string)n.GetChild("dmap.itemname").Value;
        int id = (int)n.GetChild("dmap.itemid").Value;
        Playlist l = new Playlist();
        l.name = name;
        l.id = id;
        playlists.Add(l);

        name = name.ToLower();

        if (name == "music")
          musicDB = id;
        else if (name == "itunes dj")
          djDB = id;
      }     
    }

    public void VoteSong(ulong pid, int id)
    {//&database-spec='dmap.persistentid=" + pid + "'&
      TunesMsg data = new TunesMsg(MsgType.MSG_VOTE);
      data["pid"] = pid;
      data["id"] = id;
      msgQueue.Enqueue(data);
      
      
      //DAAP.ContentNode n = GetDAAP("ctrl-int/1/setproperty?com.apple.itunes.jukebox-vote=1&item-spec='dmap.itemid:" + id + "'"); //cue?command=add&query='dmap.itemid:" + id + "'");
    }

    private void threadVoteSong(TunesMsg data)
    {
      GetDAAP("ctrl-int/1/setproperty?com.apple.itunes.jukebox-vote=1&item-spec='dmap.itemid:" + data["id"] + "'"); //cue?command=add&query='dmap.itemid:" + id + "'");
    }

    public void GetPlaylist(int plId)
    {
      TunesMsg data = new TunesMsg(MsgType.MSG_GET_PLAYLIST);
      data["plId"] = plId;
      msgQueue.Enqueue(data);

      //return Song.parsePlaylist(GetDAAP("databases/" + dbID + "/containers/" + plId + "/items?revision-id=1&type=music&meta=dmap.itemid,dmap.persistentid,dmap.itemname,daap.songalbum,daap.songartist,daap.songtime"));
    }

    private List<Song> threadGetPlaylist(TunesMsg data)
    {
      return Song.parsePlaylist(GetDAAP("databases/" + dbID + "/containers/" + data["plId"] + "/items?revision-id=" + revId + "&type=music&meta=dmap.itemid,dmap.persistentid,dmap.itemname,daap.songalbum,daap.songartist,daap.songtime"));
    }

    public void Status()
    {
      TunesMsg data = new TunesMsg(MsgType.MSG_STATUS);
      lock (msgQueue)
      {
        msgQueue.Enqueue(data);
      }

      //DAAP.ContentNode node = GetDAAP("ctrl-int/1/playstatusupdate?revision-number=" + revId);
      //return null;
    }

    private DAAP.ContentNode threadStatus()
    {
      return GetDAAP("ctrl-int/1/playstatusupdate?revision-number=" + revId);
    }

    public DAAP.ContentNode PlayPause()
    {
      lock (msgQueue)
      {
        msgQueue.Enqueue(new TunesMsg(MsgType.MSG_PLAYPAUSE));
      }
      return null;// GetDAAP("ctrl-int/1/playpause");
    }

    private void threadPlayPause()
    {
      GetDAAP("ctrl-int/1/playpause");
    }

    public void SkipNext()
    {
      lock (msgQueue)
      {
        msgQueue.Enqueue(new TunesMsg(MsgType.MSG_SKIP_NEXT));
      }
      //return null; // 
    }

    private void threadSkipNext()
    {
      GetDAAP("ctrl-int/1/nextitem");
    }

    public DAAP.ContentNode SkipPrev()
    {
      lock (msgQueue)
      {
        msgQueue.Enqueue(new TunesMsg(MsgType.MSG_SKIP_PREV));
      }
      return null;// GetDAAP("ctrl-int/1/previtem");
    }

    private void threadSkipPrev()
    {
      GetDAAP("ctrl-int/1/previtem");
    }

    public int GetVolume()
    {
      lock (msgQueue)
      {
        msgQueue.Enqueue(new TunesMsg(MsgType.MSG_GETVOL));
      }
      
      return 0;//
    }

    private int threadGetVolume()
    {
      DAAP.ContentNode node = GetDAAP("ctrl-int/1/getproperty?properties=dmcp.volume");
      return (int)node.GetChild("tune.volume").Value;
    }

    public bool SetVolume(int volPercent)
    {
      TunesMsg data = new TunesMsg(MsgType.MSG_SETVOL);
      data["vol"] = volPercent;
      lock (msgQueue)
      {
        msgQueue.Enqueue(data);
      }
      //
      return true;
    }

    private void threadSetVolume(TunesMsg data)
    {
      GetDAAP("ctrl-int/1/setproperty?dmcp.volume=" + data["vol"] + ".000000");
    }

    public System.IO.Stream GetItemArt(int item, int w, int h)
    {
      TunesMsg data = new TunesMsg(MsgType.MSG_GET_ART);
      data["item"] = item;
      data["w"] = w;
      data["h"] = h;
      lock (msgQueue)
      {
        msgQueue.Enqueue(data);
      }
      /*
      try
      {
        int cnt = 0;
        while (true)
        {
          cnt++;
          System.Net.WebRequest req = System.Net.HttpWebRequest.Create(URL + "databases/" + dbID + "/items/" + item + "/extra_data/artwork?mw=" + w + "&mh=" + h + "&revision-number=" + revId + "&session-id=" + SessID);
          req.Headers.Add("Viewer-Only-Client", "1");
          req.Headers.Add("Accept-Encoding", "gzip, deflate");

          System.Net.WebResponse resp = req.GetResponse();
          System.IO.Stream st;
          if (resp.Headers["Content-encoding"] == "gzip")
            st = new System.IO.Compression.GZipStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
          else if (resp.Headers["Content-encoding"] == "deflate")
            st = new System.IO.Compression.DeflateStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
          else
            st = resp.GetResponseStream();

          byte[] b = new byte[resp.ContentLength];
          int len = st.Read(b, 0, b.Length);
          Array.Resize<byte>(ref b, len);

          System.IO.MemoryStream stream = new System.IO.MemoryStream();
          stream.Write(b, 0, b.Length);

          if (len > 0 && len < 50)
          {
            if (cnt > 2)
              return null;

            DAAP.ContentNode n = DAAP.ContentParser.Parse(bag, b);

            revId = (int)n.GetChild("dmap.serverrevision").Value;
            continue;
          }

          if (b.Length > 0)
            return stream;
          else
            return null;
        }
      }
      catch (Exception e)
      {*/
        return null;
      //}
    }

    public System.IO.Stream GetArt(int w, int h)
    {
      TunesMsg data = new TunesMsg(MsgType.MSG_GET_ART);
      data["w"] = w;
      data["h"] = h;
      lock (msgQueue)
      {
        msgQueue.Enqueue(data);
      }
      /*
      try
      {
        System.Net.WebRequest req = System.Net.HttpWebRequest.Create(URL + "ctrl-int/1/nowplayingartwork?mw=" + w + "&mh=" + h + "&session-id=" + SessID);
        req.Headers.Add("Viewer-Only-Client", "1");
        req.Headers.Add("Accept-Encoding", "gzip, deflate");

        System.Net.WebResponse resp = req.GetResponse();
        System.IO.Stream st;
        if (resp.Headers["Content-encoding"] == "gzip")
          st = new System.IO.Compression.GZipStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
        else if (resp.Headers["Content-encoding"] == "deflate")
          st = new System.IO.Compression.DeflateStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
        else
          st = resp.GetResponseStream();

        byte[] b = new byte[resp.ContentLength];
        int len = st.Read(b, 0, b.Length);
        Array.Resize<byte>(ref b, len);

        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        stream.Write(b, 0, b.Length);

        if (b.Length > 0)
          return stream;
        else
          return null;
      }
      catch (Exception e)
      {*/
        return null;
      //}
    }

    private DAAP.ContentNode GetDAAP(string url)
    {
      return GetDAAP(url, null);
    }

    private DAAP.ContentNode GetDAAP(string url, string root)
    {
  
      System.Net.WebRequest req;
      if (SessID > 0)
      {
        if (url.Contains("?"))
          req = System.Net.HttpWebRequest.Create(URL + url + "&session-id=" + SessID.ToString());
        else
          req = System.Net.HttpWebRequest.Create(URL + url + "?session-id=" + SessID.ToString());
      }
      else
      {
        req = System.Net.HttpWebRequest.Create(URL + url);
      }

      req.Headers.Add("Viewer-Only-Client", "1");
      req.Headers.Add("Accept-Encoding", "gzip, deflate");

      System.Net.WebResponse resp = req.GetResponse();
      System.IO.Stream st;
      if (resp.Headers["Content-encoding"] == "gzip")
        st = new System.IO.Compression.GZipStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
      else if (resp.Headers["Content-encoding"] == "deflate")
        st = new System.IO.Compression.DeflateStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
      else
        st = resp.GetResponseStream();

      byte[] b = new byte[65535 * 1024];
      int len = st.Read(b, 0, b.Length);
      Array.Resize<byte>(ref b, len);
      if (len > 4)
        return DAAP.ContentParser.Parse(bag, b, root);
      else
        return null;
    }

    private void PrepBag()
    {
      System.Net.WebRequest req = System.Net.HttpWebRequest.Create(URL + "content-codes");
      req.Headers.Add("Viewer-Only-Client", "1");
      req.Headers.Add("Accept-Encoding", "gzip, deflate");

      System.Net.WebResponse resp = req.GetResponse();
      System.IO.Stream st;
      if (resp.Headers["Content-encoding"] == "gzip")
        st = new System.IO.Compression.GZipStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
      else if (resp.Headers["Content-encoding"] == "deflate")
        st = new System.IO.Compression.DeflateStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
      else
        st = resp.GetResponseStream();

      byte[] b = new byte[65535*1024];
      int len = st.Read(b, 0, b.Length);
      Array.Resize<byte>(ref b, len);
      bag = DAAP.ContentCodeBag.ParseCodes(b);
      
    }

    #region Pairing
    public static byte[] PAIRING_RAW = new byte[] { 0x63, 0x6d, 0x70, 0x61, 0x00, 0x00, 0x00, 0x3a, 0x63, 0x6d, 0x70,
            0x67, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x63, 0x6d, 0x6e, 0x6d, 0x00,
            0x00, 0x00, 0x16, 0x41, 0x64, 0x6d, 0x69, 0x6e, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x6f, 0x72,
            (byte) 0xe2, (byte) 0x80, (byte) 0x99, 0x73, 0x20, 0x69, 0x50, 0x6f, 0x64, 0x63, 0x6d, 0x74, 0x79, 0x00,
            0x00, 0x00, 0x04, 0x69, 0x50, 0x6f, 0x64 };


    public bool DoPair()
    {
      if (Properties.Settings.Default.GUID.Length >= 8)
      {
        return true;
        // already paired, and have a GUID floating around
      }

  
      // Create service to advertise our control abilities
      Random rdmPort = new Random();
      int portNum = rdmPort.Next(50000, 51000);

      ZeroconfService.NetService sv = new ZeroconfService.NetService("", "_touch-remote._tcp", Environment.MachineName + "TunesRemote", portNum);
      sv.AllowMultithreadedCallbacks = true;

      Dictionary<string, string> d = new Dictionary<string, string>();
      d["DvNm"] = Environment.MachineName + "-iRemote";
      d["RemV"] = "10000";
      d["DvTy"] = "iPod";
      d["RemN"] = "Remote";
      d["txtvers"] = "1";

      byte[] codez = new byte[8];
      Random rdm = new Random();
      rdm.NextBytes(codez);
      string pair = "";
      for (int i = 0; i < 8; ++i)
        pair += codez[i].ToString("x2");

      d["Pair"] = pair;

      sv.TXTRecordData = Dict2TXT(d);
      sv.Publish(); // Imma control me an iTunes

      System.Net.Sockets.TcpListener server = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, portNum);
      server.Start();
      while (true)
      {
        System.Windows.Forms.MessageBox.Show("Hit ok, and then enter pincode (0000) into iTunes");

        while (!server.Pending()) 
          System.Threading.Thread.Sleep(100);

        System.Net.Sockets.Socket client = server.AcceptSocket();
        // They sending us something, but who cares - it should be a pairing request unless they've changed the spec again

        byte[] code = new byte[8];

        Random r = new Random();
        r.NextBytes(code);

        Array.Copy(code, 0, PAIRING_RAW, 16, 8);

        //string niceCode = toHex(code);

        byte[] header = System.Text.Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-Length: " + PAIRING_RAW.Length.ToString() + "\r\n\r\n");
        byte[] reply = new byte[header.Length + PAIRING_RAW.Length];

        Array.Copy(header, 0, reply, 0, header.Length);
        Array.Copy(PAIRING_RAW, 0, reply, header.Length, PAIRING_RAW.Length);

        client.Send(reply);

        // paired?
        Properties.Settings.Default.GUID = Convert.ToBase64String(code);
        Properties.Settings.Default.Save();

        client.Close();
        server.Server.Close();
        break;

      }
      return true;
    }

    private Dictionary<string, string> TXT2Dict(byte[] txt)
    {
      string tmp = System.Text.Encoding.ASCII.GetString(txt);
      Dictionary<string, string> dict = new Dictionary<string, string>();
      int i = 0;
      int len = 0;
      for (i = 0; i < txt.Length;)
      {
        len = txt[i++];
        string key = "", val = "";

        int sep = 0;
        for (int j = 0; j < len; ++j)
        {
          if (txt[j + i] == (byte)'=')
          {
            sep = j;
            break;
          }
        }
        if (sep == 0)
          throw new Exception("Noooo.  Couldn't decode TXT data :(");

        key = System.Text.Encoding.ASCII.GetString(txt, i, sep);
        val = System.Text.Encoding.ASCII.GetString(txt, i + sep + 1, len - sep - 1);
        i += len;
        dict.Add(key, val);
      }
      return dict;
    }

    private byte[] Dict2TXT(Dictionary<string, string> dict)
    {
      byte[] b = new byte[65535];
      int i = 0;
      foreach (string key in dict.Keys)
      {
        int len = key.Length + dict[key].Length + 1;
        b[i] = (byte)len;
        i++;
        foreach (byte c in key)
          b[i++] = c;
        b[i++] = (byte)'=';
        foreach (byte c in dict[key])
          b[i++] = c;
      }
      System.Array.Resize<byte>(ref b, i);
      return b;
    }

    #endregion
  }
}
