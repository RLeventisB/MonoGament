// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Allows getting keystrokes from keyboard.
    /// </summary>
    public static partial class Keyboard
    {
        public static List<KeyData> ActiveKeyDatas = new List<KeyData>(6);
        public static Action<Keys> OnKeyDown;
        public static Action<Keys> OnKeyUp;
        public static Action<char> TextInput;
        public static unsafe readonly KeyData* KeysPointer;
        unsafe static Keyboard()
        {
            KeysPointer = (KeyData*)NativeMemory.AllocZeroed(sizeof(uint) * 255);
            for (int i = 0; i < byte.MaxValue; i++)
            {
                KeysPointer[i] = new KeyData();
            }
        }
        #region Public Static Methods
        public static void SetActive(ref KeyData data)
        {
            data.HasPressedThisFrame = true;
            if (!ActiveKeyDatas.Contains(data))
            {
                ActiveKeyDatas.Add(data);
            }
        }
        public static void SetDeactive(ref KeyData data)
        {
            ActiveKeyDatas.Remove(data);
        }
        public static bool IsKeyPressed(Keys key) => GetDataFromKey(key).TotalRepeatCount > 1;
        public static bool IsKeyRepeat(Keys key) => GetDataFromKey(key).TotalRepeatCount > 2;
        public static ushort KeyRepeatCount(Keys key) => GetDataFromKey(key).TotalRepeatCount;
        public static bool IsKeyPressedWithDelay(Keys key) => GetDataFromKey(key).HasPressedThisFrame;
        public static ushort GetFrameInputCount(Keys key) => GetDataFromKey(key).FrameRepeatCount;
        public static bool IsKeyPressedFirst(Keys key) => GetDataFromKey(key).HasPressedThisFrame && GetDataFromKey(key).TotalRepeatCount == 2;
        public static unsafe ref KeyData GetDataFromKey(Keys key) => ref KeysPointer[(byte)key];
        #endregion
    }
}
