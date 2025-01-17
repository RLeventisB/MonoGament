﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#if __IOS__ || __TVOS__ || MONOMAC
using ObjCRuntime;
#endif

namespace MonoGame.OpenGL
{
    public enum BufferAccess
    {
        ReadOnly = 0x88B8,
    }

    public enum BufferUsageHint
    {
        StreamDraw = 0x88E0,
        StaticDraw = 0x88E4,
    }

    public enum StencilFace
    {
        Front = 0x0404,
        Back = 0x0405,
    }
    public enum DrawBuffersEnum
    {
        UnsignedShort,
        UnsignedInt,
    }

    public enum ShaderType
    {
        VertexShader = 0x8B31,
        FragmentShader = 0x8B30,
    }

    public enum ShaderParameter
    {
        LogLength = 0x8B84,
        CompileStatus = 0x8B81,
        SourceLength = 0x8B88,
    }

    public enum GetProgramParameterName
    {
        LogLength = 0x8B84,
        LinkStatus = 0x8B82,
    }

    public enum DrawElementsType
    {
        UnsignedShort = 0x1403,
        UnsignedInt = 0x1405,
    }

    public enum QueryTarget
    {
        SamplesPassed = 0x8914,
        SamplesPassedExt = 0x8C2F,
    }

    public enum GetQueryObjectParam
    {
        QueryResultAvailable = 0x8867,
        QueryResult = 0x8866,
    }

    public enum GenerateMipmapTarget
    {
        Texture1D = 0x0DE0,
        Texture2D = 0x0DE1,
        Texture3D = 0x806F,
        TextureCubeMap = 0x8513,
        Texture1DArray = 0x8C18,
        Texture2DArray = 0x8C1A,
        Texture2DMultisample = 0x9100,
        Texture2DMultisampleArray = 0x9102,
    }

    public enum BlitFramebufferFilter
    {
        Nearest = 0x2600,
    }

    public enum ReadBufferMode
    {
        ColorAttachment0 = 0x8CE0,
    }

    public enum DrawBufferMode
    {
        ColorAttachment0 = 0x8CE0,
    }

    public enum FramebufferErrorCode
    {
        FramebufferUndefined = 0x8219,
        FramebufferComplete = 0x8CD5,
        FramebufferCompleteExt = 0x8CD5,
        FramebufferIncompleteAttachment = 0x8CD6,
        FramebufferIncompleteAttachmentExt = 0x8CD6,
        FramebufferIncompleteMissingAttachment = 0x8CD7,
        FramebufferIncompleteMissingAttachmentExt = 0x8CD7,
        FramebufferIncompleteDimensionsExt = 0x8CD9,
        FramebufferIncompleteFormatsExt = 0x8CDA,
        FramebufferIncompleteDrawBuffer = 0x8CDB,
        FramebufferIncompleteDrawBufferExt = 0x8CDB,
        FramebufferIncompleteReadBuffer = 0x8CDC,
        FramebufferIncompleteReadBufferExt = 0x8CDC,
        FramebufferUnsupported = 0x8CDD,
        FramebufferUnsupportedExt = 0x8CDD,
        FramebufferIncompleteMultisample = 0x8D56,
        FramebufferIncompleteLayerTargets = 0x8DA8,
        FramebufferIncompleteLayerCount = 0x8DA9,
    }

    public enum BufferTarget
    {
        ArrayBuffer = 0x8892,
        ElementArrayBuffer = 0x8893,
    }

    public enum RenderbufferTarget
    {
        Renderbuffer = 0x8D41,
        RenderbufferExt = 0x8D41,
    }

    public enum FramebufferTarget
    {
        Framebuffer = 0x8D40,
        FramebufferExt = 0x8D40,
        ReadFramebuffer = 0x8CA8,
    }

    public enum RenderbufferStorage
    {
        Rgba8 = 0x8058,
        DepthComponent16 = 0x81a5,
        DepthComponent24 = 0x81a6,
        Depth24Stencil8 = 0x88F0,
        // GLES Values
        DepthComponent24Oes = 0x81A6,
        Depth24Stencil8Oes = 0x88F0,
        StencilIndex8 = 0x8D48,
    }

    public enum EnableCap : int
    {
        PointSmooth = 0x0B10,
        LineSmooth = 0x0B20,
        CullFace = 0x0B44,
        Lighting = 0x0B50,
        ColorMaterial = 0x0B57,
        Fog = 0x0B60,
        DepthTest = 0x0B71,
        StencilTest = 0x0B90,
        Normalize = 0x0BA1,
        AlphaTest = 0x0BC0,
        Dither = 0x0BD0,
        Blend = 0x0BE2,
        ColorLogicOp = 0x0BF2,
        ScissorTest = 0x0C11,
        Texture2D = 0x0DE1,
        PolygonOffsetFill = 0x8037,
        RescaleNormal = 0x803A,
        VertexArray = 0x8074,
        NormalArray = 0x8075,
        ColorArray = 0x8076,
        TextureCoordArray = 0x8078,
        Multisample = 0x809D,
        SampleAlphaToCoverage = 0x809E,
        SampleAlphaToOne = 0x809F,
        SampleCoverage = 0x80A0,
        DebugOutputSynchronous = 0x8242,
        DebugOutput = 0x92E0,
    }

    public enum VertexPointerType
    {
        Float = 0x1406,
        Short = 0x1402,
    }

    public enum VertexAttribPointerType
    {
        Float = 0x1406,
        Short = 0x1402,
        UnsignedByte = 0x1401,
        HalfFloat = 0x140B,
    }

    public enum CullFaceMode
    {
        Back = 0x0405,
        Front = 0x0404,
    }

    public enum FrontFaceDirection
    {
        Cw = 0x0900,
        Ccw = 0x0901,
    }

    public enum MaterialFace
    {
        FrontAndBack = 0x0408,
    }

    public enum PolygonMode
    {
        Fill = 0x1B02,
        Line = 0x1B01,
    }

    public enum ColorPointerType
    {
        Float = 0x1406,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        UnsignedByte = 0x1401,
        HalfFloat = 0x140B,
    }

    public enum NormalPointerType
    {
        Byte = 0x1400,
        Float = 0x1406,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        UnsignedByte = 0x1401,
        HalfFloat = 0x140B,
    }

    public enum TexCoordPointerType
    {
        Byte = 0x1400,
        Float = 0x1406,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        UnsignedByte = 0x1401,
        HalfFloat = 0x140B,
    }

    public enum BlendEquationMode
    {
        FuncAdd = 0x8006,
        Max = 0x8008, // ios MaxExt
        Min = 0x8007, // ios MinExt
        FuncReverseSubtract = 0x800B,
        FuncSubtract = 0x800A,
    }

