// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Helper class for drawing text strings and sprites in one or more optimized batches.
    /// </summary>
    public class DepthlessSpriteBatch : GraphicsResource
    {
        #region Public Fields
        public IDepthlessSpriteBatcher batcher;

        public BlendState blendState;
        public SamplerState samplerState;
        public DepthStencilState depthStencilState;
        public RasterizerState rasterizerState;
        public bool immediate, beginCalled;

        public Effect effect, spriteEffect;
        public Matrix TransformationMatrix;
        public EffectParameter transformationMatrixParameter;
        public EffectPass spritePass;

        public Rectangle _tempRect = new Rectangle(0, 0, 0, 0);
        public Vector2 _texCoordTL = new Vector2(0, 0);
        public Vector2 _texCoordBR = new Vector2(0, 0);
        public int oldViewportX, oldViewportY;
        #endregion

        /// <summary>
        /// Constructs a <see cref="DepthlessSpriteBatch"/>.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/>, which will be used for sprite rendering.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="graphicsDevice"/> is null.</exception>
        public DepthlessSpriteBatch(GraphicsDevice graphicsDevice) : this(graphicsDevice, 0)
        {
        }

        /// <summary>
        /// Constructs a <see cref="DepthlessSpriteBatch"/>.
        /// </summary>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/>, which will be used for sprite rendering.</param>
        /// <param name="capacity">The initial capacity of the internal array holding batch items (the value will be rounded to the next multiple of 64).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="graphicsDevice"/> is null.</exception>
        public DepthlessSpriteBatch(GraphicsDevice graphicsDevice, int capacity)
        {
            if (graphicsDevice == null)
            {
                throw new ArgumentNullException("graphicsDevice", FrameworkResources.ResourceCreationWhenDeviceIsNull);
            }

            GraphicsDevice = graphicsDevice;

            spriteEffect = new SpriteEffect(graphicsDevice);
            spritePass = spriteEffect.CurrentTechnique.Passes[0];
            transformationMatrixParameter = spriteEffect.Parameters["MatrixTransform"];

            batcher = new DepthlessSpriteBatcher(graphicsDevice, capacity);

            beginCalled = false;
        }

        /// <summary>
        /// Begins a new sprite and text batch with the specified render state.
        /// </summary>
        /// <param name="immediate">Whether to use immediate batching or not. Defaults to false.</param>
        /// <param name="blendState">State of the blending. Uses <see cref="BlendState.AlphaBlend"/> if null.</param>
        /// <param name="samplerState">State of the sampler. Uses <see cref="SamplerState.LinearClamp"/> if null.</param>
        /// <param name="depthStencilState">State of the depth-stencil buffer. Uses <see cref="DepthStencilState.None"/> if null.</param>
        /// <param name="rasterizerState">State of the rasterization. Uses <see cref="RasterizerState.CullCounterClockwise"/> if null.</param>
        /// <param name="effect">A custom <see cref="Effect"/> to override the default sprite effect. Uses default sprite effect if null.</param>
        /// <param name="transformMatrix">An optional matrix used to transform the sprite geometry. Uses <see cref="Matrix.Identity"/> if null.</param>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="Begin"/> is called next time without previous <see cref="End"/>.</exception>
        /// <remarks>This method uses optional parameters.</remarks>
        /// <remarks>The <see cref="Begin"/> Begin should be called before drawing commands, and you cannot call it again before subsequent <see cref="End"/>.</remarks>
        public void Begin
        (
            bool immediate = false,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null
        )
        {
            if (beginCalled)
                throw new InvalidOperationException("Begin cannot be called again until End has been successfully called.");

            // defaults
            this.immediate = immediate;
            this.blendState = blendState ?? BlendState.AlphaBlend;
            this.samplerState = samplerState ?? SamplerState.LinearClamp;
            this.depthStencilState = depthStencilState ?? DepthStencilState.None;
            this.rasterizerState = rasterizerState ?? RasterizerState.CullCounterClockwise;
            this.effect = effect;
            TransformationMatrix = transformMatrix ?? Matrix.Identity;

            // Setup things now so a user can change them.
            if (immediate)
            {
                Setup();
            }

            beginCalled = true;
        }

        /// <summary>
        /// Flushes all batched text and sprites to the screen.
        /// </summary>
        /// <remarks>This command should be called after <see cref="Begin"/> and drawing commands.</remarks>
        public virtual void End()
        {
            if (!beginCalled)
                throw new InvalidOperationException("Begin must be called before calling End.");

            beginCalled = false;

            if (!immediate)
                Setup();

            batcher.DrawBatch(effect);
        }

        public virtual unsafe void Setup()
        {
            var gd = GraphicsDevice;
            gd.BlendState = blendState;
            gd.DepthStencilState = depthStencilState;
            gd.RasterizerState = rasterizerState;
            gd.SamplerStates[0] = samplerState;
            Viewport viewport = GraphicsDevice.Viewport;

            if (oldViewportX != viewport.Width || oldViewportY != viewport.Height)
            {
                float num = (float)(2.0 / viewport.Width);
                float num2 = (float)(-2.0 / viewport.Height);
                float* ptr = (float*)transformationMatrixParameter.data;
                *ptr = num * TransformationMatrix.M11 - TransformationMatrix.M14;
                ptr[1] = num * TransformationMatrix.M21 - TransformationMatrix.M24;
                ptr[2] = num * TransformationMatrix.M31 - TransformationMatrix.M34;
                ptr[3] = num * TransformationMatrix.M41 - TransformationMatrix.M44;
                ptr[4] = num2 * TransformationMatrix.M12 + TransformationMatrix.M14;
                ptr[5] = num2 * TransformationMatrix.M22 + TransformationMatrix.M24;
                ptr[6] = num2 * TransformationMatrix.M32 + TransformationMatrix.M34;
                ptr[7] = num2 * TransformationMatrix.M42 + TransformationMatrix.M44;
                ptr[8] = TransformationMatrix.M13;
                ptr[9] = TransformationMatrix.M23;
                ptr[10] = TransformationMatrix.M33;
                ptr[11] = TransformationMatrix.M43;
                ptr[12] = TransformationMatrix.M14;
                ptr[13] = TransformationMatrix.M24;
                ptr[14] = TransformationMatrix.M34;
                ptr[15] = TransformationMatrix.M44;

                transformationMatrixParameter.AdvanceState();
                oldViewportX = viewport.Width;
                oldViewportY = viewport.Height;
            }
            spritePass.Apply();
        }

        void CheckValid(Texture2D texture)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!beginCalled)
                throw new InvalidOperationException("Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");
        }

        void CheckValid(SpriteFont spriteFont, string text)
        {
            if (spriteFont == null)
                throw new ArgumentNullException("spriteFont");
            if (text == null)
                throw new ArgumentNullException("text");
            if (!beginCalled)
                throw new InvalidOperationException("DrawString was called, but Begin has not yet been called. Begin must be called successfully before you can call DrawString.");
        }

        void CheckValid(SpriteFont spriteFont, StringBuilder text)
        {
            if (spriteFont == null)
                throw new ArgumentNullException("spriteFont");
            if (text == null)
                throw new ArgumentNullException("text");
            if (!beginCalled)
                throw new InvalidOperationException("DrawString was called, but Begin has not yet been called. Begin must be called successfully before you can call DrawString.");
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public void Draw(Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            origin *= scale;

            float w, h;
            if (sourceRectangle.HasValue)
            {
                var srcRect = sourceRectangle.GetValueOrDefault();
                w = srcRect.Width * scale.X;
                h = srcRect.Height * scale.Y;
                _texCoordTL.X = srcRect.X * texture.texelWidth;
                _texCoordTL.Y = srcRect.Y * texture.texelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.texelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.texelHeight;
            }
            else
            {
                w = texture.Width * scale.X;
                h = texture.Height * scale.Y;
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            if ((effects & SpriteEffects.FlipVertically) != 0)
            {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }
            if ((effects & SpriteEffects.FlipHorizontally) != 0)
            {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f)
            {
                item.Set(position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR);
            }
            else
            {
                item.Set(position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
                    color,
                    _texCoordTL,
                    _texCoordBR);
            }

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public void Draw(Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects = SpriteEffects.None)
        {
            var scaleVec = new Vector2(scale, scale);
            Draw(texture, position, sourceRectangle, color, rotation, origin, scaleVec, effects);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public void Draw(Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            if (sourceRectangle.HasValue)
            {
                var srcRect = sourceRectangle.GetValueOrDefault();
                _texCoordTL.X = srcRect.X * texture.texelWidth;
                _texCoordTL.Y = srcRect.Y * texture.texelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.texelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.texelHeight;

                if (srcRect.Width != 0)
                    origin.X = origin.X * destinationRectangle.Width / srcRect.Width;
                else
                    origin.X = origin.X * destinationRectangle.Width * texture.texelWidth;
                if (srcRect.Height != 0)
                    origin.Y = origin.Y * destinationRectangle.Height / srcRect.Height;
                else
                    origin.Y = origin.Y * destinationRectangle.Height * texture.texelHeight;
            }
            else
            {
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;

                origin.X = origin.X * destinationRectangle.Width * texture.texelWidth;
                origin.Y = origin.Y * destinationRectangle.Height * texture.texelHeight;
            }

            if ((effects & SpriteEffects.FlipVertically) != 0)
            {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }
            if ((effects & SpriteEffects.FlipHorizontally) != 0)
            {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f)
            {
                item.Set(destinationRectangle.X - origin.X,
                    destinationRectangle.Y - origin.Y,
                    destinationRectangle.Width,
                    destinationRectangle.Height,
                    color,
                    _texCoordTL,
                    _texCoordBR);
            }
            else
            {
                item.Set(destinationRectangle.X,
                    destinationRectangle.Y,
                    -origin.X,
                    -origin.Y,
                    destinationRectangle.Width,
                    destinationRectangle.Height,
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
                    color,
                    _texCoordTL,
                    _texCoordBR);
            }

            FlushIfNeeded();
        }

        // Mark the end of a draw operation for Immediate SpriteSortMode.
        public void FlushIfNeeded()
        {
            if (immediate)
            {
                batcher.DrawBatch(effect);
            }
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            Vector2 size;

            if (sourceRectangle.HasValue)
            {
                var srcRect = sourceRectangle.GetValueOrDefault();
                size = new Vector2(srcRect.Width, srcRect.Height);
                _texCoordTL.X = srcRect.X * texture.texelWidth;
                _texCoordTL.Y = srcRect.Y * texture.texelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.texelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.texelHeight;
            }
            else
            {
                size = new Vector2(texture.width, texture.height);
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            item.Set(position.X,
                position.Y,
                size.X,
                size.Y,
                color,
                _texCoordTL,
                _texCoordBR);

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            if (sourceRectangle.HasValue)
            {
                var srcRect = sourceRectangle.GetValueOrDefault();
                _texCoordTL.X = srcRect.X * texture.texelWidth;
                _texCoordTL.Y = srcRect.Y * texture.texelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.texelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.texelHeight;
            }
            else
            {
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            item.Set(destinationRectangle.X,
                destinationRectangle.Y,
                destinationRectangle.Width,
                destinationRectangle.Height,
                color,
                _texCoordTL,
                _texCoordBR);

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            item.Set(position.X,
                position.Y,
                texture.Width,
                texture.Height,
                color,
                Vector2.Zero,
                Vector2.One);

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            CheckValid(texture);

            var item = batcher.CreateBatchItem();
            item.Texture = texture;

            item.Set(destinationRectangle.X,
                destinationRectangle.Y,
                destinationRectangle.Width,
                destinationRectangle.Height,
                color,
                Vector2.Zero,
                Vector2.One);

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public unsafe void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            CheckValid(spriteFont, text);

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;
                    p.X += pCurrentGlyph->Cropping.X;
                    p.Y += pCurrentGlyph->Cropping.Y;
                    p += position;

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    item.Set(p.X,
                        p.Y,
                        pCurrentGlyph->BoundsInTexture.Width,
                        pCurrentGlyph->BoundsInTexture.Height,
                        color,
                        _texCoordTL,
                        _texCoordBR);

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public void DrawString(
            SpriteFont spriteFont, string text, Vector2 position, Color color,
            float rotation, Vector2 origin, float scale, SpriteEffects effects = SpriteEffects.None)
        {
            var scaleVec = new Vector2(scale, scale);
            DrawString(spriteFont, text, position, color, rotation, origin, scaleVec, effects);
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public unsafe void DrawString(
            SpriteFont spriteFont, string text, Vector2 position, Color color,
            float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(spriteFont, text);

            var flipAdjustment = Vector2.Zero;

            var flippedVert = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            var flippedHorz = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;

            if (flippedVert || flippedHorz)
            {

                var source = new SpriteFont.CharacterSource(text);
                spriteFont.MeasureString(ref source, out Vector2 size);

                if (flippedHorz)
                {
                    origin.X *= -1;
                    flipAdjustment.X = -size.X;
                }

                if (flippedVert)
                {
                    origin.Y *= -1;
                    flipAdjustment.Y = spriteFont.LineSpacing - size.Y;
                }
            }

            Matrix transformation = Matrix.Identity;
            float cos = 0, sin = 0;
            if (rotation == 0)
            {
                transformation.M11 = flippedHorz ? -scale.X : scale.X;
                transformation.M22 = flippedVert ? -scale.Y : scale.Y;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + position.X;
                transformation.M42 = (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }
            else
            {
                cos = MathF.Cos(rotation);
                sin = MathF.Sin(rotation);
                transformation.M11 = (flippedHorz ? -scale.X : scale.X) * cos;
                transformation.M12 = (flippedHorz ? -scale.X : scale.X) * sin;
                transformation.M21 = (flippedVert ? -scale.Y : scale.Y) * -sin;
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y) * cos;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + (flipAdjustment.Y - origin.Y) * transformation.M21 + position.X;
                transformation.M42 = (flipAdjustment.X - origin.X) * transformation.M12 + (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;

                    if (flippedHorz)
                        p.X += pCurrentGlyph->BoundsInTexture.Width;
                    p.X += pCurrentGlyph->Cropping.X;

                    if (flippedVert)
                        p.Y += pCurrentGlyph->BoundsInTexture.Height - spriteFont.LineSpacing;
                    p.Y += pCurrentGlyph->Cropping.Y;

                    Vector2.Transform(ref p, ref transformation, out p);

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    if ((effects & SpriteEffects.FlipVertically) != 0)
                    {
                        var temp = _texCoordBR.Y;
                        _texCoordBR.Y = _texCoordTL.Y;
                        _texCoordTL.Y = temp;
                    }
                    if ((effects & SpriteEffects.FlipHorizontally) != 0)
                    {
                        var temp = _texCoordBR.X;
                        _texCoordBR.X = _texCoordTL.X;
                        _texCoordTL.X = temp;
                    }

                    if (rotation == 0f)
                    {
                        item.Set(p.X,
                            p.Y,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }
                    else
                    {
                        item.Set(p.X,
                            p.Y,
                            0,
                            0,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            sin,
                            cos,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="rtl">Text is Right to Left.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public unsafe void DrawString(
            SpriteFont spriteFont, string text, Vector2 position, Color color,
            float rotation, Vector2 origin, Vector2 scale, bool rtl, SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(spriteFont, text);

            var flipAdjustment = Vector2.Zero;

            var flippedVert = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            var flippedHorz = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally ^ rtl;

            if (flippedVert || flippedHorz || rtl)
            {

                var source = new SpriteFont.CharacterSource(text);
                spriteFont.MeasureString(ref source, out Vector2 size);

                if (flippedHorz ^ rtl)
                {
                    origin.X *= -1;
                    flipAdjustment.X = -size.X;
                }
                if (flippedVert)
                {
                    origin.Y *= -1;
                    flipAdjustment.Y = spriteFont.LineSpacing - size.Y;
                }
            }

            Matrix transformation = Matrix.Identity;
            float cos = 0, sin = 0;
            if (rotation == 0)
            {
                transformation.M11 = flippedHorz ? -scale.X : scale.X;
                transformation.M22 = flippedVert ? -scale.Y : scale.Y;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + position.X;
                transformation.M42 = (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }
            else
            {
                cos = MathF.Cos(rotation);
                sin = MathF.Sin(rotation);
                transformation.M11 = (flippedHorz ? -scale.X : scale.X) * cos;
                transformation.M12 = (flippedHorz ? -scale.X : scale.X) * sin;
                transformation.M21 = (flippedVert ? -scale.Y : scale.Y) * -sin;
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y) * cos;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + (flipAdjustment.Y - origin.Y) * transformation.M21 + position.X;
                transformation.M42 = (flipAdjustment.X - origin.X) * transformation.M12 + (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(rtl ? pCurrentGlyph->RightSideBearing : pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + (rtl ? pCurrentGlyph->RightSideBearing : pCurrentGlyph->LeftSideBearing);
                    }

                    var p = offset;

                    if (flippedHorz)
                        p.X += pCurrentGlyph->BoundsInTexture.Width;
                    p.X += pCurrentGlyph->Cropping.X;

                    if (flippedVert)
                        p.Y += pCurrentGlyph->BoundsInTexture.Height - spriteFont.LineSpacing;
                    p.Y += pCurrentGlyph->Cropping.Y;

                    Vector2.Transform(ref p, ref transformation, out p);

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    if ((effects & SpriteEffects.FlipVertically) != 0)
                    {
                        var temp = _texCoordBR.Y;
                        _texCoordBR.Y = _texCoordTL.Y;
                        _texCoordTL.Y = temp;
                    }
                    if ((effects & SpriteEffects.FlipHorizontally) != 0)
                    {
                        var temp = _texCoordBR.X;
                        _texCoordBR.X = _texCoordTL.X;
                        _texCoordTL.X = temp;
                    }

                    if (rotation == 0f)
                    {
                        item.Set(p.X,
                            p.Y,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }
                    else
                    {
                        item.Set(p.X,
                            p.Y,
                            0,
                            0,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            sin,
                            cos,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }

                    offset.X += pCurrentGlyph->Width + (rtl ? pCurrentGlyph->LeftSideBearing : pCurrentGlyph->RightSideBearing);
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public unsafe void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
        {
            CheckValid(spriteFont, text);

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;
                    p.X += pCurrentGlyph->Cropping.X;
                    p.Y += pCurrentGlyph->Cropping.Y;
                    p += position;

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    item.Set(p.X,
                        p.Y,
                        pCurrentGlyph->BoundsInTexture.Width,
                        pCurrentGlyph->BoundsInTexture.Height,
                        color,
                        _texCoordTL,
                        _texCoordBR);

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public void DrawString(
            SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color,
            float rotation, Vector2 origin, float scale, SpriteEffects effects = SpriteEffects.None)
        {
            var scaleVec = new Vector2(scale, scale);
            DrawString(spriteFont, text, position, color, rotation, origin, scaleVec, effects);
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public unsafe void DrawString(
            SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color,
            float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(spriteFont, text);

            var flipAdjustment = Vector2.Zero;

            var flippedVert = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            var flippedHorz = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;

            if (flippedVert || flippedHorz)
            {
                var source = new SpriteFont.CharacterSource(text);
                spriteFont.MeasureString(ref source, out Vector2 size);

                if (flippedHorz)
                {
                    origin.X *= -1;
                    flipAdjustment.X = -size.X;
                }

                if (flippedVert)
                {
                    origin.Y *= -1;
                    flipAdjustment.Y = spriteFont.LineSpacing - size.Y;
                }
            }

            Matrix transformation = Matrix.Identity;
            float cos = 0, sin = 0;
            if (rotation == 0)
            {
                transformation.M11 = flippedHorz ? -scale.X : scale.X;
                transformation.M22 = flippedVert ? -scale.Y : scale.Y;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + position.X;
                transformation.M42 = (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }
            else
            {
                cos = MathF.Cos(rotation);
                sin = MathF.Sin(rotation);
                transformation.M11 = (flippedHorz ? -scale.X : scale.X) * cos;
                transformation.M12 = (flippedHorz ? -scale.X : scale.X) * sin;
                transformation.M21 = (flippedVert ? -scale.Y : scale.Y) * -sin;
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y) * cos;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + (flipAdjustment.Y - origin.Y) * transformation.M21 + position.X;
                transformation.M42 = (flipAdjustment.X - origin.X) * transformation.M12 + (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;

                    if (flippedHorz)
                        p.X += pCurrentGlyph->BoundsInTexture.Width;
                    p.X += pCurrentGlyph->Cropping.X;

                    if (flippedVert)
                        p.Y += pCurrentGlyph->BoundsInTexture.Height - spriteFont.LineSpacing;
                    p.Y += pCurrentGlyph->Cropping.Y;

                    Vector2.Transform(ref p, ref transformation, out p);

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    if ((effects & SpriteEffects.FlipVertically) != 0)
                    {
                        var temp = _texCoordBR.Y;
                        _texCoordBR.Y = _texCoordTL.Y;
                        _texCoordTL.Y = temp;
                    }
                    if ((effects & SpriteEffects.FlipHorizontally) != 0)
                    {
                        var temp = _texCoordBR.X;
                        _texCoordBR.X = _texCoordTL.X;
                        _texCoordTL.X = temp;
                    }

                    if (rotation == 0f)
                    {
                        item.Set(p.X,
                            p.Y,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }
                    else
                    {
                        item.Set(p.X,
                            p.Y,
                            0,
                            0,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            sin,
                            cos,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="rtl">Text is Right to Left.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        public unsafe void DrawString(
            SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color,
            float rotation, Vector2 origin, Vector2 scale, bool rtl, SpriteEffects effects = SpriteEffects.None)
        {
            CheckValid(spriteFont, text);

            var flipAdjustment = Vector2.Zero;

            var flippedVert = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            var flippedHorz = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally ^ rtl;

            if (flippedVert || flippedHorz || rtl)
            {

                var source = new SpriteFont.CharacterSource(text);
                spriteFont.MeasureString(ref source, out Vector2 size);

                if (flippedHorz ^ rtl)
                {
                    origin.X *= -1;
                    flipAdjustment.X = -size.X;
                }
                if (flippedVert)
                {
                    origin.Y *= -1;
                    flipAdjustment.Y = spriteFont.LineSpacing - size.Y;
                }
            }

            Matrix transformation = Matrix.Identity;
            float cos = 0, sin = 0;
            if (rotation == 0)
            {
                transformation.M11 = flippedHorz ? -scale.X : scale.X;
                transformation.M22 = flippedVert ? -scale.Y : scale.Y;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + position.X;
                transformation.M42 = (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }
            else
            {
                cos = MathF.Cos(rotation);
                sin = MathF.Sin(rotation);
                transformation.M11 = (flippedHorz ? -scale.X : scale.X) * cos;
                transformation.M12 = (flippedHorz ? -scale.X : scale.X) * sin;
                transformation.M21 = (flippedVert ? -scale.Y : scale.Y) * -sin;
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y) * cos;
                transformation.M41 = (flipAdjustment.X - origin.X) * transformation.M11 + (flipAdjustment.Y - origin.Y) * transformation.M21 + position.X;
                transformation.M42 = (flipAdjustment.X - origin.X) * transformation.M12 + (flipAdjustment.Y - origin.Y) * transformation.M22 + position.Y;
            }

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i)
                {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n')
                    {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine)
                    {
                        offset.X = Math.Max(rtl ? pCurrentGlyph->RightSideBearing : pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    }
                    else
                    {
                        offset.X += spriteFont.Spacing + (rtl ? pCurrentGlyph->RightSideBearing : pCurrentGlyph->LeftSideBearing);
                    }

                    var p = offset;

                    if (flippedHorz)
                        p.X += pCurrentGlyph->BoundsInTexture.Width;
                    p.X += pCurrentGlyph->Cropping.X;

                    if (flippedVert)
                        p.Y += pCurrentGlyph->BoundsInTexture.Height - spriteFont.LineSpacing;
                    p.Y += pCurrentGlyph->Cropping.Y;

                    Vector2.Transform(ref p, ref transformation, out p);

                    var item = batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.texelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.texelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.texelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.texelHeight;

                    if ((effects & SpriteEffects.FlipVertically) != 0)
                    {
                        var temp = _texCoordBR.Y;
                        _texCoordBR.Y = _texCoordTL.Y;
                        _texCoordTL.Y = temp;
                    }
                    if ((effects & SpriteEffects.FlipHorizontally) != 0)
                    {
                        var temp = _texCoordBR.X;
                        _texCoordBR.X = _texCoordTL.X;
                        _texCoordTL.X = temp;
                    }

                    if (rotation == 0f)
                    {
                        item.Set(p.X,
                            p.Y,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }
                    else
                    {
                        item.Set(p.X,
                            p.Y,
                            0,
                            0,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            sin,
                            cos,
                            color,
                            _texCoordTL,
                            _texCoordBR);
                    }

                    offset.X += pCurrentGlyph->Width + (rtl ? pCurrentGlyph->LeftSideBearing : pCurrentGlyph->RightSideBearing);
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (spriteEffect != null)
                    {
                        spriteEffect.Dispose();
                        spriteEffect = null;
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}
