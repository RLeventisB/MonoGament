// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class CharReader : ContentTypeReader<char>
    {
        public CharReader()
        {
        }

        public override char Read(ContentReader input, char existingInstance)
        {
            return input.ReadChar();
        }
    }
}
