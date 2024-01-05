// #region License
// /*
// Microsoft Public License (Ms-PL)
// MonoGame - Copyright © 2009 The MonoGame Team
// 
// All rights reserved.
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. If you do not
// accept the license, do not use the software.
// 
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under 
// U.S. copyright law.
// 
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
// each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
// each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
// your patent license from such contributor to the software ends automatically.
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
// notices that are present in the software.
// (D) If you distribute any portion of the software in source code form, you may do so only under this license by including 
// a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object 
// code form, you may only do so under a license that complies with this license.
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees
// or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent
// permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular
// purpose and non-infringement.
// */
// #endregion License
// 

using Android.Views;

using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class Keyboard
    {
        private static readonly IDictionary<Keycode, Keys> KeyMap = LoadKeyMap();
        internal static bool KeyDown(Keycode keyCode)
        {
            if (KeyMap.TryGetValue(keyCode, out Keys key))
            {
                ref KeyData keyData = ref GetDataFromKey(key);
                SetActive(ref keyData);
                keyData.TotalRepeatCount++;
                OnKeyDown?.Invoke(key);
                keyData.FrameRepeatCount++;

                return true;
            }
            return false;
        }

        internal static bool KeyUp(Keycode keyCode)
        {
            if (KeyMap.TryGetValue(keyCode, out Keys key))
            {
                ref KeyData keyData = ref GetDataFromKey(key);
                keyData.TotalRepeatCount = 1;
                OnKeyUp?.Invoke(key);
                SetDeactive(ref keyData);

                return true;
            }
            return false;
        }

        private static IDictionary<Keycode, Keys> LoadKeyMap()
        {
            // create a map for every Keycode and default it to none so that every possible key is mapped
            Dictionary<Keycode, Keys> maps = new()
            {
                // then update it with the actual mappings
                [Keycode.DpadLeft] = Keys.Left,
                [Keycode.DpadRight] = Keys.Right,
                [Keycode.DpadUp] = Keys.Up,
                [Keycode.DpadDown] = Keys.Down,
                [Keycode.DpadCenter] = Keys.Enter,
                [Keycode.Num0] = Keys.D0,
                [Keycode.Num1] = Keys.D1,
                [Keycode.Num2] = Keys.D2,
                [Keycode.Num3] = Keys.D3,
                [Keycode.Num4] = Keys.D4,
                [Keycode.Num5] = Keys.D5,
                [Keycode.Num6] = Keys.D6,
                [Keycode.Num7] = Keys.D7,
                [Keycode.Num8] = Keys.D8,
                [Keycode.Num9] = Keys.D9,
                [Keycode.A] = Keys.A,
                [Keycode.B] = Keys.B,
                [Keycode.C] = Keys.C,
                [Keycode.D] = Keys.D,
                [Keycode.E] = Keys.E,
                [Keycode.F] = Keys.F,
                [Keycode.G] = Keys.G,
                [Keycode.H] = Keys.H,
                [Keycode.I] = Keys.I,
                [Keycode.J] = Keys.J,
                [Keycode.K] = Keys.K,
                [Keycode.L] = Keys.L,
                [Keycode.M] = Keys.M,
                [Keycode.N] = Keys.N,
                [Keycode.O] = Keys.O,
                [Keycode.P] = Keys.P,
                [Keycode.Q] = Keys.Q,
                [Keycode.R] = Keys.R,
                [Keycode.S] = Keys.S,
                [Keycode.T] = Keys.T,
                [Keycode.U] = Keys.U,
                [Keycode.V] = Keys.V,
                [Keycode.W] = Keys.W,
                [Keycode.X] = Keys.X,
                [Keycode.Y] = Keys.Y,
                [Keycode.Z] = Keys.Z,
                [Keycode.Space] = Keys.Space,
                [Keycode.Escape] = Keys.Escape,
                [Keycode.Back] = Keys.Back,
                [Keycode.Home] = Keys.Home,
                [Keycode.Enter] = Keys.Enter,
                [Keycode.Period] = Keys.OemPeriod,
                [Keycode.Comma] = Keys.OemComma,
                [Keycode.Menu] = Keys.Help,
                [Keycode.Search] = Keys.BrowserSearch,
                [Keycode.VolumeUp] = Keys.VolumeUp,
                [Keycode.VolumeDown] = Keys.VolumeDown,
                [Keycode.MediaPause] = Keys.Pause,
                [Keycode.MediaPlayPause] = Keys.MediaPlayPause,
                [Keycode.MediaStop] = Keys.MediaStop,
                [Keycode.MediaNext] = Keys.MediaNextTrack,
                [Keycode.MediaPrevious] = Keys.MediaPreviousTrack,
                [Keycode.Mute] = Keys.VolumeMute,
                [Keycode.AltLeft] = Keys.LeftAlt,
                [Keycode.AltRight] = Keys.RightAlt,
                [Keycode.ShiftLeft] = Keys.LeftShift,
                [Keycode.ShiftRight] = Keys.RightShift,
                [Keycode.Tab] = Keys.Tab,
                [Keycode.Del] = Keys.Delete,
                [Keycode.Minus] = Keys.OemMinus,
                [Keycode.LeftBracket] = Keys.OemOpenBrackets,
                [Keycode.RightBracket] = Keys.OemCloseBrackets,
                [Keycode.Backslash] = Keys.OemBackslash,
                [Keycode.Semicolon] = Keys.OemSemicolon,
                [Keycode.PageUp] = Keys.PageUp,
                [Keycode.PageDown] = Keys.PageDown,
                [Keycode.CtrlLeft] = Keys.LeftControl,
                [Keycode.CtrlRight] = Keys.RightControl,
                [Keycode.CapsLock] = Keys.CapsLock,
                [Keycode.ScrollLock] = Keys.Scroll,
                [Keycode.NumLock] = Keys.NumLock,
                [Keycode.Insert] = Keys.Insert,
                [Keycode.F1] = Keys.F1,
                [Keycode.F2] = Keys.F2,
                [Keycode.F3] = Keys.F3,
                [Keycode.F4] = Keys.F4,
                [Keycode.F5] = Keys.F5,
                [Keycode.F6] = Keys.F6,
                [Keycode.F7] = Keys.F7,
                [Keycode.F8] = Keys.F8,
                [Keycode.F9] = Keys.F9,
                [Keycode.F10] = Keys.F10,
                [Keycode.F11] = Keys.F11,
                [Keycode.F12] = Keys.F12,
                [Keycode.NumpadDivide] = Keys.Divide,
                [Keycode.NumpadMultiply] = Keys.Multiply,
                [Keycode.NumpadSubtract] = Keys.Subtract,
                [Keycode.NumpadAdd] = Keys.Add
            };

            return maps;
        }
    }
}