    public enum BlendingFactorSrc
    {
        Zero = 0,
        SrcColor = 0x0300,
        OneMinusSrcColor = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        DstColor = 0x0306,
        OneMinusDstColor = 0x0307,
        SrcAlphaSaturate = 0x0308,
        ConstantColor = 0x8001,
        OneMinusConstantColor = 0x8002,
        ConstantAlpha = 0x8003,
        OneMinusConstantAlpha = 0x8004,
        One = 1,
    }

    public enum BlendingFactorDest
    {
        Zero = 0,
        SrcColor = 0x0300,
        OneMinusSrcColor = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        DstColor = 0X0306,
        OneMinusDstColor = 0x0307,
        SrcAlphaSaturate = 0x0308,
        ConstantColor = 0x8001,
        OneMinusConstantColor = 0x8002,
        ConstantAlpha = 0x8003,
        OneMinusConstantAlpha = 0x8004,
        One = 1,
    }

    public enum DepthFunction
    {
        Always = 0x0207,
        Equal = 0x0202,
        Greater = 0x0204,
        Gequal = 0x0206,
        Less = 0x0201,
        Lequal = 0x0203,
        Never = 0x0200,
        Notequal = 0x0205,
    }

    public enum GetPName : int
    {
        ArrayBufferBinding = 0x8894,
        MaxTextureImageUnits = 0x8872,
        MaxCombinedTextureImageUnits = 0x8B4D,
        MaxVertexAttribs = 0x8869,
        MaxTextureSize = 0x0D33,
        MaxDrawBuffers = 0x8824,
        TextureBinding2D = 0x8069,
        MaxTextureMaxAnisotropyExt = 0x84FF,
        MaxSamples = 0x8D57,
    }

