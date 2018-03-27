
using System;

using NUnit.Framework;

namespace Dys.Tests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void Constructor_Default_ReturnsMaybeWithHasValueFalse()
        {
            var maybe = new Maybe<int>();
            
            Assert.That(maybe.HasValue, Is.False);
        }

        [Test]
        public void Constructor_WithValue_ReturnsMeybeWithHasValueTrue()
        {
            var maybe = new Maybe<int>(42);

            Assert.That(maybe.HasValue, Is.True);
        }
    }
}
