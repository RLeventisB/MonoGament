// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
    public struct HalfSingle : IPackedVector<ushort>, IEquatable<HalfSingle>, IPackedVector
    {
        ushort packedValue;

        public HalfSingle(float single)
        {
            packedValue = HalfTypeHelper.Convert(single);
        }

        [CLSCompliant(false)]
        public ushort PackedValue
        {
            get => packedValue;
            set => packedValue = value;
        }

        public float ToSingle()
        {
            return HalfTypeHelper.Convert(packedValue);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            packedValue = HalfTypeHelper.Convert(vector.X);
        }

        /// <summary>
        /// Gets the packed vector in Vector4 format.
        /// </summary>
        /// <returns>The packed vector in Vector4 format</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(ToSingle(), 0f, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == GetType())
            {
                return this == (HalfSingle)obj;
            }

            return false;
        }

        public bool Equals(HalfSingle other)
        {
            return packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return ToSingle().ToString();
        }

        public override int GetHashCode()
        {
            return packedValue.GetHashCode();
        }

        public static bool operator ==(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
