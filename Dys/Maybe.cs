using System;
using System.Collections.Generic;

namespace Dys
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
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

        public T GetValueOrFallback(T fallbackValue)
            => _HasValue ? _Value : fallbackValue;

        public override int GetHashCode()
            => _HasValue ? _Value?.GetHashCode() ?? 0 : 0;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Maybe<T> maybe && Equals(maybe);
        }

        public bool Equals(Maybe<T> other)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _HasValue == other._HasValue && EqualityComparer<T>.Default.Equals(_Value, other._Value);
        }
    }
}