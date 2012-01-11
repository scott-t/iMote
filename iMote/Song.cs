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
using System.Linq;
using System.Text;

namespace iMote
{
  class Song
  {
    public int trackID;
    public ulong perID;
    public string trackName;
    public string trackAlbum;
    public string trackArtist;

    public TimeSpan trackPos = new TimeSpan();
    public TimeSpan trackLength;

    public System.Drawing.Bitmap art = null;// = Properties.Resources.coverart;

    public string Name
    {
      get { return trackName; }
      set { trackName = value; }
    }

    public string Artist
    {
      get { return trackArtist; }
      set { trackAlbum = value; }
    }

    public string Album
    {
      get { return trackAlbum; }
      set { trackAlbum = value; }
    }

    public static List<Song> parsePlaylist(DAAP.ContentNode node)
    {
      if (node == null)
        return null;

      List<Song> list = new List<Song>();

      DAAP.ContentNode n = node.GetChild("dmap.listing");
      foreach (DAAP.ContentNode song in (DAAP.ContentNode[])n.Value)
      {
        Song s = new Song();
        s.trackAlbum = GetString(song, "daap.songalbum");
        s.trackArtist = GetString(song, "daap.songartist");
        s.trackID = GetInt(song, "dmap.itemid");
        s.trackName = GetString(song, "dmap.itemname");
        s.perID = GetLong(song, "dmap.persistentid");

        DAAP.ContentNode len = song.GetChild("daap.songtime");
        if (len != null)
          s.trackLength = TimeSpan.FromMilliseconds((int)len.Value);

        list.Add(s);
      }

      return list;
    }

    private static string GetString(DAAP.ContentNode n, string name)
    {
      DAAP.ContentNode t = n.GetChild(name);
      if (t == null)
        return "";
      else
        return (string)(t.Value);
    }

    private static int GetInt(DAAP.ContentNode n, string name)
    {
      DAAP.ContentNode t = n.GetChild(name);
      if (t == null)
        return 0;
      else
        return (int)(t.Value);
    }

    private static ulong GetLong(DAAP.ContentNode n, string name)
    {
      DAAP.ContentNode t = n.GetChild(name);
      if (t == null)
        return 0;
      else
        return (ulong)((long)(t.Value));
    }
  }
}
