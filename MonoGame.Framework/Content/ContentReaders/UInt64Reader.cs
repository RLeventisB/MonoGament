// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class UInt64Reader : ContentTypeReader<ulong>
    {
        public UInt64Reader()
        {
        }

        public override ulong Read(ContentReader input, ulong existingInstance)
        {
            return input.ReadUInt64();
        }
    }
}
