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

        [Test]
        public void Value_WhenEmpty_ThrowsInvalidOperationException()
        {
            var maybe = new Maybe<int>();

            Assert.Throws<InvalidOperationException>(() => _ = maybe.Value);
        }

        [Test]
        [TestCase(0)]
        [TestCase(42)]
        public void Value_WhenInitializedWithValue_ReturnsValue(int input)
        {
            var maybe = new Maybe<int>(input);

            Assert.That(maybe.Value, Is.EqualTo(input));
        }
    }
}