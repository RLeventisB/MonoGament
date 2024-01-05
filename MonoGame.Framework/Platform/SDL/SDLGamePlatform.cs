// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Framework.Utilities;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Microsoft.Xna.Framework
{
    public class SdlGamePlatform : GamePlatform
    {
        public static Dictionary<Sdl.EventType, EventRegisterPair> OnEventDictionary = new Dictionary<Sdl.EventType, EventRegisterPair>();
        static SdlGamePlatform()
        {
            foreach (var item in Enum.GetValues<Sdl.EventType>())
            {
                if (!OnEventDictionary.ContainsKey(item))
                    OnEventDictionary.Add(item, new EventRegisterPair());
            }
        }

        public override GameRunBehavior DefaultRunBehavior => GameRunBehavior.Synchronous;

        private readonly Game _game;

        private int _isExiting;
        private SdlGameWindow _view;

        private readonly List<string> _dropList;

        public SdlGamePlatform(Game game)
            : base(game)
        {
            _game = game;

            Sdl.GetVersion(out Sdl.version);

            var minVersion = new Sdl.Version() { Major = 2, Minor = 0, Patch = 5 };

            if (Sdl.version < minVersion)
                Debug.WriteLine("Please use SDL " + minVersion + " or higher.");

            // Needed so VS can debug the project on Windows
            if (Sdl.version >= minVersion && CurrentPlatform.OS == OS.Windows && Debugger.IsAttached)
                Sdl.SetHint("SDL_WINDOWS_DISABLE_THREAD_NAMING", "1");

            _dropList = new List<string>();

            Sdl.Init((int)(
                Sdl.InitFlags.Video |
                Sdl.InitFlags.Joystick |
                Sdl.InitFlags.GameController |
                Sdl.InitFlags.Haptic
            ));

            Sdl.DisableScreenSaver();

            GamePad.InitDatabase();
            Window = _view = new SdlGameWindow(_game);
        }

        public override void BeforeInitialize()
        {
            SdlRunLoop();

            base.BeforeInitialize();
        }

        protected override void OnIsMouseVisibleChanged()
        {
            _view.SetCursorVisible(_game.IsMouseVisible);
        }

        internal override void OnPresentationChanged(PresentationParameters pp)
        {
            var displayIndex = Sdl.Window.GetDisplayIndex(Window.Handle);
            var displayName = Sdl.Display.GetDisplayName(displayIndex);
            BeginScreenDeviceChange(pp.IsFullScreen);
            EndScreenDeviceChange(displayName, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        public override void RunLoop()
        {
            Sdl.Window.Show(Window.Handle);

            while (true)
            {
                SdlRunLoop();
                Game.Tick();
                Threading.Run();
                GraphicsDevice.DisposeContexts();

                if (_isExiting > 0)
                    break;
            }
        }

        private unsafe void SdlRunLoop()
        {
            for (int i = 0; i < 255; i++)
            {
                ref KeyData data = ref Keyboard.KeysPointer[i];
                data.HasPressedThisFrame = false;
                data.FrameRepeatCount = 0;
            }
            char* charsBuffer = stackalloc char[32]; // SDL_TEXTINPUTEVENT_TEXT_SIZE

            while (Sdl.PollEvent(out Sdl.Event ev) == 1)
            {
                bool hasRegister = OnEventDictionary.TryGetValue(ev.Type, out EventRegisterPair pair);
                if (hasRegister)
                    pair.BeforeEvent.Invoke(ev);
                switch (ev.Type)
                {
                    case Sdl.EventType.Quit:
                        _isExiting++;
                        break;
                    case Sdl.EventType.JoyDeviceAdded:
                        Joystick.AddDevices();
                        break;
                    case Sdl.EventType.JoyDeviceRemoved:
                        Joystick.RemoveDevice(ev.JoystickDevice.Which);
                        break;
                    case Sdl.EventType.ControllerDeviceRemoved:
                        GamePad.RemoveDevice(ev.ControllerDevice.Which);
                        break;
                    case Sdl.EventType.ControllerButtonUp:
                    case Sdl.EventType.ControllerButtonDown:
                    case Sdl.EventType.ControllerAxisMotion:
                        GamePad.UpdatePacketInfo(ev.ControllerDevice.Which, ev.ControllerDevice.TimeStamp);
                        break;
                    case Sdl.EventType.MouseWheel:
                        const int wheelDelta = 120;
                        Mouse.ScrollY += ev.Wheel.Y * wheelDelta;
                        Mouse.ScrollX += ev.Wheel.X * wheelDelta;
                        break;
                    case Sdl.EventType.KeyDown:
                    {
                        Keys key = KeyboardUtil.ToXna(ev.Key.Keysym.Sym);
                        ref KeyData keydata = ref Keyboard.GetDataFromKey(key);
                        keydata.TotalRepeatCount++;
                        Keyboard.OnKeyDown?.Invoke(key);
                        Keyboard.SetActive(ref keydata);
                        keydata.FrameRepeatCount++;
                        break;
                    }
                    case Sdl.EventType.KeyUp:
                    {
                        Keys key = KeyboardUtil.ToXna(ev.Key.Keysym.Sym);
                        ref KeyData keydata = ref Keyboard.GetDataFromKey(key);
                        keydata.TotalRepeatCount = 1;
                        Keyboard.OnKeyUp?.Invoke(key);
                        keydata.HasPressedThisFrame = false;
                        Keyboard.SetDeactive(ref keydata);
                        break;
                    }
                    case Sdl.EventType.TextInput:
                        // Based on the SDL2# LPUtf8StrMarshaler
                        int bytes = MeasureStringLength(ev.Text.Text);
                        if (bytes > 0)
                        {
                            /* UTF8 will never encode more characters
                             * than bytes in a string, so bytes is a
                             * suitable upper estimate of size needed
                             */

                            for (int i = 0; i < Encoding.UTF8.GetChars(ev.Text.Text, bytes, charsBuffer, bytes); i++)
                            {
                                Keyboard.TextInput?.Invoke(charsBuffer[i]);
                            }
                        }
                        break;
                    case Sdl.EventType.WindowEvent:

                        // If the ID is not the same as our main window ID
                        // that means that we received an event from the
                        // dummy window, so don't process the event.
                        if (ev.Window.WindowID != _view.Id)
                            break;

                        switch (ev.Window.EventID)
                        {
                            case Sdl.Window.EventId.Resized:
                            case Sdl.Window.EventId.SizeChanged:
                                _view.ClientResize(ev.Window.Data1, ev.Window.Data2);
                                break;
                            case Sdl.Window.EventId.FocusGained:
                                IsActive = true;
                                break;
                            case Sdl.Window.EventId.FocusLost:
                                IsActive = false;
                                break;
                            case Sdl.Window.EventId.Moved:
                                _view.Moved();
                                break;
                            case Sdl.Window.EventId.Close:
                                _isExiting++;
                                break;
                        }
                        break;

                    case Sdl.EventType.DropFile:
                        if (ev.Drop.WindowId != _view.Id)
                            break;

                        string path = InteropHelpers.Utf8ToString(ev.Drop.File);
                        Sdl.Drop.SDL_Free(ev.Drop.File);
                        _dropList.Add(path);

                        break;

                    case Sdl.EventType.DropComplete:
                        if (ev.Drop.WindowId != _view.Id)
                            break;

                        if (_dropList.Count > 0)
                        {
                            _view.OnFileDrop(new FileDropEventArgs(_dropList.ToArray()));
                            _dropList.Clear();
                        }

                        break;
                }
                if (hasRegister)
                    pair.AfterEvent.Invoke(ev);
            }
        }
        private static unsafe int MeasureStringLength(byte* ptr)
        {
            int bytes;
            for (bytes = 0; *ptr != 0; ptr += 1, bytes += 1) ;
            return bytes;
        }
        private int UTF8ToUnicode(int utf8)
        {
            int
                byte4 = utf8 & 0xFF,
                byte3 = utf8 >> 8 & 0xFF,
                byte2 = utf8 >> 16 & 0xFF,
                byte1 = utf8 >> 24 & 0xFF;

            switch (byte1)
            {
                case < 0x80:
                    return byte1;
                case < 0xC0:
                    return -1;
                case < 0xE0 when byte2 >= 0x80 && byte2 < 0xC0:
                    return byte1 % 0x20 * 0x40 + byte2 % 0x40;
                case < 0xF0 when byte2 >= 0x80 && byte2 < 0xC0 && byte3 >= 0x80 && byte3 < 0xC0:
                    return byte1 % 0x10 * 0x40 * 0x40 + byte2 % 0x40 * 0x40 + byte3 % 0x40;
                case < 0xF8 when byte2 >= 0x80 && byte2 < 0xC0 && byte3 >= 0x80 && byte3 < 0xC0 && byte4 >= 0x80 && byte4 < 0xC0:
                    return byte1 % 0x8 * 0x40 * 0x40 * 0x40 + byte2 % 0x40 * 0x40 * 0x40 + byte3 % 0x40 * 0x40 + byte4 % 0x40;
                default:
                    return -1;
            }
        }

        public override void StartRunLoop()
        {
            throw new NotSupportedException("The desktop platform does not support asynchronous run loops");
        }

        public override void Exit()
        {
            Interlocked.Increment(ref _isExiting);
        }

        public override bool BeforeUpdate(GameTime gameTime)
        {
            return true;
        }

        public override bool BeforeDraw(GameTime gameTime)
        {
            return true;
        }

        public override void EnterFullScreen()
        {
        }

        public override void ExitFullScreen()
        {
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            _view.BeginScreenDeviceChange(willBeFullScreen);
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            _view.EndScreenDeviceChange(screenDeviceName, clientWidth, clientHeight);
        }

        public override void Log(string message)
        {
            Console.WriteLine(message);
        }

        public override void Present()
        {
            Game.GraphicsDevice?.Present();
        }

        protected override void Dispose(bool disposing)
        {
            if (_view != null)
            {
                _view.Dispose();
                _view = null;

                Joystick.CloseDevices();

                Sdl.Quit();
            }

            base.Dispose(disposing);
        }
    }
    public struct EventRegisterPair
    {
        public static readonly Action<Sdl.Event> Noop = _ => { };
        public Action<Sdl.Event> BeforeEvent = Noop, AfterEvent = Noop;
        public EventRegisterPair()
        {
        }
    }
}
