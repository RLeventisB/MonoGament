// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Content
{
    internal class CurveReader : ContentTypeReader<Curve>
    {
        public override Curve Read(ContentReader input, Curve existingInstance)
        {
            Curve curve = existingInstance;
            curve ??= new Curve();

            curve.PreLoop = (CurveLoopType)input.ReadInt32();
            curve.PostLoop = (CurveLoopType)input.ReadInt32();
            int num6 = input.ReadInt32();

            for (int i = 0; i < num6; i++)
            {
                float position = input.ReadSingle();
                float num4 = input.ReadSingle();
                float tangentIn = input.ReadSingle();
                float tangentOut = input.ReadSingle();
                CurveContinuity continuity = (CurveContinuity)input.ReadInt32();
                curve.Keys.Add(new CurveKey(position, num4, tangentIn, tangentOut, continuity));
            }
            return curve;
        }
    }
}

