// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class ByteReader : ContentTypeReader<byte>
    {
        public ByteReader()
        {
        }

        public override byte Read(ContentReader input, byte existingInstance)
        {
            return input.ReadByte();
        }
    }
}
