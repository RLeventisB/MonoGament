// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class SByteReader : ContentTypeReader<sbyte>
    {
        public SByteReader()
        {
        }

        public override sbyte Read(ContentReader input, sbyte existingInstance)
        {
            return input.ReadSByte();
        }
    }
}
