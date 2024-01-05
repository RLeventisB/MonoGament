// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
    public class DepthlessSpriteBatchItem
    {
        public Texture2D Texture;

        public VertexPositionColorTexture vertexTL;
        public VertexPositionColorTexture vertexTR;
        public VertexPositionColorTexture vertexBL;
        public VertexPositionColorTexture vertexBR;
        public DepthlessSpriteBatchItem()
        {
            vertexTL = new VertexPositionColorTexture();
            vertexTR = new VertexPositionColorTexture();
            vertexBL = new VertexPositionColorTexture();
            vertexBR = new VertexPositionColorTexture();
        }

        public void Set(float x, float y, float dx, float dy, float w, float h, float sin, float cos, Color color, Vector2 texCoordTL, Vector2 texCoordBR)
        {
            // TODO, Should we be just assigning the Depth Value to Z?
            // According to http://blogs.msdn.com/b/shawnhar/archive/2011/01/12/spritebatch-billboards-in-a-3d-world.aspx
            // We do.
            vertexTL.Position.X = x + dx * cos - dy * sin;
            vertexTL.Position.Y = y + dx * sin + dy * cos;
            vertexTL.Color = color;
            vertexTL.TextureCoordinate.X = texCoordTL.X;
            vertexTL.TextureCoordinate.Y = texCoordTL.Y;

            vertexTR.Position.X = x + (dx + w) * cos - dy * sin;
            vertexTR.Position.Y = y + (dx + w) * sin + dy * cos;
            vertexTR.Color = color;
            vertexTR.TextureCoordinate.X = texCoordBR.X;
            vertexTR.TextureCoordinate.Y = texCoordTL.Y;

            vertexBL.Position.X = x + dx * cos - (dy + h) * sin;
            vertexBL.Position.Y = y + dx * sin + (dy + h) * cos;
            vertexBL.Color = color;
            vertexBL.TextureCoordinate.X = texCoordTL.X;
            vertexBL.TextureCoordinate.Y = texCoordBR.Y;

            vertexBR.Position.X = x + (dx + w) * cos - (dy + h) * sin;
            vertexBR.Position.Y = y + (dx + w) * sin + (dy + h) * cos;
            vertexBR.Color = color;
            vertexBR.TextureCoordinate.X = texCoordBR.X;
            vertexBR.TextureCoordinate.Y = texCoordBR.Y;
        }
        public void Set(float x, float y, float w, float h, Color color, Vector2 texCoordTL, Vector2 texCoordBR)
        {
            vertexTL.Position.X = x;
            vertexTL.Position.Y = y;
            vertexTL.Color = color;
            vertexTL.TextureCoordinate.X = texCoordTL.X;
            vertexTL.TextureCoordinate.Y = texCoordTL.Y;

            vertexTR.Position.X = x + w;
            vertexTR.Position.Y = y;
            vertexTR.Color = color;
            vertexTR.TextureCoordinate.X = texCoordBR.X;
            vertexTR.TextureCoordinate.Y = texCoordTL.Y;

            vertexBL.Position.X = x;
            vertexBL.Position.Y = y + h;
            vertexBL.Color = color;
            vertexBL.TextureCoordinate.X = texCoordTL.X;
            vertexBL.TextureCoordinate.Y = texCoordBR.Y;

            vertexBR.Position.X = x + w;
            vertexBR.Position.Y = y + h;
            vertexBR.Color = color;
            vertexBR.TextureCoordinate.X = texCoordBR.X;
            vertexBR.TextureCoordinate.Y = texCoordBR.Y;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct VertexPositionColorTexture : IVertexType
        {
            public Vector2 Position;
            public Color Color;
            public Vector2 TextureCoordinate;
            public static readonly VertexDeclaration VertexDeclaration;

            public VertexPositionColorTexture(Vector2 position, Color color, Vector2 textureCoordinate)
            {
                Position = position;
                Color = color;
                TextureCoordinate = textureCoordinate;
            }

            VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Position.GetHashCode();
                    hashCode = hashCode * 397 ^ Color.GetHashCode();
                    hashCode = hashCode * 397 ^ TextureCoordinate.GetHashCode();
                    return hashCode;
                }
            }
            public override string ToString()
            {
                return "{{Position:" + Position + " Color:" + Color + " TextureCoordinate:" + TextureCoordinate + "}}";
            }

            public static bool operator ==(VertexPositionColorTexture left, VertexPositionColorTexture right)
            {
                return left.Position == right.Position && left.Color == right.Color && left.TextureCoordinate == right.TextureCoordinate;
            }

            public static bool operator !=(VertexPositionColorTexture left, VertexPositionColorTexture right)
            {
                return !(left == right);
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                if (obj.GetType() != GetType())
                    return false;

                return this == (VertexPositionColorTexture)obj;
            }

            static VertexPositionColorTexture()
            {
                var elements = new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                    new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                    new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                };
                VertexDeclaration = new VertexDeclaration(elements);
            }
        }
    }
}
