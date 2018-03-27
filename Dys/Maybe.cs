using System;

namespace Dys
{
    public struct Maybe<T>
    {
        private readonly bool _HasValue;
        private readonly T _Value;

        public Maybe(T value)
        {
            _HasValue = true;
            _Value = value;
        }

        public bool HasValue
            => _HasValue;

        public T Value
            => _HasValue ? _Value : throw new InvalidOperationException("Maybe object must have a value.");

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return _HasValue ? new Maybe<TResult>(selector(_Value)) : new Maybe<TResult>();
        }

        public T GetValueOrDefault()
            => _HasValue ? _Value : default(T);
    }
}