#region File Description
//-----------------------------------------------------------------------------
// DualTextureEffect.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Built-in effect that supports two-layer multitexturing.
    /// </summary>
    public class DualTextureEffect : Effect, IEffectMatrices, IEffectFog
    {
        #region Effect Parameters

        EffectParameter textureParam;
        EffectParameter texture2Param;
        EffectParameter diffuseColorParam;
        EffectParameter fogColorParam;
        EffectParameter fogVectorParam;
        EffectParameter worldViewProjParam;

        #endregion

        #region Fields

        bool fogEnabled;
        bool vertexColorEnabled;

        Matrix world = Matrix.Identity;
        Matrix view = Matrix.Identity;
        Matrix projection = Matrix.Identity;

        Matrix worldView;

        Vector3 diffuseColor = Vector3.One;

        float alpha = 1;

        float fogStart;
        float fogEnd = 1;

        EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;

        #endregion

        #region Public Properties


        /// <summary>
        /// Gets or sets the world matrix.
        /// </summary>
        public Matrix World
        {
            get => world;

            set
            {
                world = value;
                dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.Fog;
            }
        }


        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        public Matrix View
        {
            get => view;

            set
            {
                view = value;
                dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.Fog;
            }
        }


        /// <summary>
        /// Gets or sets the projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get => projection;

            set
            {
                projection = value;
                dirtyFlags |= EffectDirtyFlags.WorldViewProj;
            }
        }


        /// <summary>
        /// Gets or sets the material diffuse color (range 0 to 1).
        /// </summary>
        public Vector3 DiffuseColor
        {
            get => diffuseColor;

            set
            {
                diffuseColor = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }


        /// <summary>
        /// Gets or sets the material alpha.
        /// </summary>
        public float Alpha
        {
            get => alpha;

            set
            {
                alpha = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }


        /// <summary>
        /// Gets or sets the fog enable flag.
        /// </summary>
        public bool FogEnabled
        {
            get => fogEnabled;

            set
            {
                if (fogEnabled != value)
                {
                    fogEnabled = value;
                    dirtyFlags |= EffectDirtyFlags.ShaderIndex | EffectDirtyFlags.FogEnable;
                }
            }
        }


        /// <summary>
        /// Gets or sets the fog start distance.
        /// </summary>
        public float FogStart
        {
            get => fogStart;

            set
            {
                fogStart = value;
                dirtyFlags |= EffectDirtyFlags.Fog;
            }
        }


        /// <summary>
        /// Gets or sets the fog end distance.
        /// </summary>
        public float FogEnd
        {
            get => fogEnd;

            set
            {
                fogEnd = value;
                dirtyFlags |= EffectDirtyFlags.Fog;
            }
        }


        /// <summary>
        /// Gets or sets the fog color.
        /// </summary>
        public Vector3 FogColor
        {
            get => fogColorParam.GetValueVector3();
            set => fogColorParam.SetValue(value);
        }


        /// <summary>
        /// Gets or sets the current base texture.
        /// </summary>
        public Texture2D Texture
        {
            get => textureParam.GetValueTexture2D();
            set => textureParam.SetValue(value);
        }


        /// <summary>
        /// Gets or sets the current overlay texture.
        /// </summary>
        public Texture2D Texture2
        {
            get => texture2Param.GetValueTexture2D();
            set => texture2Param.SetValue(value);
        }


        /// <summary>
        /// Gets or sets whether vertex color is enabled.
        /// </summary>
        public bool VertexColorEnabled
        {
            get => vertexColorEnabled;

            set
            {
                if (vertexColorEnabled != value)
                {
                    vertexColorEnabled = value;
                    dirtyFlags |= EffectDirtyFlags.ShaderIndex;
                }
            }
        }


        #endregion

        #region Methods


        /// <summary>
        /// Creates a new DualTextureEffect with default parameter settings.
        /// </summary>
        public DualTextureEffect(GraphicsDevice device)
            : base(device, EffectResource.DualTextureEffect.Bytecode)
        {
            CacheEffectParameters();
        }


        /// <summary>
        /// Creates a new DualTextureEffect by cloning parameter settings from an existing instance.
        /// </summary>
        protected DualTextureEffect(DualTextureEffect cloneSource)
            : base(cloneSource)
        {
            CacheEffectParameters();

            fogEnabled = cloneSource.fogEnabled;
            vertexColorEnabled = cloneSource.vertexColorEnabled;

            world = cloneSource.world;
            view = cloneSource.view;
            projection = cloneSource.projection;

            diffuseColor = cloneSource.diffuseColor;

            alpha = cloneSource.alpha;

            fogStart = cloneSource.fogStart;
            fogEnd = cloneSource.fogEnd;
        }


        /// <summary>
        /// Creates a clone of the current DualTextureEffect instance.
        /// </summary>
        public override Effect Clone()
        {
            return new DualTextureEffect(this);
        }


        /// <summary>
        /// Looks up shortcut references to our effect parameters.
        /// </summary>
        void CacheEffectParameters()
        {
            textureParam        = Parameters["Texture"];
            texture2Param       = Parameters["Texture2"];
            diffuseColorParam   = Parameters["DiffuseColor"];
            fogColorParam       = Parameters["FogColor"];
            fogVectorParam      = Parameters["FogVector"];
            worldViewProjParam  = Parameters["WorldViewProj"];
        }


        /// <summary>
        /// Lazily computes derived parameter values immediately before applying the effect.
        /// </summary>
        protected internal override void OnApply()
        {
            // Recompute the world+view+projection matrix or fog vector?
            dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(dirtyFlags, ref world, ref view, ref projection, ref worldView, fogEnabled, fogStart, fogEnd, worldViewProjParam, fogVectorParam);

            // Recompute the diffuse/alpha material color parameter?
            if ((dirtyFlags & EffectDirtyFlags.MaterialColor) != 0)
            {
                diffuseColorParam.SetValue(new Vector4(diffuseColor * alpha, alpha));

                dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
            }

            // Recompute the shader index?
            if ((dirtyFlags & EffectDirtyFlags.ShaderIndex) != 0)
            {
                int shaderIndex = 0;
                
                if (!fogEnabled)
                    shaderIndex += 1;
                
                if (vertexColorEnabled)
                    shaderIndex += 2;
                
                dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;

                CurrentTechnique = Techniques[shaderIndex];
            }
        }


        #endregion
    }
}
