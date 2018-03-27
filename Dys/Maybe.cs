using System;
using System.Collections.Generic;
using System.Text;

namespace Dys
{
    public struct Maybe<T>
    {
        private readonly bool _HasValue;

        public Maybe(T value)
        {
            _HasValue = true;
        }

        public bool HasValue
            => _HasValue;
    }
}
