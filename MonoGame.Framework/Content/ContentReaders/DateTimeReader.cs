// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Content
{
    internal class DateTimeReader : ContentTypeReader<DateTime>
    {
        public DateTimeReader()
        {
        }

        public override DateTime Read(ContentReader input, DateTime existingInstance)
        {
            ulong value = input.ReadUInt64();
            const ulong mask = (ulong)3 << 62;
            long ticks = (long)(value & ~mask);
            DateTimeKind kind = (DateTimeKind)(value >> 62 & 3);
            return new DateTime(ticks, kind);
        }
    }
}
