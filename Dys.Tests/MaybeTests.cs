using System;

using NUnit.Framework;
// ReSharper disable AssignmentIsFullyDiscarded

// ReSharper disable PossibleNullReferenceException

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

        [Test]
        public void Select_NullSelector_ThrowsArgumentNullException()
        {
            var maybe = new Maybe<int>();

            Assert.Throws<ArgumentNullException>(() => maybe.Select<string>(null));
        }

        [Test]
        public void Select_WhenEmpty_ReturnsEmptyMaybe()
        {
            var maybe = new Maybe<int>();

            var output = maybe.Select(i => i.ToString());

            Assert.That(output.HasValue, Is.False);
        }

        [Test]
        public void Select_WhenEmpty_DoesNotCallSelector()
        {
            var maybe = new Maybe<int>();

            bool wasCalled = false;

            string func(int i)
            {
                wasCalled = true;
                return i.ToString();
            }

            maybe.Select(func);

            Assert.That(wasCalled, Is.False);
        }

        [Test]
        public void Select_WhenInitializedWithValue_CallsSelector()
        {
            var maybe = new Maybe<int>(42);

            bool wasCalled = false;

            string func(int i)
            {
                wasCalled = true;
                return i.ToString();
            }

            maybe.Select(func);

            Assert.That(wasCalled, Is.True);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(42, "42")]
        public void Select_WithTestCases_ReturnsCorrectValues(int input, string expected)
        {
            var maybe = new Maybe<int>(input);

            var output = maybe.Select(i => i.ToString()).Value;

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void GetValueOrDefault_WhenEmpty_ReturnsDefaultValue()
        {
            var maybe = new Maybe<int>();

            var output = maybe.GetValueOrDefault();

            Assert.That(output, Is.EqualTo(0));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(42)]
        public void GetValueOrDefault_WhenInitializedWithValue_ReturnsValue(int input)
        {
            var maybe = new Maybe<int>(input);

            var output = maybe.GetValueOrDefault();

            Assert.That(output, Is.EqualTo(input));
        }

        [Test]
        public void GetValueOrFallback_WhenEmpty_ReturnsFallbackValue()
        {
            var maybe = new Maybe<int>();

            var output = maybe.GetValueOrFallback(17);

            Assert.That(output, Is.EqualTo(17));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(42)]
        public void GetValueOrFallback_WhenInitializedWithValue_ReturnsValue(int input)
        {
            var maybe = new Maybe<int>(input);

            var output = maybe.GetValueOrFallback(-1);

            Assert.That(output, Is.EqualTo(input));
        }

        [Test]
        public void GetHashCode_WhenEmpty_ReturnsZero()
        {
            var maybe = new Maybe<int>();

            var output = maybe.GetHashCode();

            Assert.That(output, Is.EqualTo(0));
        }

        [Test]
        public void GetHashCode_WhenInitializedWithNull_ReturnsZero()
        {
            var maybe = new Maybe<string>(null);

            var output = maybe.GetHashCode();

            Assert.That(output, Is.EqualTo(0));
        }

        [Test]
        [TestCase("TEST")]
        public void GetHashCode_WhenInitializedWithValue_ReturnsSameValueAsGetHashCodeForThatValue(string input)
        {
            var maybe = new Maybe<string>(input);
            var expected = input.GetHashCode();

            var output = maybe.GetHashCode();

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void Equals_NullValue_ReturnsFalse()
        {
            var maybe = new Maybe<int>();

            var output = maybe.Equals(null);

            Assert.That(output, Is.False);
        }
        
        [Test]
        public void EqualsObject_TwoMaybesInitializedToSameValue_ReturnsTrue()
        {
            var maybe1 = new Maybe<string>("A");
            var maybe2 = new Maybe<string>("A");

            var output = maybe1.Equals((object)maybe2);

            Assert.That(output, Is.True);
        }

        [Test]
        public void EqualsMaybe_TwoMaybesInitializedToSameValue_ReturnsTrue()
        {
            var maybe1 = new Maybe<string>("A");
            var maybe2 = new Maybe<string>("A");

            var output = maybe1.Equals(maybe2);

            Assert.That(output, Is.True);
        }
    }
}