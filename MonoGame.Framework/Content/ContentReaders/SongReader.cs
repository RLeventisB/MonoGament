// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Media;

using MonoGame.Framework.Utilities;

using System;
using System.IO;

namespace Microsoft.Xna.Framework.Content
{
    internal class SongReader : ContentTypeReader<Song>
    {
        public override Song Read(ContentReader input, Song existingInstance)
        {
            var path = input.ReadString();

            if (!string.IsNullOrEmpty(path))
            {
                // Add the ContentManager's RootDirectory
                var dirPath = Path.Combine(input.ContentManager.RootDirectoryFullPath, input.AssetName);

                // Resolve the relative path
                path = FileHelpers.ResolveRelativePath(dirPath, path);
            }

            var durationMs = input.ReadObject<int>();

            return new Song(path, durationMs);
        }
    }
}
