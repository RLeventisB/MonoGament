﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Media
{
    public sealed class AlbumCollection : IDisposable
    {
        private List<Album> albumCollection;

        /// <summary>
        /// Gets the number of Album objects in the AlbumCollection.
        /// </summary>
        public int Count => albumCollection.Count;

        /// <summary>
        /// Gets a value indicating whether the object is disposed.
        /// </summary>
        public bool IsDisposed => false;

        public AlbumCollection(List<Album> albums)
        {
            albumCollection = albums;
        }

        /// <summary>
        /// Gets the Album at the specified index in the AlbumCollection.
        /// </summary>
        /// <param name="index">Index of the Album to get.</param>
        public Album this[int index] => albumCollection[index];

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        public void Dispose()
        {
            foreach (var album in albumCollection)
                album.Dispose();
        }
    }
}
