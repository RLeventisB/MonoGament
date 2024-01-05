// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Content
{
    internal class EnumReader<T> : ContentTypeReader<T>
    {
        ContentTypeReader elementReader;

        public EnumReader()
        {
        }

        public override void Initialize(ContentTypeReaderManager manager)
        {
            Type readerType = Enum.GetUnderlyingType(typeof(T));
            elementReader = manager.GetTypeReader(readerType);
        }

        public override T Read(ContentReader input, T existingInstance)
        {
            return input.ReadRawObject<T>(elementReader);
        }
    }
}

