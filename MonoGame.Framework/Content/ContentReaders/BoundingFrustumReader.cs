// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class BoundingFrustumReader : ContentTypeReader<BoundingFrustum>
    {
        public BoundingFrustumReader()
        {
        }

        public override BoundingFrustum Read(ContentReader input, BoundingFrustum existingInstance)
        {
            return new BoundingFrustum(input.ReadMatrix());
        }
    }
}
