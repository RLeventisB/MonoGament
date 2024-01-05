// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class Song : IEquatable<Song>, IDisposable
    {
        private string _name;
        private int _playCount;
        private TimeSpan _duration = TimeSpan.Zero;
        bool disposed;
        /// <summary>
        /// Gets the Album on which the Song appears.
        /// </summary>
        public Album Album => PlatformGetAlbum();

        /// <summary>
        /// Gets the Artist of the Song.
        /// </summary>
        public Artist Artist => PlatformGetArtist();

        /// <summary>
        /// Gets the Genre of the Song.
        /// </summary>
        public Genre Genre => PlatformGetGenre();

        public bool IsDisposed => disposed;

#if ANDROID || OPENAL || WEB || IOS
        internal delegate void FinishedPlayingHandler(object sender, EventArgs args);
#if !DESKTOPGL
        event FinishedPlayingHandler DonePlaying;
#endif
#endif
        internal Song(string fileName, int durationMS)
            : this(fileName)
        {
            _duration = TimeSpan.FromMilliseconds(durationMS);
        }

        internal Song(string fileName)
        {
            _name = fileName;

            PlatformInitialize(fileName);
        }

        ~Song()
        {
            Dispose(false);
        }

        internal string FilePath => _name;

        /// <summary>
        /// Returns a song that can be played via <see cref="MediaPlayer"/>.
        /// </summary>
        /// <param name="name">The name for the song. See <see cref="Song.Name"/>.</param>
        /// <param name="uri">The path to the song file.</param>
        /// <returns></returns>
        public static Song FromUri(string name, Uri uri)
        {
            var song = new Song(uri.OriginalString);
            song._name = name;
            return song;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    PlatformDispose(disposing);
                }

                disposed = true;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Song song)
        {
#if DIRECTX
            return song != null && song.FilePath == FilePath;
#else
            return (object)song != null && Name == song.Name;
#endif
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as Song);
        }

        public static bool operator ==(Song song1, Song song2)
        {
            if ((object)song1 == null)
            {
                return (object)song2 == null;
            }

            return song1.Equals(song2);
        }

        public static bool operator !=(Song song1, Song song2)
        {
            return !(song1 == song2);
        }

        public TimeSpan Duration => PlatformGetDuration();

        public bool IsProtected => PlatformIsProtected();

        public bool IsRated => PlatformIsRated();

        public string Name => PlatformGetName();

        public int PlayCount => PlatformGetPlayCount();

        public int Rating => PlatformGetRating();

        public int TrackNumber => PlatformGetTrackNumber();
    }
}
