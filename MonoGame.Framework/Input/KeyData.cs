namespace Microsoft.Xna.Framework.Input
{
    public struct KeyData
    {
        public ushort TotalRepeatCount // 16 bits
        {
            get => (ushort)data;
            set => data = data & 0xFFFF0000 | value;
        }
        public ushort FrameRepeatCount // 15 bits
        {
            get => (ushort)(data >> 16 & 0x00007FFF);
            set => data = data & 0x8000FFFF | (uint)(value << 16);
        }
        public bool HasPressedThisFrame // 1 bit
        {
            get => (data & 0b_1000_0000_0000_0000_0000_0000_0000_0000) != 0;
            set
            {
                if (value)
                {
                    data |= 2147483648;
                }
                else
                {
                    data &= 2147483647U;
                }
            }
        }
        public uint data;
        public KeyData()
        {
            TotalRepeatCount = 1;
            FrameRepeatCount = 0;
            HasPressedThisFrame = false;
        }
    }
}
