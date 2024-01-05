// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Content
{
    public abstract class ContentTypeReader
    {
        private Type _targetType;

        public virtual bool CanDeserializeIntoExistingObject => false;

        public Type TargetType => _targetType;

        public virtual int TypeVersion => 0;

        protected ContentTypeReader(Type targetType)
        {
            _targetType = targetType;
        }

        public virtual void Initialize(ContentTypeReaderManager manager)
        {
            // Do nothing. Are we supposed to add ourselves to the manager?
        }

        public abstract object Read(ContentReader input, object existingInstance);
    }

    public abstract class ContentTypeReader<T> : ContentTypeReader
    {
        protected ContentTypeReader()
            : base(typeof(T))
        {
            // Nothing
        }

        public override object Read(ContentReader input, object existingInstance)
        {
            // as per the documentation http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.content.contenttypereader.read.aspx
            // existingInstance
            // The object receiving the data, or null if a new instance of the object should be created.
            if (existingInstance == null)
            {
                return Read(input, default(T));
            }
            return Read(input, (T)existingInstance);
        }

        public abstract T Read(ContentReader input, T existingInstance);
    }
}
