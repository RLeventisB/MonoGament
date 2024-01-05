// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class QuaternionReader : ContentTypeReader<Quaternion>
    {
        public QuaternionReader()
        {
        }

        public override Quaternion Read(ContentReader input, Quaternion existingInstance)
        {
            return input.ReadQuaternion();
        }
    }
}
