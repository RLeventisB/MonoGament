using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    [DebuggerDisplay("{DebugDisplayString}")]
    public class EffectParameter
    {
        internal Texture texture;
        public IntPtr data;
        private ushort elementCount;

        /// <summary>
        /// The next state key used when an effect parameter
        /// is updated by any of the 'set' methods.
        /// </summary>
        internal static ulong NextStateKey { get; private set; }

        internal EffectParameter(EffectParameterClass class_,
                                    EffectParameterType type,
                                    string name,
                                    int rowCount,
                                    int columnCount,
                                    string semantic,
                                    EffectAnnotationCollection annotations,
                                    EffectParameterCollection elements,
                                    EffectParameterCollection structMembers,
                                    IntPtr data,
                                    ushort elementCount)
        {
            ParameterClass = class_;
            ParameterType = type;

            Name = name;
            Semantic = semantic;
            Annotations = annotations;

            RowCount = rowCount;
            ColumnCount = columnCount;

            Elements = elements;
            StructureMembers = structMembers;

            Data = data;
            this.elementCount = elementCount;
            AdvanceState();
        }

        internal unsafe EffectParameter(EffectParameter cloneSource)
        {
            // Share all the immutable types.
            ParameterClass = cloneSource.ParameterClass;
            ParameterType = cloneSource.ParameterType;
            Name = cloneSource.Name;
            Semantic = cloneSource.Semantic;
            Annotations = cloneSource.Annotations;
            RowCount = cloneSource.RowCount;
            ColumnCount = cloneSource.ColumnCount;

            // Clone the mutable types.
            Elements = cloneSource.Elements.Clone();
            StructureMembers = cloneSource.StructureMembers.Clone();
            elementCount = cloneSource.elementCount;

            // The data is mutable, so we have to clone it.
            if (cloneSource.Data != IntPtr.Zero)
            {
                nuint size = new nuint((uint)(elementCount * sizeof(float)));
                Data = (nint)NativeMemory.Alloc(size);
                NativeMemory.Copy(cloneSource.Data.ToPointer(), Data.ToPointer(), size);
            }
            AdvanceState();
        }

        public string Name { get; private set; }

        public string Semantic { get; private set; }

        public EffectParameterClass ParameterClass { get; private set; }

        public EffectParameterType ParameterType { get; private set; }

        public int RowCount { get; private set; }

        public int ColumnCount { get; private set; }

        public EffectParameterCollection Elements { get; private set; }

        public EffectParameterCollection StructureMembers { get; private set; }

        public EffectAnnotationCollection Annotations { get; private set; }


        // TODO: Using object adds alot of boxing/unboxing overhead
        // and garbage generation.  We should consider a templated
        // type implementation to fix this!

        public IntPtr Data { get => data; private set => data = value; }
        public void AdvanceState()
        {
            StateKey = unchecked(NextStateKey++);
        }
        /// <summary>
        /// The current state key which is used to detect
		/// if the parameter value has been changed.
        /// </summary>
        public ulong StateKey { get; private set; }

        /// <summary>
        /// Property referenced by the DebuggerDisplayAttribute.
        /// </summary>
        private string DebugDisplayString
        {
            get
            {
                var semanticStr = string.Empty;
                if (!string.IsNullOrEmpty(Semantic))
                    semanticStr = string.Concat(" <", Semantic, ">");

                return string.Concat("[", ParameterClass, " ", ParameterType, "]", semanticStr, ", ", Name, " : ", GetDataValueString());
            }
        }
        #region m
        private unsafe string GetDataValueString()
        {
            string valueStr;

            if (Data == IntPtr.Zero)
            {
                if (Elements == null)
                    valueStr = "(null)";
                else
                    valueStr = string.Join(", ", Elements.Select(e => e.GetDataValueString()));
            }
            else
            {
                switch (ParameterClass)
                {
                    // Object types are stored directly in the Data property.
                    // Display Data's string value.
                    case EffectParameterClass.Object:
                        valueStr = Data.ToString();
                        break;

                    // Matrix types are stored in a float[16] which we don't really have room for.
                    // Display "...".
                    case EffectParameterClass.Matrix:
                        valueStr = "...";
                        break;

                    // Scalar types are stored as a float[1].
                    // Display the first (and only) element's string value.                    
                    case EffectParameterClass.Scalar:
                        valueStr = ((float*)data)[0].ToString();
                        break;

                    // Vector types are stored as an Array<Type>.
                    // Display the string value of each array element.
                    case EffectParameterClass.Vector:
                        float* array = (float*)data;
                        var arrayStr = new string[elementCount];
                        for (int i = 0; i < elementCount; i++)
                        {
                            arrayStr[i] = array[i].ToString();
                        }

                        valueStr = string.Join(" ", arrayStr);
                        break;

                    // Handle additional cases here...
                    default:
                        valueStr = Data.ToString();
                        break;
                }
            }

            return string.Concat("{", valueStr, "}");
        }

        public bool[] GetValueBooleanArray()
        {
            throw new NotImplementedException();
        }


        public unsafe int GetValueInt32()
        {
            if (ParameterClass != EffectParameterClass.Scalar || ParameterType != EffectParameterType.Int32)
                throw new InvalidCastException();

#if OPENGL
            // MojoShader encodes integers into a float.
            return (int)((float*)Data)[0];
#else
            return ((int[])Data)[0];
#endif
        }

        public int[] GetValueInt32Array()
        {
            if (Elements != null && Elements.Count > 0)
            {
                var ret = new int[RowCount * ColumnCount * Elements.Count];
                for (int i = 0; i < Elements.Count; i++)
                {
                    var elmArray = Elements[i].GetValueInt32Array();
                    for (var j = 0; j < elmArray.Length; j++)
                        ret[RowCount * ColumnCount * i + j] = elmArray[j];
                }
                return ret;
            }

            switch (ParameterClass)
            {
                case EffectParameterClass.Scalar:
                    return new int[] { GetValueInt32() };
                default:
                    throw new NotImplementedException();
            }
        }

        public unsafe Matrix GetValueMatrix()
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            if (RowCount != 4 || ColumnCount != 4)
                throw new InvalidCastException();

            var floatData = (float*)Data;

            return new Matrix(floatData[0], floatData[4], floatData[8], floatData[12],
                                floatData[1], floatData[5], floatData[9], floatData[13],
                                floatData[2], floatData[6], floatData[10], floatData[14],
                                floatData[3], floatData[7], floatData[11], floatData[15]);
        }

        public Matrix[] GetValueMatrixArray(int count)
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var ret = new Matrix[count];
            for (var i = 0; i < count; i++)
                ret[i] = Elements[i].GetValueMatrix();

            return ret;
        }

        public unsafe Quaternion GetValueQuaternion()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var vecInfo = (float*)Data;
            return new Quaternion(vecInfo[0], vecInfo[1], vecInfo[2], vecInfo[3]);
        }

        public Quaternion[] GetValueQuaternionArray()
        {
            throw new NotImplementedException();
        }

        public unsafe float GetValueSingle()
        {
            // TODO: Should this fetch int and bool as a float?
            if (ParameterClass != EffectParameterClass.Scalar || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            return ((float*)Data)[0];
        }

        public unsafe float[] GetValueSingleArray()
        {
            if (Elements != null && Elements.Count > 0)
            {
                var ret = new float[RowCount * ColumnCount * Elements.Count];
                for (int i = 0; i < Elements.Count; i++)
                {
                    var elmArray = Elements[i].GetValueSingleArray();
                    for (var j = 0; j < elmArray.Length; j++)
                        ret[RowCount * ColumnCount * i + j] = elmArray[j];
                }
                return ret;
            }

            switch (ParameterClass)
            {
                case EffectParameterClass.Scalar:
                    return new float[] { GetValueSingle() };
                case EffectParameterClass.Vector:
                    return new float[] { ((float*)Data)[0], ((float*)Data)[1] };
                case EffectParameterClass.Matrix:
                    float[] array = new float[16];
                    Marshal.Copy(Data, array, 0, array.Length);
                    return array;
                default:
                    throw new NotImplementedException();
            }
        }

        public unsafe string GetValueString()
        {
            if (ParameterClass != EffectParameterClass.Object || ParameterType != EffectParameterType.String)
                throw new InvalidCastException();

            return Encoding.UTF8.GetString((byte*)Data, elementCount);
        }

        public Texture2D GetValueTexture2D()
        {
            if (ParameterClass != EffectParameterClass.Object || ParameterType != EffectParameterType.Texture2D)
                throw new InvalidCastException();

            return (Texture2D)texture;
        }

#if !GLES
        public Texture3D GetValueTexture3D()
        {
            if (ParameterClass != EffectParameterClass.Object || ParameterType != EffectParameterType.Texture3D)
                throw new InvalidCastException();

            return (Texture3D)texture;
        }
#endif

        public TextureCube GetValueTextureCube()
        {
            if (ParameterClass != EffectParameterClass.Object || ParameterType != EffectParameterType.TextureCube)
                throw new InvalidCastException();

            return (TextureCube)texture;
        }

        public unsafe Vector2 GetValueVector2()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var vecInfo = (float*)data;
            return new Vector2(vecInfo[0], vecInfo[1]);
        }

        public Vector2[] GetValueVector2Array()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();
            if (Elements != null && Elements.Count > 0)
            {
                Vector2[] result = new Vector2[Elements.Count];
                for (int i = 0; i < Elements.Count; i++)
                {
                    var v = Elements[i].GetValueSingleArray();
                    result[i] = new Vector2(v[0], v[1]);
                }
                return result;
            }

            return null;
        }

        public unsafe Vector3 GetValueVector3()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var vecInfo = (float*)Data;
            return new Vector3(vecInfo[0], vecInfo[1], vecInfo[2]);
        }

        public Vector3[] GetValueVector3Array()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            if (Elements != null && Elements.Count > 0)
            {
                Vector3[] result = new Vector3[Elements.Count];
                for (int i = 0; i < Elements.Count; i++)
                {
                    var v = Elements[i].GetValueSingleArray();
                    result[i] = new Vector3(v[0], v[1], v[2]);
                }
                return result;
            }
            return null;
        }


        public unsafe Vector4 GetValueVector4()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var vecInfo = (float*)Data;
            return new Vector4(vecInfo[0], vecInfo[1], vecInfo[2], vecInfo[3]);
        }

        public Vector4[] GetValueVector4Array()
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            if (Elements != null && Elements.Count > 0)
            {
                Vector4[] result = new Vector4[Elements.Count];
                for (int i = 0; i < Elements.Count; i++)
                {
                    var v = Elements[i].GetValueSingleArray();
                    result[i] = new Vector4(v[0], v[1], v[2], v[3]);
                }
                return result;
            }
            return null;
        }

        public unsafe void SetValue(bool value)
        {
            if (ParameterClass != EffectParameterClass.Scalar || ParameterType != EffectParameterType.Bool)
                throw new InvalidCastException();

#if OPENGL
            // MojoShader encodes even booleans into a float.
            ((float*)Data)[0] = value ? 1 : 0;
#else
            ((int[])Data)[0] = value ? 1 : 0;
#endif

            AdvanceState();
        }
        public void SetValue(bool[] value)
        {
            throw new NotImplementedException();
        }
        public unsafe void SetValue(int value)
        {
            if (ParameterType == EffectParameterType.Single)
            {
                SetValue((float)value);
                return;
            }

            if (ParameterClass != EffectParameterClass.Scalar || ParameterType != EffectParameterType.Int32)
                throw new InvalidCastException();

#if OPENGL
            // MojoShader encodes integers into a float.
            ((float*)Data)[0] = value;
#else
            ((int[])Data)[0] = value;
#endif
            AdvanceState();
        }

        public void SetValue(int[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue(value[i]);

            AdvanceState();
        }

        public unsafe void SetValue(Matrix value)
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            // HLSL expects matrices to be transposed by default.
            // These unrolled loops do the transpose during assignment.
            if (RowCount == 4 && ColumnCount == 4)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;

                fData[8] = value.M13;
                fData[9] = value.M23;
                fData[10] = value.M33;
                fData[11] = value.M43;

                fData[12] = value.M14;
                fData[13] = value.M24;
                fData[14] = value.M34;
                fData[15] = value.M44;
            }
            else if (RowCount == 4 && ColumnCount == 3)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;

                fData[8] = value.M13;
                fData[9] = value.M23;
                fData[10] = value.M33;
                fData[11] = value.M43;
            }
            else if (RowCount == 4 && ColumnCount == 2)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;
            }
            else if (RowCount == 3 && ColumnCount == 4)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;

                fData[6] = value.M13;
                fData[7] = value.M23;
                fData[8] = value.M33;

                fData[9] = value.M14;
                fData[10] = value.M24;
                fData[11] = value.M34;
            }
            else if (RowCount == 3 && ColumnCount == 3)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;

                fData[6] = value.M13;
                fData[7] = value.M23;
                fData[8] = value.M33;
            }
            else if (RowCount == 3 && ColumnCount == 2)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;
            }

            AdvanceState();
        }

        public unsafe void SetValueTranspose(Matrix value)
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            // HLSL expects matrices to be transposed by default, so copying them straight
            // from the in-memory version effectively transposes them back to row-major.
            if (RowCount == 4 && ColumnCount == 4)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M12;
                fData[2] = value.M13;
                fData[3] = value.M14;

                fData[4] = value.M21;
                fData[5] = value.M22;
                fData[6] = value.M23;
                fData[7] = value.M24;

                fData[8] = value.M31;
                fData[9] = value.M32;
                fData[10] = value.M33;
                fData[11] = value.M34;

                fData[12] = value.M41;
                fData[13] = value.M42;
                fData[14] = value.M43;
                fData[15] = value.M44;
            }
            else if (RowCount == 4 && ColumnCount == 3)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M12;
                fData[2] = value.M13;

                fData[3] = value.M21;
                fData[4] = value.M22;
                fData[5] = value.M23;

                fData[6] = value.M31;
                fData[7] = value.M32;
                fData[8] = value.M33;

                fData[9] = value.M41;
                fData[10] = value.M42;
                fData[11] = value.M43;
            }
            else if (RowCount == 4 && ColumnCount == 2)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;
            }
            else if (RowCount == 3 && ColumnCount == 4)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M12;
                fData[2] = value.M13;
                fData[3] = value.M14;

                fData[4] = value.M21;
                fData[5] = value.M22;
                fData[6] = value.M23;
                fData[7] = value.M24;

                fData[8] = value.M31;
                fData[9] = value.M32;
                fData[10] = value.M33;
                fData[11] = value.M34;
            }
            else if (RowCount == 3 && ColumnCount == 3)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M12;
                fData[2] = value.M13;

                fData[3] = value.M21;
                fData[4] = value.M22;
                fData[5] = value.M23;

                fData[6] = value.M31;
                fData[7] = value.M32;
                fData[8] = value.M33;
            }
            else if (RowCount == 3 && ColumnCount == 2)
            {
                var fData = (float*)Data;

                fData[0] = value.M11;
                fData[1] = value.M12;
                fData[2] = value.M13;

                fData[3] = value.M21;
                fData[4] = value.M22;
                fData[5] = value.M23;
            }

            AdvanceState();
        }

        public unsafe void SetValue(Matrix[] value)
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            if (RowCount == 4 && ColumnCount == 4)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float*)Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;
                    fData[3] = value[i].M41;

                    fData[4] = value[i].M12;
                    fData[5] = value[i].M22;
                    fData[6] = value[i].M32;
                    fData[7] = value[i].M42;

                    fData[8] = value[i].M13;
                    fData[9] = value[i].M23;
                    fData[10] = value[i].M33;
                    fData[11] = value[i].M43;

                    fData[12] = value[i].M14;
                    fData[13] = value[i].M24;
                    fData[14] = value[i].M34;
                    fData[15] = value[i].M44;
                }
            }
            else if (RowCount == 4 && ColumnCount == 3)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float*)Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;
                    fData[3] = value[i].M41;

                    fData[4] = value[i].M12;
                    fData[5] = value[i].M22;
                    fData[6] = value[i].M32;
                    fData[7] = value[i].M42;

                    fData[8] = value[i].M13;
                    fData[9] = value[i].M23;
                    fData[10] = value[i].M33;
                    fData[11] = value[i].M43;
                }
            }
            else if (RowCount == 4 && ColumnCount == 2)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float[])Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;
                    fData[3] = value[i].M41;

                    fData[4] = value[i].M12;
                    fData[5] = value[i].M22;
                    fData[6] = value[i].M32;
                    fData[7] = value[i].M42;
                }
            }
            else if (RowCount == 3 && ColumnCount == 4)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float*)Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;

                    fData[6] = value[i].M13;
                    fData[7] = value[i].M23;
                    fData[8] = value[i].M33;

                    fData[9] = value[i].M14;
                    fData[10] = value[i].M24;
                    fData[11] = value[i].M34;
                }
            }
            else if (RowCount == 3 && ColumnCount == 3)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float*)Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;

                    fData[6] = value[i].M13;
                    fData[7] = value[i].M23;
                    fData[8] = value[i].M33;
                }
            }
            else if (RowCount == 3 && ColumnCount == 2)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float*)Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;
                }
            }

            AdvanceState();
        }

        public unsafe void SetValue(Quaternion value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float*)Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            fData[3] = value.W;
            AdvanceState();
        }

        public void SetValue(Quaternion[] value)
        {
            throw new NotImplementedException();
        }

        public unsafe void SetValue(float value)
        {
            if (ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();
            ((float*)Data)[0] = value;
            AdvanceState();
        }

        public void SetValue(float[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue(value[i]);

            AdvanceState();
        }

        public unsafe void SetValue(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            data = (nint)NativeMemory.AllocZeroed(new nuint(elementCount = (ushort)bytes.Length));
            Marshal.Copy(bytes, 0, data, value.Length);
        }

        public void SetValue(Texture value)
        {
            if (ParameterType != EffectParameterType.Texture &&
                ParameterType != EffectParameterType.Texture1D &&
                ParameterType != EffectParameterType.Texture2D &&
                ParameterType != EffectParameterType.Texture3D &&
                ParameterType != EffectParameterType.TextureCube)
            {
                throw new InvalidCastException();
            }

            texture = value;
            AdvanceState();
        }

        public unsafe void SetValue(Vector2 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float*)Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            AdvanceState();
        }

        public void SetValue(Vector2[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue(value[i]);
            AdvanceState();
        }

        public unsafe void SetValue(Vector3 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float*)Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            AdvanceState();
        }

        public void SetValue(Vector3[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue(value[i]);
            AdvanceState();
        }

        public unsafe void SetValue(Vector4 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float*)Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            fData[3] = value.W;
            AdvanceState();
        }

        public void SetValue(Vector4[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue(value[i]);
            AdvanceState();
        }

        #endregion
    }
}
