// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Audio;

using MonoGame.Framework.Utilities;

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MonoGame.OpenAL
{
    public enum ALFormat
    {
        Mono8 = 0x1100,
        Mono16 = 0x1101,
        Stereo8 = 0x1102,
        Stereo16 = 0x1103,
        MonoIma4 = 0x1300,
        StereoIma4 = 0x1301,
        MonoMSAdpcm = 0x1302,
        StereoMSAdpcm = 0x1303,
        MonoFloat32 = 0x10010,
        StereoFloat32 = 0x10011,
    }

    public enum ALError
    {
        NoError = 0,
        InvalidName = 0xA001,
        InvalidEnum = 0xA002,
        InvalidValue = 0xA003,
        InvalidOperation = 0xA004,
        OutOfMemory = 0xA005,
    }

    public enum ALGetString
    {
        Extensions = 0xB004,
    }

    public enum ALBufferi
    {
        UnpackBlockAlignmentSoft = 0x200C,
        LoopSoftPointsExt = 0x2015,
    }

    public enum ALGetBufferi
    {
        Bits = 0x2002,
        Channels = 0x2003,
        Size = 0x2004,
    }

    public enum ALSourceb
    {
        Looping = 0x1007,
    }

    public enum ALSourcei
    {
        SourceRelative = 0x202,
        Buffer = 0x1009,
        EfxDirectFilter = 0x20005,
        EfxAuxilarySendFilter = 0x20006,
    }

    public enum ALSourcef
    {
        Pitch = 0x1003,
        Gain = 0x100A,
        ReferenceDistance = 0x1020
    }

    public enum ALGetSourcei
    {
        SampleOffset = 0x1025,
        SourceState = 0x1010,
        BuffersQueued = 0x1015,
        BuffersProcessed = 0x1016,
    }

    public enum ALSourceState
    {
        Initial = 0x1011,
        Playing = 0x1012,
        Paused = 0x1013,
        Stopped = 0x1014,
    }

    public enum ALListener3f
    {
        Position = 0x1004,
    }

    public enum ALSource3f
    {
        Position = 0x1004,
        Velocity = 0x1006,
    }

    public enum ALDistanceModel
    {
        None = 0,
        InverseDistanceClamped = 0xD002,
    }

    public enum AlcError
    {
        NoError = 0,
    }

    public enum AlcGetString
    {
        CaptureDeviceSpecifier = 0x0310,
        CaptureDefaultDeviceSpecifier = 0x0311,
        Extensions = 0x1006,
    }

    public enum AlcGetInteger
    {
        CaptureSamples = 0x0312,
    }

    public enum EfxFilteri
    {
        FilterType = 0x8001,
    }

    public enum EfxFilterf
    {
        LowpassGain = 0x0001,
        LowpassGainHF = 0x0002,
        HighpassGain = 0x0001,
        HighpassGainLF = 0x0002,
        BandpassGain = 0x0001,
        BandpassGainLF = 0x0002,
        BandpassGainHF = 0x0003,
    }

    public enum EfxFilterType
    {
        None = 0x0000,
        Lowpass = 0x0001,
        Highpass = 0x0002,
        Bandpass = 0x0003,
    }

    public enum EfxEffecti
    {
        EffectType = 0x8001,
        SlotEffect = 0x0001,
    }

    public enum EfxEffectSlotf
    {
        EffectSlotGain = 0x0002,
    }

    public enum EfxEffectf
    {
        EaxReverbDensity = 0x0001,
        EaxReverbDiffusion = 0x0002,
        EaxReverbGain = 0x0003,
        EaxReverbGainHF = 0x0004,
        EaxReverbGainLF = 0x0005,
        DecayTime = 0x0006,
        DecayHighFrequencyRatio = 0x0007,
        DecayLowFrequencyRation = 0x0008,
        EaxReverbReflectionsGain = 0x0009,
        EaxReverbReflectionsDelay = 0x000A,
        ReflectionsPain = 0x000B,
        LateReverbGain = 0x000C,
        LateReverbDelay = 0x000D,
        LateRevertPain = 0x000E,
        EchoTime = 0x000F,
        EchoDepth = 0x0010,
        ModulationTime = 0x0011,
        ModulationDepth = 0x0012,
        AirAbsorbsionHighFrequency = 0x0013,
        EaxReverbHFReference = 0x0014,
        EaxReverbLFReference = 0x0015,
        RoomRolloffFactor = 0x0016,
        DecayHighFrequencyLimit = 0x0017,
    }

    public enum EfxEffectType
    {
        Reverb = 0x8000,
    }

    public class AL
    {
        public static IntPtr NativeLibrary = GetNativeLibrary();

        private static IntPtr GetNativeLibrary()
        {
#if DESKTOPGL
            if (CurrentPlatform.OS == OS.Windows)
                return FuncLoader.LoadLibraryExt("soft_oal.dll");
            if (CurrentPlatform.OS == OS.Linux)
                return FuncLoader.LoadLibraryExt("libopenal.so.1");
            if (CurrentPlatform.OS == OS.MacOSX)
                return FuncLoader.LoadLibraryExt("libopenal.1.dylib");
            return FuncLoader.LoadLibraryExt("openal");
#elif ANDROID
            var ret = FuncLoader.LoadLibrary("libopenal32.so");

            if (ret == IntPtr.Zero)
            {
                var appFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var appDir = Path.GetDirectoryName(appFilesDir);
                var lib = Path.Combine(appDir, "lib", "libopenal32.so");

                ret = FuncLoader.LoadLibrary(lib);
            }

            return ret;
#else
            return FuncLoader.LoadLibrary("/System/Library/Frameworks/OpenAL.framework/OpenAL");
#endif
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alenable(int cap);
        public static d_alenable Enable = FuncLoader.LoadFunction<d_alenable>(NativeLibrary, "alEnable");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_albufferdata(uint bid, int format, IntPtr data, int size, int freq);
        public static d_albufferdata alBufferData = FuncLoader.LoadFunction<d_albufferdata>(NativeLibrary, "alBufferData");
        public static void BufferData(int bid, ALFormat format, byte[] data, int size, int freq)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                alBufferData((uint)bid, (int)format, handle.AddrOfPinnedObject(), size, freq);
            }
            finally
            {
                handle.Free();
            }
        }
        public static void BufferData(int bid, ALFormat format, short[] data, int size, int freq)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                alBufferData((uint)bid, (int)format, handle.AddrOfPinnedObject(), size, freq);
            }
            finally
            {
                handle.Free();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void d_aldeletebuffers(int n, int* buffers);
        public static d_aldeletebuffers alDeleteBuffers = FuncLoader.LoadFunction<d_aldeletebuffers>(NativeLibrary, "alDeleteBuffers");

        public static void DeleteBuffers(int[] buffers)
        {
            DeleteBuffers(buffers.Length, ref buffers[0]);
        }

        public unsafe static void DeleteBuffers(int n, ref int buffers)
        {
            fixed (int* pbuffers = &buffers)
            {
                alDeleteBuffers(n, pbuffers);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_albufferi(int buffer, ALBufferi param, int value);
        public static d_albufferi Bufferi = FuncLoader.LoadFunction<d_albufferi>(NativeLibrary, "alBufferi");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_algetbufferi(int bid, ALGetBufferi param, out int value);
        public static d_algetbufferi GetBufferi = FuncLoader.LoadFunction<d_algetbufferi>(NativeLibrary, "alGetBufferi");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void d_albufferiv(int bid, ALBufferi param, int* values);
        public static d_albufferiv Bufferiv = FuncLoader.LoadFunction<d_albufferiv>(NativeLibrary, "alBufferiv");
        public static void GetBuffer(int bid, ALGetBufferi param, out int value)
        {
            GetBufferi(bid, param, out value);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void d_algenbuffers(int count, int* buffers);
        public static d_algenbuffers alGenBuffers = FuncLoader.LoadFunction<d_algenbuffers>(NativeLibrary, "alGenBuffers");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void* d_alGetBufferPtrSOFT(int bid);
        public static d_alGetBufferPtrSOFT GetBufferPtrSOFT = FuncLoader.LoadFunction<d_alGetBufferPtrSOFT>(NativeLibrary, "alGetBufferPtrSOFT", true);
        public unsafe static void GenBuffers(int count, out int[] buffers)
        {
            buffers = new int[count];
            fixed (int* ptr = &buffers[0])
            {
                alGenBuffers(count, ptr);
            }
        }

        public static void GenBuffers(int count, out int buffer)
        {
            GenBuffers(count, out int[] ret);
            buffer = ret[0];
        }

        public static int[] GenBuffers(int count)
        {
            GenBuffers(count, out int[] ret);
            return ret;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_algensources(int n, uint[] sources);
        public static d_algensources alGenSources = FuncLoader.LoadFunction<d_algensources>(NativeLibrary, "alGenSources");


        public static void GenSources(int[] sources)
        {
            uint[] temp = new uint[sources.Length];
            alGenSources(temp.Length, temp);
            for (int i = 0; i < temp.Length; i++)
            {
                sources[i] = (int)temp[i];
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ALError d_algeterror();
        public static d_algeterror GetError = FuncLoader.LoadFunction<d_algeterror>(NativeLibrary, "alGetError");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_alisbuffer(uint buffer);
        public static d_alisbuffer alIsBuffer = FuncLoader.LoadFunction<d_alisbuffer>(NativeLibrary, "alIsBuffer");

        public static bool IsBuffer(int buffer)
        {
            return alIsBuffer((uint)buffer);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourcepause(uint source);
        public static d_alsourcepause alSourcePause = FuncLoader.LoadFunction<d_alsourcepause>(NativeLibrary, "alSourcePause");

        public static void SourcePause(int source)
        {
            alSourcePause((uint)source);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourceplay(uint source);
        public static d_alsourceplay alSourcePlay = FuncLoader.LoadFunction<d_alsourceplay>(NativeLibrary, "alSourcePlay");

        public static void SourcePlay(int source)
        {
            alSourcePlay((uint)source);
        }

        public static string GetErrorString(ALError errorCode)
        {
            return errorCode.ToString();
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_alissource(int source);
        public static d_alissource IsSource = FuncLoader.LoadFunction<d_alissource>(NativeLibrary, "alIsSource");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_aldeletesources(int n, ref int sources);
        public static d_aldeletesources alDeleteSources = FuncLoader.LoadFunction<d_aldeletesources>(NativeLibrary, "alDeleteSources");

        public static void DeleteSource(int source)
        {
            alDeleteSources(1, ref source);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourcestop(int sourceId);
        public static d_alsourcestop SourceStop = FuncLoader.LoadFunction<d_alsourcestop>(NativeLibrary, "alSourceStop");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourcei(int sourceId, int i, int a);
        public static d_alsourcei alSourcei = FuncLoader.LoadFunction<d_alsourcei>(NativeLibrary, "alSourcei");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsource3i(int sourceId, ALSourcei i, int a, int b, int c);
        public static d_alsource3i alSource3i = FuncLoader.LoadFunction<d_alsource3i>(NativeLibrary, "alSource3i");

        public static void Source(int sourceId, ALSourcei i, int a)
        {
            alSourcei(sourceId, (int)i, a);
        }

        public static void Source(int sourceId, ALSourceb i, bool a)
        {
            alSourcei(sourceId, (int)i, a ? 1 : 0);
        }

        public static void Source(int sourceId, ALSource3f i, float x, float y, float z)
        {
            alSource3f(sourceId, i, x, y, z);
        }

        public static void Source(int sourceId, ALSourcef i, float dist)
        {
            alSourcef(sourceId, i, dist);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourcef(int sourceId, ALSourcef i, float a);
        public static d_alsourcef alSourcef = FuncLoader.LoadFunction<d_alsourcef>(NativeLibrary, "alSourcef");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsource3f(int sourceId, ALSource3f i, float x, float y, float z);
        public static d_alsource3f alSource3f = FuncLoader.LoadFunction<d_alsource3f>(NativeLibrary, "alSource3f");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_algetsourcei(int sourceId, ALGetSourcei i, out int state);
        public static d_algetsourcei GetSource = FuncLoader.LoadFunction<d_algetsourcei>(NativeLibrary, "alGetSourcei");

        public static ALSourceState GetSourceState(int sourceId)
        {
            GetSource(sourceId, ALGetSourcei.SourceState, out int state);
            return (ALSourceState)state;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_algetlistener3f(ALListener3f param, out float value1, out float value2, out float value3);
        public static d_algetlistener3f GetListener = FuncLoader.LoadFunction<d_algetlistener3f>(NativeLibrary, "alGetListener3f");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_aldistancemodel(ALDistanceModel model);
        public static d_aldistancemodel DistanceModel = FuncLoader.LoadFunction<d_aldistancemodel>(NativeLibrary, "alDistanceModel");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_aldopplerfactor(float value);
        public static d_aldopplerfactor DopplerFactor = FuncLoader.LoadFunction<d_aldopplerfactor>(NativeLibrary, "alDopplerFactor");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void d_alsourcequeuebuffers(int sourceId, int numEntries, int* buffers);
        public static d_alsourcequeuebuffers alSourceQueueBuffers = FuncLoader.LoadFunction<d_alsourcequeuebuffers>(NativeLibrary, "alSourceQueueBuffers");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void d_alsourceunqueuebuffers(int sourceId, int numEntries, int* salvaged);
        public static d_alsourceunqueuebuffers alSourceUnqueueBuffers = FuncLoader.LoadFunction<d_alsourceunqueuebuffers>(NativeLibrary, "alSourceUnqueueBuffers");

        public static unsafe void SourceQueueBuffers(int sourceId, int numEntries, int[] buffers)
        {
            fixed (int* ptr = &buffers[0])
            {
                alSourceQueueBuffers(sourceId, numEntries, ptr);
            }
        }

        public unsafe static void SourceQueueBuffer(int sourceId, int buffer)
        {
            alSourceQueueBuffers(sourceId, 1, &buffer);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alsourceunqueuebuffers2(int sid, int numEntries, out int[] bids);
        public static d_alsourceunqueuebuffers2 alSourceUnqueueBuffers2 = FuncLoader.LoadFunction<d_alsourceunqueuebuffers2>(NativeLibrary, "alSourceUnqueueBuffers");

        public static unsafe int[] SourceUnqueueBuffers(int sourceId, int numEntries)
        {
            if (numEntries <= 0)
            {
                throw new ArgumentOutOfRangeException("numEntries", "Must be greater than zero.");
            }
            int[] array = new int[numEntries];
            fixed (int* ptr = &array[0])
            {
                alSourceUnqueueBuffers(sourceId, numEntries, ptr);
            }
            return array;
        }

        public static void SourceUnqueueBuffers(int sid, int numENtries, out int[] bids)
        {
            alSourceUnqueueBuffers2(sid, numENtries, out bids);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int d_algetenumvalue(string enumName);
        public static d_algetenumvalue alGetEnumValue = FuncLoader.LoadFunction<d_algetenumvalue>(NativeLibrary, "alGetEnumValue");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_alisextensionpresent(string extensionName);
        public static d_alisextensionpresent IsExtensionPresent = FuncLoader.LoadFunction<d_alisextensionpresent>(NativeLibrary, "alIsExtensionPresent");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_algetprocaddress(string functionName);
        public static d_algetprocaddress alGetProcAddress = FuncLoader.LoadFunction<d_algetprocaddress>(NativeLibrary, "alGetProcAddress");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr d_algetstring(int p);
        private static d_algetstring alGetString = FuncLoader.LoadFunction<d_algetstring>(NativeLibrary, "alGetString");

        public static string GetString(int p)
        {
            return Marshal.PtrToStringAnsi(alGetString(p));
        }

        public static string Get(ALGetString p)
        {
            return GetString((int)p);
        }
    }

    public partial class Alc
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alccreatecontext(IntPtr device, int[] attributes);
        public static d_alccreatecontext CreateContext = FuncLoader.LoadFunction<d_alccreatecontext>(AL.NativeLibrary, "alcCreateContext");

        public static AlcError GetError()
        {
            return GetErrorForDevice(IntPtr.Zero);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate AlcError d_alcgeterror(IntPtr device);
        public static d_alcgeterror GetErrorForDevice = FuncLoader.LoadFunction<d_alcgeterror>(AL.NativeLibrary, "alcGetError");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcgetintegerv(IntPtr device, int param, int size, int[] values);
        public static d_alcgetintegerv alcGetIntegerv = FuncLoader.LoadFunction<d_alcgetintegerv>(AL.NativeLibrary, "alcGetIntegerv");

        public static void GetInteger(IntPtr device, AlcGetInteger param, int size, int[] values)
        {
            alcGetIntegerv(device, (int)param, size, values);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alcgetcurrentcontext();
        public static d_alcgetcurrentcontext GetCurrentContext = FuncLoader.LoadFunction<d_alcgetcurrentcontext>(AL.NativeLibrary, "alcGetCurrentContext");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcmakecontextcurrent(IntPtr context);
        public static d_alcmakecontextcurrent MakeContextCurrent = FuncLoader.LoadFunction<d_alcmakecontextcurrent>(AL.NativeLibrary, "alcMakeContextCurrent");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcdestroycontext(IntPtr context);
        public static d_alcdestroycontext DestroyContext = FuncLoader.LoadFunction<d_alcdestroycontext>(AL.NativeLibrary, "alcDestroyContext");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcclosedevice(IntPtr device);
        public static d_alcclosedevice CloseDevice = FuncLoader.LoadFunction<d_alcclosedevice>(AL.NativeLibrary, "alcCloseDevice");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alcopendevice(string device);
        public static d_alcopendevice OpenDevice = FuncLoader.LoadFunction<d_alcopendevice>(AL.NativeLibrary, "alcOpenDevice");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alccaptureopendevice(string device, uint sampleRate, int format, int sampleSize);
        public static d_alccaptureopendevice alcCaptureOpenDevice = FuncLoader.LoadFunction<d_alccaptureopendevice>(AL.NativeLibrary, "alcCaptureOpenDevice");

        public static IntPtr CaptureOpenDevice(string device, uint sampleRate, ALFormat format, int sampleSize)
        {
            return alcCaptureOpenDevice(device, sampleRate, (int)format, sampleSize);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alccapturestart(IntPtr device);
        public static d_alccapturestart CaptureStart = FuncLoader.LoadFunction<d_alccapturestart>(AL.NativeLibrary, "alcCaptureStart");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alccapturesamples(IntPtr device, IntPtr buffer, int samples);
        public static d_alccapturesamples CaptureSamples = FuncLoader.LoadFunction<d_alccapturesamples>(AL.NativeLibrary, "alcCaptureSamples");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alccapturestop(IntPtr device);
        public static d_alccapturestop CaptureStop = FuncLoader.LoadFunction<d_alccapturestop>(AL.NativeLibrary, "alcCaptureStop");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alccaptureclosedevice(IntPtr device);
        public static d_alccaptureclosedevice CaptureCloseDevice = FuncLoader.LoadFunction<d_alccaptureclosedevice>(AL.NativeLibrary, "alcCaptureCloseDevice");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_alcisextensionpresent(IntPtr device, string extensionName);
        public static d_alcisextensionpresent IsExtensionPresent = FuncLoader.LoadFunction<d_alcisextensionpresent>(AL.NativeLibrary, "alcIsExtensionPresent");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_alcgetstring(IntPtr device, int p);
        public static d_alcgetstring alcGetString = FuncLoader.LoadFunction<d_alcgetstring>(AL.NativeLibrary, "alcGetString");

        public static string GetString(IntPtr device, int p)
        {
            return Marshal.PtrToStringAnsi(alcGetString(device, p));
        }

        public static string GetString(IntPtr device, AlcGetString p)
        {
            return GetString(device, (int)p);
        }

#if IOS
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcsuspendcontext(IntPtr context);
        public static d_alcsuspendcontext SuspendContext = FuncLoader.LoadFunction<d_alcsuspendcontext>(AL.NativeLibrary, "alcSuspendContext");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcprocesscontext(IntPtr context);
        public static d_alcprocesscontext ProcessContext = FuncLoader.LoadFunction<d_alcprocesscontext>(AL.NativeLibrary, "alcProcessContext");
#endif

#if ANDROID
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcdevicepausesoft(IntPtr device);
        public static d_alcdevicepausesoft DevicePause = FuncLoader.LoadFunction<d_alcdevicepausesoft>(AL.NativeLibrary, "alcDevicePauseSOFT");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_alcdeviceresumesoft(IntPtr device);
        public static d_alcdeviceresumesoft DeviceResume = FuncLoader.LoadFunction<d_alcdeviceresumesoft>(AL.NativeLibrary, "alcDeviceResumeSOFT");
#endif
    }

    public class XRamExtension
    {
        public enum XRamStorage
        {
            Automatic,
            Hardware,
            Accessible
        }

        private int RamSize;
        private int RamFree;
        private int StorageAuto;
        private int StorageHardware;
        private int StorageAccessible;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SetBufferModeDelegate(int n, ref int buffers, int value);

        private SetBufferModeDelegate setBufferMode;

        public XRamExtension()
        {
            IsInitialized = false;
            if (!AL.IsExtensionPresent("EAX-RAM"))
            {
                return;
            }
            RamSize = AL.alGetEnumValue("AL_EAX_RAM_SIZE");
            RamFree = AL.alGetEnumValue("AL_EAX_RAM_FREE");
            StorageAuto = AL.alGetEnumValue("AL_STORAGE_AUTOMATIC");
            StorageHardware = AL.alGetEnumValue("AL_STORAGE_HARDWARE");
            StorageAccessible = AL.alGetEnumValue("AL_STORAGE_ACCESSIBLE");
            if (RamSize == 0 || RamFree == 0 || StorageAuto == 0 || StorageHardware == 0 || StorageAccessible == 0)
            {
                return;
            }
            try
            {
                setBufferMode = (SetBufferModeDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("EAXSetBufferMode"), typeof(SetBufferModeDelegate));
            }
            catch (Exception)
            {
                return;
            }
            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; }

        public bool SetBufferMode(int i, ref int id, XRamStorage storage)
        {
            if (storage == XRamStorage.Accessible)
            {
                return setBufferMode(i, ref id, StorageAccessible);
            }
            if (storage != XRamStorage.Hardware)
            {
                return setBufferMode(i, ref id, StorageAuto);
            }
            return setBufferMode(i, ref id, StorageHardware);
        }
    }

    public class EffectsExtension
    {
        /* Effect API */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alGenEffectsDelegate(int n, out uint effect);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alDeleteEffectsDelegate(int n, ref int effect);
        //[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
        //private delegate bool alIsEffectDelegate (uint effect);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alEffectfDelegate(uint effect, EfxEffectf param, float value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alEffectiDelegate(uint effect, EfxEffecti param, int value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alGenAuxiliaryEffectSlotsDelegate(int n, out uint effectslots);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alDeleteAuxiliaryEffectSlotsDelegate(int n, ref int effectslots);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alAuxiliaryEffectSlotiDelegate(uint slot, EfxEffecti type, uint effect);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alAuxiliaryEffectSlotfDelegate(uint slot, EfxEffectSlotf param, float value);

        /* Filter API */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void alGenFiltersDelegate(int n, [Out] uint* filters);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alFilteriDelegate(uint fid, EfxFilteri param, int value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void alFilterfDelegate(uint fid, EfxFilterf param, float value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void alDeleteFiltersDelegate(int n, [In] uint* filters);


        private alGenEffectsDelegate alGenEffects;
        private alDeleteEffectsDelegate alDeleteEffects;
        //private alIsEffectDelegate alIsEffect;
        private alEffectfDelegate alEffectf;
        private alEffectiDelegate alEffecti;
        private alGenAuxiliaryEffectSlotsDelegate alGenAuxiliaryEffectSlots;
        private alDeleteAuxiliaryEffectSlotsDelegate alDeleteAuxiliaryEffectSlots;
        private alAuxiliaryEffectSlotiDelegate alAuxiliaryEffectSloti;
        private alAuxiliaryEffectSlotfDelegate alAuxiliaryEffectSlotf;
        private alGenFiltersDelegate alGenFilters;
        private alFilteriDelegate alFilteri;
        private alFilterfDelegate alFilterf;
        private alDeleteFiltersDelegate alDeleteFilters;

        public static IntPtr device;
        static EffectsExtension _instance;
        public static EffectsExtension Instance
        {
            get
            {
                _instance ??= new EffectsExtension();
                return _instance;
            }
        }

        public EffectsExtension()
        {
            IsInitialized = false;
            if (!Alc.IsExtensionPresent(device, "ALC_EXT_EFX"))
            {
                return;
            }

            alGenEffects = (alGenEffectsDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alGenEffects"), typeof(alGenEffectsDelegate));
            alDeleteEffects = (alDeleteEffectsDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alDeleteEffects"), typeof(alDeleteEffectsDelegate));
            alEffectf = (alEffectfDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alEffectf"), typeof(alEffectfDelegate));
            alEffecti = (alEffectiDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alEffecti"), typeof(alEffectiDelegate));
            alGenAuxiliaryEffectSlots = (alGenAuxiliaryEffectSlotsDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alGenAuxiliaryEffectSlots"), typeof(alGenAuxiliaryEffectSlotsDelegate));
            alDeleteAuxiliaryEffectSlots = (alDeleteAuxiliaryEffectSlotsDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alDeleteAuxiliaryEffectSlots"), typeof(alDeleteAuxiliaryEffectSlotsDelegate));
            alAuxiliaryEffectSloti = (alAuxiliaryEffectSlotiDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alAuxiliaryEffectSloti"), typeof(alAuxiliaryEffectSlotiDelegate));
            alAuxiliaryEffectSlotf = (alAuxiliaryEffectSlotfDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alAuxiliaryEffectSlotf"), typeof(alAuxiliaryEffectSlotfDelegate));

            alGenFilters = (alGenFiltersDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alGenFilters"), typeof(alGenFiltersDelegate));
            alFilteri = (alFilteriDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alFilteri"), typeof(alFilteriDelegate));
            alFilterf = (alFilterfDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alFilterf"), typeof(alFilterfDelegate));
            alDeleteFilters = (alDeleteFiltersDelegate)Marshal.GetDelegateForFunctionPointer(AL.alGetProcAddress("alDeleteFilters"), typeof(alDeleteFiltersDelegate));

            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; }

        /*

alEffecti (effect, EfxEffecti.FilterType, (int)EfxEffectType.Reverb);
            ALHelper.CheckError ("Failed to set Filter Type.");

        */

        public void GenAuxiliaryEffectSlots(int count, out uint slot)
        {
            alGenAuxiliaryEffectSlots(count, out slot);
            ALHelper.CheckError("Failed to Genereate Aux slot");
        }

        public void GenEffect(out uint effect)
        {
            alGenEffects(1, out effect);
            ALHelper.CheckError("Failed to Generate Effect.");
        }

        public void DeleteAuxiliaryEffectSlot(int slot)
        {
            alDeleteAuxiliaryEffectSlots(1, ref slot);
        }

        public void DeleteEffect(int effect)
        {
            alDeleteEffects(1, ref effect);
        }

        public void BindEffectToAuxiliarySlot(uint slot, uint effect)
        {
            alAuxiliaryEffectSloti(slot, EfxEffecti.SlotEffect, effect);
            ALHelper.CheckError("Failed to bind Effect");
        }

        public void AuxiliaryEffectSlot(uint slot, EfxEffectSlotf param, float value)
        {
            alAuxiliaryEffectSlotf(slot, param, value);
            ALHelper.CheckError("Failes to set " + param + " " + value);
        }

        public void BindSourceToAuxiliarySlot(int SourceId, int slot, int slotnumber, int filter)
        {
            AL.alSource3i(SourceId, ALSourcei.EfxAuxilarySendFilter, slot, slotnumber, filter);
        }

        public void Effect(uint effect, EfxEffectf param, float value)
        {
            alEffectf(effect, param, value);
            ALHelper.CheckError("Failed to set " + param + " " + value);
        }

        public void Effect(uint effect, EfxEffecti param, int value)
        {
            alEffecti(effect, param, value);
            ALHelper.CheckError("Failed to set " + param + " " + value);
        }

        public unsafe int GenFilter()
        {
            uint filter = 0;
            alGenFilters(1, &filter);
            return (int)filter;
        }
        public void Filter(int sourceId, EfxFilteri filter, int EfxFilterType)
        {
            alFilteri((uint)sourceId, filter, EfxFilterType);
        }
        public void Filter(int sourceId, EfxFilterf filter, float EfxFilterType)
        {
            alFilterf((uint)sourceId, filter, EfxFilterType);
        }
        public void BindFilterToSource(int sourceId, int filterId)
        {
            AL.Source(sourceId, ALSourcei.EfxDirectFilter, filterId);
        }
        public unsafe void DeleteFilter(int filterId)
        {
            alDeleteFilters(1, (uint*)&filterId);
        }
    }
}
