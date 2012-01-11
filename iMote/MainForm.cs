using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iMote
{
  public partial class MainForm : Form
  {
    System.Threading.Mutex mutex = new System.Threading.Mutex();

    System.Threading.Mutex playlistMutex = new System.Threading.Mutex();

    Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton btnPrevThumb, btnNextThumb, btnPlayThumb;
    
    Icon pauseIco, playIco;

    List<Song> playlist = new List<Song>();
    List<Song> musicDB = null;
    Song currSong = null;
    Song nextSong = null;
    Song nextNext = null;

    bool firstSongList = true;

    TunesRemote tunes;

    public MainForm()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      tunes = new TunesRemote(this);
      tunes.DoPair();
      tunes.Login();

      List<Playlist> lists = tunes.playlists;
      foreach (Playlist list in lists)
      {
        cmbPlaylist.Items.Add(list);
        if (list.id == tunes.DJID)
          cmbPlaylist.SelectedItem = list;
      }

      tunes.Status();
      tunes.GetPlaylist(tunes.DJID);
      tmrRefresh.Start();

      Notify.Tag = this;
      
      System.Drawing.IconConverter ic = new System.Drawing.IconConverter();

      pauseIco = Icon.FromHandle(Properties.Resources.pause.GetHicon());
      playIco = Icon.FromHandle(Properties.Resources.play.GetHicon());

      btnPrevThumb = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(Icon.FromHandle(Properties.Resources.prev.GetHicon()), "Previous");
      btnPrevThumb.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(btnPrev_Click);
      btnPlayThumb = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(pauseIco, "Play/Pause");
      btnPlayThumb.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(btnPP_Click);
      btnNextThumb = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(Icon.FromHandle(Properties.Resources.next.GetHicon()), "Next");
      btnNextThumb.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(btnNext_Click);
      Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, btnPrevThumb, btnPlayThumb, btnNextThumb);

      tmrTick.Start();
    }

    #region Form Events

    private void btnNextThumb_Click()
    {

    }
    private void btnPrevThumb_Click()
    {

    }

    void btnNext_Click(object sender, EventArgs e)
    {
      tunes.SkipNext();
    }

    private void btnPP_Click(object sender, EventArgs e)
    {
      tunes.PlayPause();
    }

    void btnPrev_Click(object sender, EventArgs e)
    {
      tunes.SkipPrev();
    }

    private void Notify_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
      this.ShowInTaskbar = true;
      Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
        ;//this.ShowInTaskbar = false;
    }

    private void btnVolUp_Click(object sender, EventArgs e)
    {
       tunes.SetVolume(Vol.Value + 5);
    }

    private void btnVolDown_Click(object sender, EventArgs e)
    {
      tunes.SetVolume(Vol.Value - 5);
    }

    private void djQueueSong(object sender, EventArgs e)
    {
      int id = (int)((DataRowView)AllMusicSource.Current).Row["id"];
      if (musicDB.Count > id)
        btnQueue.Text = "Queue " + musicDB[id].Name;
    }

    private void btnQueue_Click(object sender, EventArgs e)
    {
      int id = (int)((DataRowView)AllMusicSource.Current).Row["id"];
      if (musicDB.Count > id)
        tunes.VoteSong(musicDB[id].perID, musicDB[id].trackID);
    }

    #endregion


    private void tmrRefresh_Tick(object sender, EventArgs e)
    {
      tunes.Status();
    }

    private void StatusEvent(DAAP.ContentNode node)
    {
      if (mutex.WaitOne(5))
      {
        try
        {
          btnPlayThumb.Icon = ((byte)(node.GetChild("tune.status").Value) == 3 ? playIco : pauseIco);
          if ((byte)(node.GetChild("tune.status").Value) < 3)
          {
            mutex.ReleaseMutex();
            return;
          }
          lblTrack.Text = node.GetChild("tune.track").Value.ToString();
          lblArtist.Text = node.GetChild("tune.artist").Value.ToString();
          lblAlbum.Text = node.GetChild("tune.album").Value.ToString();
        }
        catch (System.Exception ex)
        {
          mutex.ReleaseMutex();
          return;
        }
        string prev = lblTrack.Text + lblArtist.Text + lblAlbum.Text;

        if (prev != (string)this.Tag)
        {
          this.Tag = prev;

          Notify.ShowBalloonTip(5000, "Now Playing:", lblTrack.Text + "\n" + lblArtist.Text + "\n" + lblAlbum.Text, ToolTipIcon.Info);
          string ico = "Now Playing: " + lblTrack.Text;
          if (ico.Length > 63)
            Notify.Text = ico.Substring(0, 60) + "...";
          else
            Notify.Text = ico;

          int songNum;

          for (songNum = 0; songNum < playlist.Count; ++songNum)
          {
            if (playlist[songNum].Name == lblTrack.Text && playlist[songNum].trackArtist == lblArtist.Text)
            {
              break;
            }
          }

          currSong = null;
          nextSong = null;
          nextNext = null;

          if (songNum < playlist.Count - 2)
          {
            SetMeta(songNum);
            artBox.Image = (playlist[songNum].art != null ? new Bitmap(playlist[songNum].art, artBox.Size) : null);
          }

          if ((songNum > playlist.Count - 5 && playlist.Count > 8) || playlist.Count < 8)
          {
            tunes.GetPlaylist(((Playlist)(cmbPlaylist.SelectedItem)).id);
          }
        }

        TimeSpan songPrg = TimeSpan.FromMilliseconds((int)node.GetChild("tune.remaining").Value);
        TimeSpan songTotal = TimeSpan.Zero;
        DAAP.ContentNode content = node.GetChild("tune.total");
        if (content != null)
          songTotal = TimeSpan.FromMilliseconds((int)content.Value);

        if (currSong == null)
        {
          int play = 0;
          if (songTotal.TotalMilliseconds > 0)
          {
            play = 100 - (int)((100.0 * songPrg.TotalMilliseconds) / songTotal.TotalMilliseconds);
            if (prgTimestamp.Value != play)
              prgTimestamp.Value = play;
          }
           
          songPrg = songTotal - songPrg;
          
          lblTimestamp.Text = string.Format("{0}:{1:D2} / {2}:{3:D2}", new object[] { songPrg.Hours * 60 + songPrg.Minutes, songPrg.Seconds, songTotal.Minutes, songTotal.Seconds });
          Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(play, 100, this.Handle);

        }
        else
        {
          currSong.trackLength = songTotal;
          currSong.trackPos = songPrg;
        }

        tunes.GetVolume();

        mutex.ReleaseMutex();
      }
    }

    private delegate void delegateMeta(int d);

    private void NowPlayingPlaylist(List<Song> songs)
    {
      if (!playlistMutex.WaitOne(5))
        return;

      if (songs == null)
        return;

      // Try to merge...
      if (playlist.Count == 0)
      {
//        PopulateArt(ref songs, 0);
        playlist = songs;
        for (int j = 0; j < playlist.Count; ++j)
        {
          if (playlist[j].Name == lblTrack.Text && playlist[j].trackArtist == lblArtist.Text)
          {
            //this.Invoke(new delegateMeta(SetMeta), new Object[] { j });
            SetMeta(j);
            break;
          }
        }
        playlistMutex.ReleaseMutex();
        return;
      }

      int i = 0;
      for (; i < playlist.Count; ++i)
      {
        if (playlist[i].trackID == songs[0].trackID)
          break;
      }

      if (i < playlist.Count)
      {
        // Hit
        int oldCount = playlist.Count;
        i = oldCount - i;
        //PopulateArt(ref songs, i);
        for (int j = i; j < songs.Count; ++j)
        {
          playlist.Add(songs[j]);
        }
        i = oldCount - i;
        for (int j = 0; j < i; ++j)
        {
          playlist.RemoveAt(0);
        }
        
        for (int j = 0; j < playlist.Count; ++j)
        {
          if (playlist[j].Name == lblTrack.Text && playlist[j].trackArtist == lblArtist.Text)
          {
            //this.Invoke(new delegateMeta(SetMeta), new Object[] { j });
            SetMeta(j);
            break;
          }
        }
      }
      else
      {
        // No hit - complete replace
        //PopulateArt(ref songs, 0);
        playlist = songs;
        //this.Invoke(new delegateMeta(SetMeta), new Object[] { 0 });
        SetMeta(0);
      }

      playlistMutex.ReleaseMutex();
    }

    private void PopulateArt(ref List<Song> list, int start)
    {
      if (list == null)
        return;

      for (int i = start; i < list.Count; ++i)
      {
        /*System.IO.Stream imgStream = tunes.GetItemArt(list[i].trackId, artBox.Width, artBox.Height);
        if (imgStream != null)
          list[i].art = new Bitmap(imgStream);
        else
        {
          list[i].art = new Bitmap(Properties.Resources.coverart, artBox.Width, artBox.Height);
        }*/
      }
    }

    private Bitmap GetItemArt(ref Song song, Size size)
    {
      if (song.art == null)
      {
        // Attempt retrieval
        System.IO.Stream ioStream = tunes.GetItemArt(ref song, artBox.Width, artBox.Height);
        if (ioStream != null && ioStream.Length > 0)
        {
          song.art = new Bitmap(ioStream);
        }
        else
        {
          song.art = Properties.Resources.coverart;
        }
      }

      return (song.art == null ? null : new Bitmap(song.art, size));
    }

    private void SetMeta(int num)
    {
      if (num >= playlist.Count - 2)
        return;

      currSong = playlist[num];

      Song s = nextSong = playlist[num + 1];
      lblTrackNext.Text = s.trackName;
      lblArtistNext.Text = s.trackArtist;
      lblAlbumNext.Text = s.trackAlbum;
      lblLenNext.Text = string.Format("{0}:{1:D2}", s.trackLength.Minutes, s.trackLength.Seconds);
      artNext.Image = GetItemArt(ref s, artNext.Size);

      s = nextNext = playlist[num + 2];
      lblTrackNextNext.Text = s.trackName;
      lblArtistNextNext.Text = s.trackArtist;
      lblAlbumNextNext.Text = s.trackAlbum;
      lblLenNextNext.Text = string.Format("{0}:{1:D2}", s.trackLength.Minutes, s.trackLength.Seconds);
      artNextNext.Image = GetItemArt(ref s, artNext.Size);
    }

    private void TabChange(object sender, EventArgs e)
    {
      if (tabControl1.SelectedIndex == 1)
      {
        if (firstSongList)
        {
          firstSongList = false;
          tunes.GetPlaylist(tunes.MusicID);
        }
      }
    }

    void setMusicList(List<Song> list)
    {
      AllMusicSource.DataSource = list;
    }

    private delegate void delegateHandleTunes(TunesMsg data);
    internal void TunesHandleEvent(TunesMsg data)
    {
      try
      {
        Invoke(new delegateHandleTunes(tunesHandleEventDlg), new object[] { data });
      }
      catch (ObjectDisposedException ex)
      {
        // Form died during invoke (exiting, most likely)
      }
    }

    private void tunesHandleEventDlg(TunesMsg msg)
    {
      //System.Diagnostics.Debug.WriteLine("Main-thread - tunes event: " + System.Enum.GetName(msg.ReqType.GetType(), msg.ReqType));
      switch (msg.ReqType)
      {
        case MsgType.MSG_GETVOL:
          Vol.Value = (int)msg["vol"];
          break;

        case MsgType.MSG_STATUS:
          StatusEvent((DAAP.ContentNode)msg["status"]);
          break;

        case MsgType.MSG_GET_PLAYLIST:
          if ((int)(msg.data["plId"]) == ((Playlist)(cmbPlaylist.SelectedItem)).id)
          {
            NowPlayingPlaylist((List<Song>)msg.data["plist"]);
          }
          
          if ((int)(msg.data["plId"]) == tunes.MusicID)
          {
            //AllMusicSource.DataSource = (List<Song>)msg.data["plist"];
            musicDB = (List<Song>)msg.data["plist"];
            DataTable t = new DataTable();
            DataColumn c = new DataColumn("id", System.Type.GetType("System.Int32"));
            c.Unique = true;
            t.Columns.Add(c);
            c = new DataColumn("name", System.Type.GetType("System.String"));
            t.Columns.Add(c);
            c = new DataColumn("album", System.Type.GetType("System.String"));
            t.Columns.Add(c);
            c = new DataColumn("artist", System.Type.GetType("System.String"));
            t.Columns.Add(c);
            DataColumn[] pk = new DataColumn[1];
            pk[0] = t.Columns["id"];
            t.PrimaryKey = pk;
            
            for (int i = 0; i < musicDB.Count; ++i)
            {
              Song s = musicDB[i];
              DataRow r = t.NewRow();
              r["id"] = i;
              r["name"] = s.Name;
              r["album"] = s.Album;
              r["artist"] = s.Artist;
              t.Rows.Add(r);
            }
            AllMusicSource.DataSource = t;
          }
          break;

        case MsgType.MSG_GET_ART:
          // Go thru current and the two meta thingies to see if the id's match... if so, we have art!
          int id = (int)msg.data["item"];
          System.IO.Stream stream = (System.IO.Stream)msg.data["art"];
          if (stream != null)
          {
            try
            {
              if (id == currSong.trackID)
              {
                currSong.art = new Bitmap(stream);
                artBox.Image = currSong.art;
              }
              else if (id == nextSong.trackID)
              {
                nextSong.art = new Bitmap(stream);
                artNext.Image = nextSong.art;
              }
              else if (id == nextNext.trackID)
              {
                nextNext.art = new Bitmap(stream);
                artNextNext.Image = nextNext.art;
              }
            }
            catch (Exception e)
            {
            }
          }
          break;

        default:
          System.Diagnostics.Debug.WriteLine("Main thread - unknown event: " + System.Enum.GetName(msg.ReqType.GetType(), msg.ReqType));
          break;
      }
    }

    private void tmrTick_Tick(object sender, EventArgs e)
    {
      if (currSong != null)
      {
        currSong.trackPos -= TimeSpan.FromMilliseconds(100);
        TimeSpan songPrg = currSong.trackPos;

        int play = 0;
        if (currSong.trackLength.TotalMilliseconds > 0)
          play = 100 - (int)((100.0 * songPrg.TotalMilliseconds) / currSong.trackLength.TotalMilliseconds);

        if (play < 0)
          play = 0;
        else if (play > 100)
          play = 100;

        if (prgTimestamp.Value != play)
          prgTimestamp.Value = play;

        songPrg = currSong.trackLength - songPrg;

        lblTimestamp.Text = string.Format("{0}:{1:D2} / {2}:{3:D2}", new object[] { songPrg.Hours * 60 + songPrg.Minutes, songPrg.Seconds, currSong.trackLength.Minutes, currSong.trackLength.Seconds });
        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(play, 100, this.Handle);
      }
    }

    private void dataGrid_ColHeaderClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], ListSortDirection.Ascending);
    }

    private void cmbPlaylist_SelectedIndexChanged(object sender, EventArgs e)
    {
      tunes.GetPlaylist(((Playlist)(cmbPlaylist.SelectedItem)).id);
    }
  }
}