    public enum StringName
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
        Extensions = 0x1F03,
    }

    public enum FramebufferAttachment
    {
        ColorAttachment0 = 0x8CE0,
        ColorAttachment0Ext = 0x8CE0,
        DepthAttachment = 0x8D00,
        StencilAttachment = 0x8D20,
        ColorAttachmentExt = 0x1800,
        DepthAttachementExt = 0x1801,
        StencilAttachmentExt = 0x1802,
    }

    public enum GLPrimitiveType
    {
        Points = 0x0000,
        Lines = 0x0001,
        LineStrip = 0x0003,
        Triangles = 0x0004,
        TriangleStrip = 0x0005,
    }

    [Flags]
    public enum ClearBufferMask
    {
        DepthBufferBit = 0x00000100,
        StencilBufferBit = 0x00000400,
        ColorBufferBit = 0x00004000,
    }

    public enum ErrorCode
    {
        NoError = 0,
    }

    public enum TextureUnit
    {
        Texture0 = 0x84C0,
    }

    public enum TextureTarget
    {
        Texture2D = 0x0DE1,
        Texture3D = 0x806F,
        TextureCubeMap = 0x8513,
        TextureCubeMapPositiveX = 0x8515,
        TextureCubeMapPositiveY = 0x8517,
        TextureCubeMapPositiveZ = 0x8519,
        TextureCubeMapNegativeX = 0x8516,
        TextureCubeMapNegativeY = 0x8518,
        TextureCubeMapNegativeZ = 0x851A,
    }

    public enum PixelInternalFormat
    {
        Rgba = 0x1908,
        Rgb = 0x1907,
        Rgba4 = 0x8056,
        Luminance = 0x1909,
        CompressedRgbS3tcDxt1Ext = 0x83F0,
        CompressedSrgbS3tcDxt1Ext = 0x8C4C,
        CompressedRgbaS3tcDxt1Ext = 0x83F1,
        CompressedRgbaS3tcDxt3Ext = 0x83F2,
        CompressedSrgbAlphaS3tcDxt3Ext = 0x8C4E,
        CompressedRgbaS3tcDxt5Ext = 0x83F3,
        CompressedSrgbAlphaS3tcDxt5Ext = 0x8C4F,
        R32f = 0x822E,
        Rg16f = 0x822F,
        Rgba16f = 0x881A,
        R16f = 0x822D,
        Rg32f = 0x8230,
        Rgba32f = 0x8814,
        Rg8i = 0x8237,
        Rgba8i = 0x8D8E,
        Rg16ui = 0x823A,
        Rgba16ui = 0x8D76,
        Rgb10A2ui = 0x906F,
        Rgba16 = 0x805B,
        // PVRTC
        CompressedRgbPvrtc2Bppv1Img = 0x8C01,
        CompressedRgbPvrtc4Bppv1Img = 0x8C00,
        CompressedRgbaPvrtc2Bppv1Img = 0x8C03,
        CompressedRgbaPvrtc4Bppv1Img = 0x8C02,
        // ATITC
        AtcRgbaExplicitAlphaAmd = 0x8C93,
        AtcRgbaInterpolatedAlphaAmd = 0x87EE,
        // ETC1
        Etc1 = 0x8D64,
        Srgb = 0x8C40,

        // ETC2 RGB8A1
        Etc2Rgb8 = 0x9274,
        Etc2Srgb8 = 0x9275,
        Etc2Rgb8A1 = 0x9276,
        Etc2Srgb8A1 = 0x9277,
        Etc2Rgba8Eac = 0x9278,
        Etc2SRgb8A8Eac = 0x9279,
    }

    public enum PixelFormat
    {
        Rgba = 0x1908,
        Rgb = 0x1907,
        Luminance = 0x1909,
        CompressedTextureFormats = 0x86A3,
        Red = 0x1903,
        Rg = 0x8227,
    }

    public enum PixelType
    {
        UnsignedByte = 0x1401,
        UnsignedShort565 = 0x8363,
        UnsignedShort4444 = 0x8033,
        UnsignedShort5551 = 0x8034,
        Float = 0x1406,
        HalfFloat = 0x140B,
        HalfFloatOES = 0x8D61,
        Byte = 0x1400,
        UnsignedShort = 0x1403,
        UnsignedInt1010102 = 0x8036,
    }

    public enum PixelStoreParameter
    {
        UnpackAlignment = 0x0CF5,
        PackAlignment = 0x0D05,
    }

    public enum GLStencilFunction
    {
        Always = 0x0207,
        Equal = 0x0202,
        Greater = 0x0204,
        Gequal = 0x0206,
        Less = 0x0201,
        Lequal = 0x0203,
        Never = 0x0200,
        Notequal = 0x0205,
    }

    public enum StencilOp
    {
        Keep = 0x1E00,
        DecrWrap = 0x8508,
        Decr = 0x1E03,
        Incr = 0x1E02,
        IncrWrap = 0x8507,
        Invert = 0x150A,
        Replace = 0x1E01,
        Zero = 0,
    }

    public enum TextureParameterName
    {
        TextureMaxAnisotropyExt = 0x84FE,
        TextureBaseLevel = 0x813C,
        TextureMaxLevel = 0x813D,
        TextureMinFilter = 0x2801,
        TextureMagFilter = 0x2800,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureBorderColor = 0x1004,
        TextureLodBias = 0x8501,
        TextureCompareMode = 0x884C,
        TextureCompareFunc = 0x884D,
        GenerateMipmap = 0x8191,
    }

    public enum Bool
    {
        True = 1,
        False = 0,
    }

    public enum TextureMinFilter
    {
        LinearMipmapNearest = 0x2701,
        NearestMipmapLinear = 0x2702,
        LinearMipmapLinear = 0x2703,
        Linear = 0x2601,
        NearestMipmapNearest = 0x2700,
        Nearest = 0x2600,
    }

    public enum TextureMagFilter
    {
        Linear = 0x2601,
        Nearest = 0x2600,
    }

    public enum TextureCompareMode
    {
        CompareRefToTexture = 0x884E,
        None = 0,
    }

    public enum TextureWrapMode
    {
        ClampToEdge = 0x812F,
        Repeat = 0x2901,
        MirroredRepeat = 0x8370,
        //GLES
        ClampToBorder = 0x812D,
    }

    public partial class ColorFormat
    {
        public ColorFormat(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int R { get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }
        public int A { get; private set; }
    }

    public partial class GL
    {
        public enum RenderApi
        {
            ES = 12448,
            GL = 12450,
        }

        public static RenderApi BoundApi = RenderApi.GL;
        private const CallingConvention callingConvention = CallingConvention.Winapi;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void EnableVertexAttribArrayDelegate(int attrib);
        public static EnableVertexAttribArrayDelegate EnableVertexAttribArray;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DisableVertexAttribArrayDelegate(int attrib);
        public static DisableVertexAttribArrayDelegate DisableVertexAttribArray;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void MakeCurrentDelegate(IntPtr window);
        public static MakeCurrentDelegate MakeCurrent;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void GetIntegerDelegate(int param, [Out] int* data);
        public static GetIntegerDelegate GetIntegerv;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate IntPtr GetStringDelegate(StringName param);
        public static GetStringDelegate GetStringInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ClearDepthDelegate(float depth);
        public static ClearDepthDelegate ClearDepth;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DepthRangedDelegate(double min, double max);
        public static DepthRangedDelegate DepthRanged;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DepthRangefDelegate(float min, float max);
        public static DepthRangefDelegate DepthRangef;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ClearDelegate(ClearBufferMask mask);
        public static ClearDelegate Clear;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ClearColorDelegate(float red, float green, float blue, float alpha);
        public static ClearColorDelegate ClearColor;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ClearStencilDelegate(int stencil);
        public static ClearStencilDelegate ClearStencil;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ViewportDelegate(int x, int y, int w, int h);
        public static ViewportDelegate Viewport;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate ErrorCode GetErrorDelegate();
        public static GetErrorDelegate GetError;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FlushDelegate();
        public static FlushDelegate Flush;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenTexturesDelegte(int count, [Out] out int id);
        public static GenTexturesDelegte GenTextures;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BindTextureDelegate(TextureTarget target, int id);
        public static BindTextureDelegate BindTexture;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int EnableDelegate(EnableCap cap);
        public static EnableDelegate Enable;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int DisableDelegate(EnableCap cap);
        public static DisableDelegate Disable;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void CullFaceDelegate(CullFaceMode mode);
        public static CullFaceDelegate CullFace;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FrontFaceDelegate(FrontFaceDirection direction);
        public static FrontFaceDelegate FrontFace;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void PolygonModeDelegate(MaterialFace face, PolygonMode mode);
        public static PolygonModeDelegate PolygonMode;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void PolygonOffsetDelegate(float slopeScaleDepthBias, float depthbias);
        public static PolygonOffsetDelegate PolygonOffset;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawBuffersDelegate(int count, DrawBuffersEnum[] buffers);
        public static DrawBuffersDelegate DrawBuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void UseProgramDelegate(int program);
        public static UseProgramDelegate UseProgram;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void Uniform4fvDelegate(int location, int size, float* values);
        public static Uniform4fvDelegate Uniform4fv;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void Uniform1iDelegate(int location, int value);
        public static Uniform1iDelegate Uniform1i;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ScissorDelegate(int x, int y, int width, int height);
        public static ScissorDelegate Scissor;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ReadPixelsDelegate(int x, int y, int width, int height, PixelFormat format, PixelType type, IntPtr data);
        public static ReadPixelsDelegate ReadPixelsInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BindBufferDelegate(BufferTarget target, int buffer);
        public static BindBufferDelegate BindBuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawElementsDelegate(GLPrimitiveType primitiveType, int count, DrawElementsType elementType, IntPtr offset);
        public static DrawElementsDelegate DrawElements;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawArraysDelegate(GLPrimitiveType primitiveType, int offset, int count);
        public static DrawArraysDelegate DrawArrays;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenRenderbuffersDelegate(int count, [Out] out int buffer);
        public static GenRenderbuffersDelegate GenRenderbuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BindRenderbufferDelegate(RenderbufferTarget target, int buffer);
        public static BindRenderbufferDelegate BindRenderbuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteRenderbuffersDelegate(int count, [In] [Out] ref int buffer);
        public static DeleteRenderbuffersDelegate DeleteRenderbuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void RenderbufferStorageMultisampleDelegate(RenderbufferTarget target, int sampleCount,
            RenderbufferStorage storage, int width, int height);
        public static RenderbufferStorageMultisampleDelegate RenderbufferStorageMultisample;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenFramebuffersDelegate(int count, out int buffer);
        public static GenFramebuffersDelegate GenFramebuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BindFramebufferDelegate(FramebufferTarget target, int buffer);
        public static BindFramebufferDelegate BindFramebuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteFramebuffersDelegate(int count, ref int buffer);
        public static DeleteFramebuffersDelegate DeleteFramebuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void InvalidateFramebufferDelegate(FramebufferTarget target, int numAttachments, FramebufferAttachment[] attachments);
        public static InvalidateFramebufferDelegate InvalidateFramebuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FramebufferTexture2DDelegate(FramebufferTarget target, FramebufferAttachment attachement,
            TextureTarget textureTarget, int texture, int level);
        public static FramebufferTexture2DDelegate FramebufferTexture2D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FramebufferTexture2DMultiSampleDelegate(FramebufferTarget target, FramebufferAttachment attachement,
            TextureTarget textureTarget, int texture, int level, int samples);
        public static FramebufferTexture2DMultiSampleDelegate FramebufferTexture2DMultiSample;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FramebufferRenderbufferDelegate(FramebufferTarget target, FramebufferAttachment attachement,
            RenderbufferTarget renderBufferTarget, int buffer);
        public static FramebufferRenderbufferDelegate FramebufferRenderbuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void RenderbufferStorageDelegate(RenderbufferTarget target, RenderbufferStorage storage, int width, int hegiht);
        public static RenderbufferStorageDelegate RenderbufferStorage;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenerateMipmapDelegate(GenerateMipmapTarget target);
        public static GenerateMipmapDelegate GenerateMipmap;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ReadBufferDelegate(ReadBufferMode buffer);
        public static ReadBufferDelegate ReadBuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawBufferDelegate(DrawBufferMode buffer);
        public static DrawBufferDelegate DrawBuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlitFramebufferDelegate(int srcX0,
            int srcY0,
            int srcX1,
            int srcY1,
            int dstX0,
            int dstY0,
            int dstX1,
            int dstY1,
            ClearBufferMask mask,
            BlitFramebufferFilter filter);
        public static BlitFramebufferDelegate BlitFramebuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate FramebufferErrorCode CheckFramebufferStatusDelegate(FramebufferTarget target);
        public static CheckFramebufferStatusDelegate CheckFramebufferStatus;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexParameterFloatDelegate(TextureTarget target, TextureParameterName name, float value);
        public static TexParameterFloatDelegate TexParameterf;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void TexParameterFloatArrayDelegate(TextureTarget target, TextureParameterName name, float* values);
        public static TexParameterFloatArrayDelegate TexParameterfv;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexParameterIntDelegate(TextureTarget target, TextureParameterName name, int value);
        public static TexParameterIntDelegate TexParameteri;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenQueriesDelegate(int count, [Out] out int queryId);
        public static GenQueriesDelegate GenQueries;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BeginQueryDelegate(QueryTarget target, int queryId);
        public static BeginQueryDelegate BeginQuery;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void EndQueryDelegate(QueryTarget target);
        public static EndQueryDelegate EndQuery;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GetQueryObjectDelegate(int queryId, GetQueryObjectParam getparam, [Out] out int ready);
        public static GetQueryObjectDelegate GetQueryObject;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteQueriesDelegate(int count, [In] [Out] ref int queryId);
        public static DeleteQueriesDelegate DeleteQueries;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ActiveTextureDelegate(TextureUnit textureUnit);
        public static ActiveTextureDelegate ActiveTexture;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int CreateShaderDelegate(ShaderType type);
        public static CreateShaderDelegate CreateShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void ShaderSourceDelegate(int shaderId, int count, IntPtr code, int* length);
        public static ShaderSourceDelegate ShaderSourceInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void CompileShaderDelegate(int shaderId);
        public static CompileShaderDelegate CompileShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void GetShaderDelegate(int shaderId, int parameter, int* value);
        public static GetShaderDelegate GetShaderiv;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GetShaderInfoLogDelegate(int shader, int bufSize, IntPtr length, StringBuilder infoLog);
        public static GetShaderInfoLogDelegate GetShaderInfoLogInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate bool IsShaderDelegate(int shaderId);
        public static IsShaderDelegate IsShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteShaderDelegate(int shaderId);
        public static DeleteShaderDelegate DeleteShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int GetAttribLocationDelegate(int programId, string name);
        public static GetAttribLocationDelegate GetAttribLocation;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int GetUniformLocationDelegate(int programId, string name);
        public static GetUniformLocationDelegate GetUniformLocation;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate bool IsProgramDelegate(int programId);
        public static IsProgramDelegate IsProgram;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteProgramDelegate(int programId);
        public static DeleteProgramDelegate DeleteProgram;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate int CreateProgramDelegate();
        public static CreateProgramDelegate CreateProgram;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void AttachShaderDelegate(int programId, int shaderId);
        public static AttachShaderDelegate AttachShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void LinkProgramDelegate(int programId);
        public static LinkProgramDelegate LinkProgram;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public unsafe delegate void GetProgramDelegate(int programId, int name, int* linked);
        public static GetProgramDelegate GetProgramiv;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GetProgramInfoLogDelegate(int program, int bufSize, IntPtr length, StringBuilder infoLog);
        public static GetProgramInfoLogDelegate GetProgramInfoLogInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DetachShaderDelegate(int programId, int shaderId);
        public static DetachShaderDelegate DetachShader;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlendColorDelegate(float r, float g, float b, float a);
        public static BlendColorDelegate BlendColor;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlendEquationSeparateDelegate(BlendEquationMode colorMode, BlendEquationMode alphaMode);
        public static BlendEquationSeparateDelegate BlendEquationSeparate;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlendEquationSeparateiDelegate(int buffer, BlendEquationMode colorMode, BlendEquationMode alphaMode);
        public static BlendEquationSeparateiDelegate BlendEquationSeparatei;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlendFuncSeparateDelegate(BlendingFactorSrc colorSrc, BlendingFactorDest colorDst,
            BlendingFactorSrc alphaSrc, BlendingFactorDest alphaDst);
        public static BlendFuncSeparateDelegate BlendFuncSeparate;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BlendFuncSeparateiDelegate(int buffer, BlendingFactorSrc colorSrc, BlendingFactorDest colorDst,
            BlendingFactorSrc alphaSrc, BlendingFactorDest alphaDst);
        public static BlendFuncSeparateiDelegate BlendFuncSeparatei;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void ColorMaskDelegate(bool r, bool g, bool b, bool a);
        public static ColorMaskDelegate ColorMask;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DepthFuncDelegate(DepthFunction function);
        public static DepthFuncDelegate DepthFunc;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DepthMaskDelegate(bool enabled);
        public static DepthMaskDelegate DepthMask;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void StencilFuncSeparateDelegate(StencilFace face, GLStencilFunction function, int referenceStencil, int mask);
        public static StencilFuncSeparateDelegate StencilFuncSeparate;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void StencilOpSeparateDelegate(StencilFace face, StencilOp stencilfail, StencilOp depthFail, StencilOp pass);
        public static StencilOpSeparateDelegate StencilOpSeparate;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void StencilFuncDelegate(GLStencilFunction function, int referenceStencil, int mask);
        public static StencilFuncDelegate StencilFunc;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void StencilOpDelegate(StencilOp stencilfail, StencilOp depthFail, StencilOp pass);
        public static StencilOpDelegate StencilOp;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void StencilMaskDelegate(int mask);
        public static StencilMaskDelegate StencilMask;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void CompressedTexImage2DDelegate(TextureTarget target, int level, PixelInternalFormat internalFormat,
            int width, int height, int border, int size, IntPtr data);
        public static CompressedTexImage2DDelegate CompressedTexImage2D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexImage2DDelegate(TextureTarget target, int level, PixelInternalFormat internalFormat,
            int width, int height, int border, PixelFormat format, PixelType pixelType, IntPtr data);
        public static TexImage2DDelegate TexImage2D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void CompressedTexSubImage2DDelegate(TextureTarget target, int level,
            int x, int y, int width, int height, PixelInternalFormat format, int size, IntPtr data);
        public static CompressedTexSubImage2DDelegate CompressedTexSubImage2D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexSubImage2DDelegate(TextureTarget target, int level,
            int x, int y, int width, int height, PixelFormat format, PixelType pixelType, IntPtr data);
        public static TexSubImage2DDelegate TexSubImage2D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void PixelStoreDelegate(PixelStoreParameter parameter, int size);
        public static PixelStoreDelegate PixelStore;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void FinishDelegate();
        public static FinishDelegate Finish;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GetTexImageDelegate(TextureTarget target, int level, PixelFormat format, PixelType type, [Out] IntPtr pixels);
        public static GetTexImageDelegate GetTexImageInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GetCompressedTexImageDelegate(TextureTarget target, int level, [Out] IntPtr pixels);
        public static GetCompressedTexImageDelegate GetCompressedTexImageInternal;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexImage3DDelegate(TextureTarget target, int level, PixelInternalFormat internalFormat,
            int width, int height, int depth, int border, PixelFormat format, PixelType pixelType, IntPtr data);
        public static TexImage3DDelegate TexImage3D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void TexSubImage3DDelegate(TextureTarget target, int level,
            int x, int y, int z, int width, int height, int depth, PixelFormat format, PixelType pixelType, IntPtr data);
        public static TexSubImage3DDelegate TexSubImage3D;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteTexturesDelegate(int count, ref int id);
        public static DeleteTexturesDelegate DeleteTextures;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void GenBuffersDelegate(int count, out int buffer);
        public static GenBuffersDelegate GenBuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BufferDataDelegate(BufferTarget target, IntPtr size, IntPtr n, BufferUsageHint usage);
        public static BufferDataDelegate BufferData;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate IntPtr MapBufferDelegate(BufferTarget target, BufferAccess access);
        public static MapBufferDelegate MapBuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void UnmapBufferDelegate(BufferTarget target);
        public static UnmapBufferDelegate UnmapBuffer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void BufferSubDataDelegate(BufferTarget target, IntPtr offset, IntPtr size, IntPtr data);
        public static BufferSubDataDelegate BufferSubData;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DeleteBuffersDelegate(int count, [In] [Out] ref int buffer);
        public static DeleteBuffersDelegate DeleteBuffers;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void VertexAttribPointerDelegate(int location, int elementCount, VertexAttribPointerType type, bool normalize,
            int stride, IntPtr data);
        public static VertexAttribPointerDelegate VertexAttribPointer;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawElementsInstancedDelegate(GLPrimitiveType primitiveType, int count, DrawElementsType elementType,
            IntPtr offset, int instanceCount);
        public static DrawElementsInstancedDelegate DrawElementsInstanced;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void DrawElementsInstancedBaseInstanceDelegate(GLPrimitiveType primitiveType, int count, DrawElementsType elementType,
            IntPtr offset, int instanceCount, int baseInstance);
        public static DrawElementsInstancedBaseInstanceDelegate DrawElementsInstancedBaseInstance;

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [UnmanagedFunctionPointer(callingConvention)]
        [MonoNativeFunctionWrapper]
        public delegate void VertexAttribDivisorDelegate(int location, int frequency);
        public static VertexAttribDivisorDelegate VertexAttribDivisor;

#if DEBUG
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void DebugMessageCallbackProc(int source, int type, int id, int severity, int length, IntPtr message, IntPtr userParam);
        static DebugMessageCallbackProc DebugProc;
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [MonoNativeFunctionWrapper]
        delegate void DebugMessageCallbackDelegate(DebugMessageCallbackProc callback, IntPtr userParam);
        static DebugMessageCallbackDelegate DebugMessageCallback;

        public delegate void ErrorDelegate(string message);
        public static event ErrorDelegate OnError;

        static void DebugMessageCallbackHandler(int source, int type, int id, int severity, int length, IntPtr message, IntPtr userParam)
        {
            var errorMessage = Marshal.PtrToStringAnsi(message);
            System.Diagnostics.Debug.WriteLine(errorMessage);
            OnError?.Invoke(errorMessage);
        }
#endif

        public static int SwapInterval { get; set; }

        public static void LoadEntryPoints()
        {
            LoadPlatformEntryPoints();

            if (Viewport == null)
                Viewport = LoadFunction<ViewportDelegate>("glViewport");
            if (Scissor == null)
                Scissor = LoadFunction<ScissorDelegate>("glScissor");
            if (MakeCurrent == null)
                MakeCurrent = LoadFunction<MakeCurrentDelegate>("glMakeCurrent");

            GetError = LoadFunction<GetErrorDelegate>("glGetError");

            TexParameterf = LoadFunction<TexParameterFloatDelegate>("glTexParameterf");
            TexParameterfv = LoadFunction<TexParameterFloatArrayDelegate>("glTexParameterfv");
            TexParameteri = LoadFunction<TexParameterIntDelegate>("glTexParameteri");

            EnableVertexAttribArray = LoadFunction<EnableVertexAttribArrayDelegate>("glEnableVertexAttribArray");
            DisableVertexAttribArray = LoadFunction<DisableVertexAttribArrayDelegate>("glDisableVertexAttribArray");
            GetIntegerv = LoadFunction<GetIntegerDelegate>("glGetIntegerv");
            GetStringInternal = LoadFunction<GetStringDelegate>("glGetString");
            ClearDepth = LoadFunction<ClearDepthDelegate>("glClearDepth");
            if (ClearDepth == null)
                ClearDepth = LoadFunction<ClearDepthDelegate>("glClearDepthf");
            DepthRanged = LoadFunction<DepthRangedDelegate>("glDepthRange");
            DepthRangef = LoadFunction<DepthRangefDelegate>("glDepthRangef");
            Clear = LoadFunction<ClearDelegate>("glClear");
            ClearColor = LoadFunction<ClearColorDelegate>("glClearColor");
            ClearStencil = LoadFunction<ClearStencilDelegate>("glClearStencil");
            Flush = LoadFunction<FlushDelegate>("glFlush");
            GenTextures = LoadFunction<GenTexturesDelegte>("glGenTextures");
            BindTexture = LoadFunction<BindTextureDelegate>("glBindTexture");

            Enable = LoadFunction<EnableDelegate>("glEnable");
            Disable = LoadFunction<DisableDelegate>("glDisable");
            CullFace = LoadFunction<CullFaceDelegate>("glCullFace");
            FrontFace = LoadFunction<FrontFaceDelegate>("glFrontFace");
            PolygonMode = LoadFunction<PolygonModeDelegate>("glPolygonMode");
            PolygonOffset = LoadFunction<PolygonOffsetDelegate>("glPolygonOffset");

            BindBuffer = LoadFunction<BindBufferDelegate>("glBindBuffer");
            DrawBuffers = LoadFunction<DrawBuffersDelegate>("glDrawBuffers");
            DrawElements = LoadFunction<DrawElementsDelegate>("glDrawElements");
            DrawArrays = LoadFunction<DrawArraysDelegate>("glDrawArrays");
            Uniform1i = LoadFunction<Uniform1iDelegate>("glUniform1i");
            Uniform4fv = LoadFunction<Uniform4fvDelegate>("glUniform4fv");
            ReadPixelsInternal = LoadFunction<ReadPixelsDelegate>("glReadPixels");

            ReadBuffer = LoadFunction<ReadBufferDelegate>("glReadBuffer");
            DrawBuffer = LoadFunction<DrawBufferDelegate>("glDrawBuffer");

            // Render Target Support. These might be null if they are not supported
            // see GraphicsDevice.OpenGL.FramebufferHelper.cs for handling other extensions.
            GenRenderbuffers = LoadFunction<GenRenderbuffersDelegate>("glGenRenderbuffers");
            BindRenderbuffer = LoadFunction<BindRenderbufferDelegate>("glBindRenderbuffer");
            DeleteRenderbuffers = LoadFunction<DeleteRenderbuffersDelegate>("glDeleteRenderbuffers");
            GenFramebuffers = LoadFunction<GenFramebuffersDelegate>("glGenFramebuffers");
            BindFramebuffer = LoadFunction<BindFramebufferDelegate>("glBindFramebuffer");
            DeleteFramebuffers = LoadFunction<DeleteFramebuffersDelegate>("glDeleteFramebuffers");
            FramebufferTexture2D = LoadFunction<FramebufferTexture2DDelegate>("glFramebufferTexture2D");
            FramebufferRenderbuffer = LoadFunction<FramebufferRenderbufferDelegate>("glFramebufferRenderbuffer");
            RenderbufferStorage = LoadFunction<RenderbufferStorageDelegate>("glRenderbufferStorage");
            RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisample");
            GenerateMipmap = LoadFunction<GenerateMipmapDelegate>("glGenerateMipmap");
            BlitFramebuffer = LoadFunction<BlitFramebufferDelegate>("glBlitFramebuffer");
            CheckFramebufferStatus = LoadFunction<CheckFramebufferStatusDelegate>("glCheckFramebufferStatus");

            GenQueries = LoadFunction<GenQueriesDelegate>("glGenQueries");
            BeginQuery = LoadFunction<BeginQueryDelegate>("glBeginQuery");
            EndQuery = LoadFunction<EndQueryDelegate>("glEndQuery");
            GetQueryObject = LoadFunction<GetQueryObjectDelegate>("glGetQueryObjectuiv");
            if (GetQueryObject == null)
                GetQueryObject = LoadFunction<GetQueryObjectDelegate>("glGetQueryObjectivARB");
            if (GetQueryObject == null)
                GetQueryObject = LoadFunction<GetQueryObjectDelegate>("glGetQueryObjectiv");
            DeleteQueries = LoadFunction<DeleteQueriesDelegate>("glDeleteQueries");

            ActiveTexture = LoadFunction<ActiveTextureDelegate>("glActiveTexture");
            CreateShader = LoadFunction<CreateShaderDelegate>("glCreateShader");
            ShaderSourceInternal = LoadFunction<ShaderSourceDelegate>("glShaderSource");
            CompileShader = LoadFunction<CompileShaderDelegate>("glCompileShader");
            GetShaderiv = LoadFunction<GetShaderDelegate>("glGetShaderiv");
            GetShaderInfoLogInternal = LoadFunction<GetShaderInfoLogDelegate>("glGetShaderInfoLog");
            IsShader = LoadFunction<IsShaderDelegate>("glIsShader");
            DeleteShader = LoadFunction<DeleteShaderDelegate>("glDeleteShader");
            GetAttribLocation = LoadFunction<GetAttribLocationDelegate>("glGetAttribLocation");
            GetUniformLocation = LoadFunction<GetUniformLocationDelegate>("glGetUniformLocation");

            IsProgram = LoadFunction<IsProgramDelegate>("glIsProgram");
            DeleteProgram = LoadFunction<DeleteProgramDelegate>("glDeleteProgram");
            CreateProgram = LoadFunction<CreateProgramDelegate>("glCreateProgram");
            AttachShader = LoadFunction<AttachShaderDelegate>("glAttachShader");
            UseProgram = LoadFunction<UseProgramDelegate>("glUseProgram");
            LinkProgram = LoadFunction<LinkProgramDelegate>("glLinkProgram");
            GetProgramiv = LoadFunction<GetProgramDelegate>("glGetProgramiv");
            GetProgramInfoLogInternal = LoadFunction<GetProgramInfoLogDelegate>("glGetProgramInfoLog");
            DetachShader = LoadFunction<DetachShaderDelegate>("glDetachShader");

            BlendColor = LoadFunction<BlendColorDelegate>("glBlendColor");
            BlendEquationSeparate = LoadFunction<BlendEquationSeparateDelegate>("glBlendEquationSeparate");
            BlendEquationSeparatei = LoadFunction<BlendEquationSeparateiDelegate>("glBlendEquationSeparatei");
            BlendFuncSeparate = LoadFunction<BlendFuncSeparateDelegate>("glBlendFuncSeparate");
            BlendFuncSeparatei = LoadFunction<BlendFuncSeparateiDelegate>("glBlendFuncSeparatei");
            ColorMask = LoadFunction<ColorMaskDelegate>("glColorMask");
            DepthFunc = LoadFunction<DepthFuncDelegate>("glDepthFunc");
            DepthMask = LoadFunction<DepthMaskDelegate>("glDepthMask");
            StencilFuncSeparate = LoadFunction<StencilFuncSeparateDelegate>("glStencilFuncSeparate");
            StencilOpSeparate = LoadFunction<StencilOpSeparateDelegate>("glStencilOpSeparate");
            StencilFunc = LoadFunction<StencilFuncDelegate>("glStencilFunc");
            StencilOp = LoadFunction<StencilOpDelegate>("glStencilOp");
            StencilMask = LoadFunction<StencilMaskDelegate>("glStencilMask");

            CompressedTexImage2D = LoadFunction<CompressedTexImage2DDelegate>("glCompressedTexImage2D");
            TexImage2D = LoadFunction<TexImage2DDelegate>("glTexImage2D");
            CompressedTexSubImage2D = LoadFunction<CompressedTexSubImage2DDelegate>("glCompressedTexSubImage2D");
            TexSubImage2D = LoadFunction<TexSubImage2DDelegate>("glTexSubImage2D");
            PixelStore = LoadFunction<PixelStoreDelegate>("glPixelStorei");
            Finish = LoadFunction<FinishDelegate>("glFinish");
            GetTexImageInternal = LoadFunction<GetTexImageDelegate>("glGetTexImage");
            GetCompressedTexImageInternal = LoadFunction<GetCompressedTexImageDelegate>("glGetCompressedTexImage");
            TexImage3D = LoadFunction<TexImage3DDelegate>("glTexImage3D");
            TexSubImage3D = LoadFunction<TexSubImage3DDelegate>("glTexSubImage3D");
            DeleteTextures = LoadFunction<DeleteTexturesDelegate>("glDeleteTextures");

            GenBuffers = LoadFunction<GenBuffersDelegate>("glGenBuffers");
            BufferData = LoadFunction<BufferDataDelegate>("glBufferData");
            MapBuffer = LoadFunction<MapBufferDelegate>("glMapBuffer");
            UnmapBuffer = LoadFunction<UnmapBufferDelegate>("glUnmapBuffer");
            BufferSubData = LoadFunction<BufferSubDataDelegate>("glBufferSubData");
            DeleteBuffers = LoadFunction<DeleteBuffersDelegate>("glDeleteBuffers");

            VertexAttribPointer = LoadFunction<VertexAttribPointerDelegate>("glVertexAttribPointer");

            // Instanced drawing requires GL 3.2 or up, if the either of the following entry points can not be loaded
            // this will get flagged by setting SupportsInstancing in GraphicsCapabilities to false.
            try
            {
                DrawElementsInstanced = LoadFunction<DrawElementsInstancedDelegate>("glDrawElementsInstanced");
                VertexAttribDivisor = LoadFunction<VertexAttribDivisorDelegate>("glVertexAttribDivisor");
                DrawElementsInstancedBaseInstance = LoadFunction<DrawElementsInstancedBaseInstanceDelegate>("glDrawElementsInstancedBaseInstance");
            }
            catch (EntryPointNotFoundException)
            {
                // this will be detected in the initialization of GraphicsCapabilities
            }

#if DEBUG
            try
            {
                DebugMessageCallback = LoadFunction<DebugMessageCallbackDelegate>("glDebugMessageCallback");
                if (DebugMessageCallback != null)
                {
                    DebugProc = DebugMessageCallbackHandler;
                    DebugMessageCallback(DebugProc, IntPtr.Zero);
                    Enable(EnableCap.DebugOutput);
                    Enable(EnableCap.DebugOutputSynchronous);
                }
            }
            catch (EntryPointNotFoundException)
            {
                // Ignore the debug message callback if the entry point can not be found
            }
#endif
            if (BoundApi == RenderApi.ES)
            {
                InvalidateFramebuffer = LoadFunction<InvalidateFramebufferDelegate>("glDiscardFramebufferEXT");
            }

            LoadExtensions();
        }

        public static List<string> Extensions = new List<string>();

        //[Conditional("DEBUG")]
        //[DebuggerHidden]
        static void LogExtensions()
        {
#if __ANDROID__
            Android.Util.Log.Verbose("GL", "Supported Extensions");
            foreach (var ext in Extensions)
                Android.Util.Log.Verbose("GL", "   " + ext);
#endif
        }

        public static void LoadExtensions()
        {
            if (Extensions.Count == 0)
            {
                string extstring = GetString(StringName.Extensions);
                var error = GetError();
                if (!string.IsNullOrEmpty(extstring) && error == ErrorCode.NoError)
                    Extensions.AddRange(extstring.Split(' '));
            }
            LogExtensions();
            // now load Extensions :)
            if (GenRenderbuffers == null && Extensions.Contains("GL_EXT_framebuffer_object"))
            {
                LoadFrameBufferObjectEXTEntryPoints();
            }
            if (RenderbufferStorageMultisample == null)
            {
                if (Extensions.Contains("GL_APPLE_framebuffer_multisample"))
                {
                    RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisampleAPPLE");
                    BlitFramebuffer = LoadFunction<BlitFramebufferDelegate>("glResolveMultisampleFramebufferAPPLE");
                }
                else if (Extensions.Contains("GL_EXT_multisampled_render_to_texture"))
                {
                    RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisampleEXT");
                    FramebufferTexture2DMultiSample = LoadFunction<FramebufferTexture2DMultiSampleDelegate>("glFramebufferTexture2DMultisampleEXT");

                }
                else if (Extensions.Contains("GL_IMG_multisampled_render_to_texture"))
                {
                    RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisampleIMG");
                    FramebufferTexture2DMultiSample = LoadFunction<FramebufferTexture2DMultiSampleDelegate>("glFramebufferTexture2DMultisampleIMG");
                }
                else if (Extensions.Contains("GL_NV_framebuffer_multisample"))
                {
                    RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisampleNV");
                    BlitFramebuffer = LoadFunction<BlitFramebufferDelegate>("glBlitFramebufferNV");
                }
            }
            if (BlendFuncSeparatei == null && Extensions.Contains("GL_ARB_draw_buffers_blend"))
            {
                BlendFuncSeparatei = LoadFunction<BlendFuncSeparateiDelegate>("BlendFuncSeparateiARB");
            }
            if (BlendEquationSeparatei == null && Extensions.Contains("GL_ARB_draw_buffers_blend"))
            {
                BlendEquationSeparatei = LoadFunction<BlendEquationSeparateiDelegate>("BlendEquationSeparateiARB");
            }
        }

        public static void LoadFrameBufferObjectEXTEntryPoints()
        {
            GenRenderbuffers = LoadFunction<GenRenderbuffersDelegate>("glGenRenderbuffersEXT");
            BindRenderbuffer = LoadFunction<BindRenderbufferDelegate>("glBindRenderbufferEXT");
            DeleteRenderbuffers = LoadFunction<DeleteRenderbuffersDelegate>("glDeleteRenderbuffersEXT");
            GenFramebuffers = LoadFunction<GenFramebuffersDelegate>("glGenFramebuffersEXT");
            BindFramebuffer = LoadFunction<BindFramebufferDelegate>("glBindFramebufferEXT");
            DeleteFramebuffers = LoadFunction<DeleteFramebuffersDelegate>("glDeleteFramebuffersEXT");
            FramebufferTexture2D = LoadFunction<FramebufferTexture2DDelegate>("glFramebufferTexture2DEXT");
            FramebufferRenderbuffer = LoadFunction<FramebufferRenderbufferDelegate>("glFramebufferRenderbufferEXT");
            RenderbufferStorage = LoadFunction<RenderbufferStorageDelegate>("glRenderbufferStorageEXT");
            RenderbufferStorageMultisample = LoadFunction<RenderbufferStorageMultisampleDelegate>("glRenderbufferStorageMultisampleEXT");
            GenerateMipmap = LoadFunction<GenerateMipmapDelegate>("glGenerateMipmapEXT");
            BlitFramebuffer = LoadFunction<BlitFramebufferDelegate>("glBlitFramebufferEXT");
            CheckFramebufferStatus = LoadFunction<CheckFramebufferStatusDelegate>("glCheckFramebufferStatusEXT");
        }

        static partial void LoadPlatformEntryPoints();

        public static IGraphicsContext CreateContext(IWindowInfo info)
        {
            return PlatformCreateContext(info);
        }

        /* Helper Functions */

        public static void DepthRange(float min, float max)
        {
            if (BoundApi == RenderApi.ES)
                DepthRangef(min, max);
            else
                DepthRanged(min, max);
        }

        public static void Uniform1(int location, int value)
        {
            Uniform1i(location, value);
        }

        public static unsafe void Uniform4(int location, int size, float* value)
        {
            Uniform4fv(location, size, value);
        }

        public unsafe static string GetString(StringName name)
        {
            return Marshal.PtrToStringAnsi(GetStringInternal(name));
        }

        protected static IntPtr MarshalStringArrayToPtr(string[] strings)
        {
            IntPtr intPtr = IntPtr.Zero;
            if (strings != null && strings.Length != 0)
            {
                intPtr = Marshal.AllocHGlobal(strings.Length * IntPtr.Size);
                if (intPtr == IntPtr.Zero)
                {
                    throw new OutOfMemoryException();
                }
                int i = 0;
                try
                {
                    for (i = 0; i < strings.Length; i++)
                    {
                        IntPtr val = MarshalStringToPtr(strings[i]);
                        Marshal.WriteIntPtr(intPtr, i * IntPtr.Size, val);
                    }
                }
                catch (OutOfMemoryException)
                {
                    for (i--; i >= 0; i--)
                    {
                        Marshal.FreeHGlobal(Marshal.ReadIntPtr(intPtr, i * IntPtr.Size));
                    }
                    Marshal.FreeHGlobal(intPtr);
                    throw;
                }
            }
            return intPtr;
        }

        protected unsafe static IntPtr MarshalStringToPtr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return IntPtr.Zero;
            }
            int num = Encoding.ASCII.GetMaxByteCount(str.Length) + 1;
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            if (intPtr == IntPtr.Zero)
            {
                throw new OutOfMemoryException();
            }
            fixed (char* chars = str + RuntimeHelpers.OffsetToStringData / 2)
            {
                int bytes = Encoding.ASCII.GetBytes(chars, str.Length, (byte*)(void*)intPtr, num);
                Marshal.WriteByte(intPtr, bytes, 0);
                return intPtr;
            }
        }

        protected static void FreeStringArrayPtr(IntPtr ptr, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Marshal.FreeHGlobal(Marshal.ReadIntPtr(ptr, i * IntPtr.Size));
            }
            Marshal.FreeHGlobal(ptr);
        }

        public static string GetProgramInfoLog(int programId)
        {
            GetProgram(programId, GetProgramParameterName.LogLength, out int length);
            var sb = new StringBuilder(length, length);
            GetProgramInfoLogInternal(programId, length, IntPtr.Zero, sb);
            return sb.ToString();
        }

        public static string GetShaderInfoLog(int shaderId)
        {
            GetShader(shaderId, ShaderParameter.LogLength, out int length);
            var sb = new StringBuilder(length, length);
            GetShaderInfoLogInternal(shaderId, length, IntPtr.Zero, sb);
            return sb.ToString();
        }

        public unsafe static void ShaderSource(int shaderId, string code)
        {
            int length = code.Length;
            IntPtr intPtr = MarshalStringArrayToPtr(new string[] { code });
            ShaderSourceInternal(shaderId, 1, intPtr, &length);
            FreeStringArrayPtr(intPtr, 1);
        }

        public unsafe static void GetShader(int shaderId, ShaderParameter name, out int result)
        {
            fixed (int* ptr = &result)
            {
                GetShaderiv(shaderId, (int)name, ptr);
            }
        }

        public unsafe static void GetProgram(int programId, GetProgramParameterName name, out int result)
        {
            fixed (int* ptr = &result)
            {
                GetProgramiv(programId, (int)name, ptr);
            }
        }

        public unsafe static void GetInteger(GetPName name, out int value)
        {
            fixed (int* ptr = &value)
            {
                GetIntegerv((int)name, ptr);
            }
        }

        public unsafe static void GetInteger(int name, out int value)
        {
            fixed (int* ptr = &value)
            {
                GetIntegerv(name, ptr);
            }
        }

        public static void TexParameter(TextureTarget target, TextureParameterName name, float value)
        {
            TexParameterf(target, name, value);
        }

        public unsafe static void TexParameter(TextureTarget target, TextureParameterName name, float[] values)
        {
            fixed (float* ptr = &values[0])
            {
                TexParameterfv(target, name, ptr);
            }
        }

        public static void TexParameter(TextureTarget target, TextureParameterName name, int value)
        {
            TexParameteri(target, name, value);
        }

        public static void GetTexImage<T>(TextureTarget target, int level, PixelFormat format, PixelType type, T[] pixels) where T : struct
        {
            var pixelsPtr = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                GetTexImageInternal(target, level, format, type, pixelsPtr.AddrOfPinnedObject());
            }
            finally
            {
                pixelsPtr.Free();
            }
        }

        public static void GetCompressedTexImage<T>(TextureTarget target, int level, T[] pixels) where T : struct
        {
            var pixelsPtr = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                GetCompressedTexImageInternal(target, level, pixelsPtr.AddrOfPinnedObject());
            }
            finally
            {
                pixelsPtr.Free();
            }
        }

        public static void ReadPixels<T>(int x, int y, int width, int height, PixelFormat format, PixelType type, T[] data)
        {
            var dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                ReadPixelsInternal(x, y, width, height, format, type, dataPtr.AddrOfPinnedObject());
            }
            finally
            {
                dataPtr.Free();
            }
        }
    }
}
