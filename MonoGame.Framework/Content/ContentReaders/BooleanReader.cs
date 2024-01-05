// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class BooleanReader : ContentTypeReader<bool>
    {
        public BooleanReader()
        {
        }

        public override bool Read(ContentReader input, bool existingInstance)
        {
            return input.ReadBoolean();
        }
    }
}
