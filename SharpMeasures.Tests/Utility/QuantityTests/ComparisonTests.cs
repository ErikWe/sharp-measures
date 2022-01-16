
using System;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class ComparisonTests
    {
        public static void Method_ShouldMatchScalar<TQuantity>(TQuantity a, TQuantity b)
            where TQuantity : IQuantity, IComparable<TQuantity>
        {
            if (a.Magnitude.CompareTo(b.Magnitude) > 0)
            {
                Assert.True(a.CompareTo(b) > 0);
            }
            else if (a.Magnitude.CompareTo(b.Magnitude) < 0)
            {
                Assert.True(a.CompareTo(b) < 0);
            }
            else
            {
                Assert.Equal(0, a.CompareTo(b));
            }
        }

        public static void Operators_ShouldMatchMethod<TQuantity>(TQuantity a, TQuantity b, bool greaterThan, bool greaterThanOrEqual, bool lesserThanOrEqual, bool lesserThan)
            where TQuantity : IQuantity, IComparable<TQuantity>
        {
            if (a.CompareTo(b) > 0)
            {
                Assert.True(greaterThan);
                Assert.True(greaterThanOrEqual);
                Assert.False(lesserThanOrEqual);
                Assert.False(lesserThan);
            }
            else if (a.CompareTo(b) < 0)
            {
                Assert.False(greaterThan);
                Assert.False(greaterThanOrEqual);
                Assert.True(lesserThanOrEqual);
                Assert.True(lesserThan);
            }
            else
            {
                Assert.False(greaterThan);
                Assert.True(greaterThanOrEqual);
                Assert.True(lesserThanOrEqual);
                Assert.False(lesserThan);
            }
        }
    }
}
