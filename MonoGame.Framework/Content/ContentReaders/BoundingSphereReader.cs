// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class BoundingSphereReader : ContentTypeReader<BoundingSphere>
    {
        public BoundingSphereReader()
        {
        }

        public override BoundingSphere Read(ContentReader input, BoundingSphere existingInstance)
        {
            Vector3 center = input.ReadVector3();
            float radius = input.ReadSingle();
            return new BoundingSphere(center, radius);
        }
    }
}
