// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Content
{
    internal class NullableReader<T> : ContentTypeReader<T?> where T : struct
    {
        ContentTypeReader elementReader;

        public NullableReader()
        {
        }

        public override void Initialize(ContentTypeReaderManager manager)
        {
            Type readerType = typeof(T);
            elementReader = manager.GetTypeReader(readerType);
        }

        public override T? Read(ContentReader input, T? existingInstance)
        {
            if (input.ReadBoolean())
                return input.ReadObject<T>(elementReader);

            return null;
        }
    }
}

