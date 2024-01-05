// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
    internal class TextureReader : ContentTypeReader<Texture>
    {
        public override Texture Read(ContentReader reader, Texture existingInstance)
        {
            return existingInstance;
        }
    }
}
