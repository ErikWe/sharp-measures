
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class EqualityTests
    {
        public static void Method_ShouldBeEqualIfEqualMagnitudeAndType(IQuantity a, IQuantity? b)
        {
            if (b is not null && a.GetType() == b.GetType() && a.Magnitude == b.Magnitude)
            {
                Assert.True(a.Equals(b));
            }
            else
            {
                Assert.False(a.Equals(b));
            }
        }

        public static void Method_ShouldBeEqualIfEqualComponentsAndType(IQuantity2 a, IQuantity2? b)
        {
            if (b is not null && a.GetType() == b.GetType() && a.MagnitudeX == b.MagnitudeX && a.MagnitudeY == b.MagnitudeY)
            {
                Assert.True(a.Equals(b));
            }
            else
            {
                Assert.False(a.Equals(b));
            }
        }

        public static void Method_ShouldBeEqualIfEqualComponentsAndType(IQuantity3 a, IQuantity3? b)
        {
            if (b is not null && a.GetType() == b.GetType() && a.MagnitudeX == b.MagnitudeX && a.MagnitudeY == b.MagnitudeY && a.MagnitudeZ == b.MagnitudeZ)
            {
                Assert.True(a.Equals(b));
            }
            else
            {
                Assert.False(a.Equals(b));
            }
        }

        public static void Operator_ShouldMatchMethodOrEqualIfBothNull(IQuantity? a, IQuantity? b, bool equality, bool inequality)
        {
            VerifyObjectEquality(a, b, equality, inequality);
        }

        public static void Operator_ShouldMatchMethodOrEqualIfBothNull(IQuantity2? a, IQuantity2? b, bool equality, bool inequality)
        {
            VerifyObjectEquality(a, b, equality, inequality);
        }

        public static void Operator_ShouldMatchMethodOrEqualIfBothNull(IQuantity3? a, IQuantity3? b, bool equality, bool inequality)
        {
            VerifyObjectEquality(a, b, equality, inequality);
        }

        private static void VerifyObjectEquality(object? a, object? b, bool equality, bool inequality)
        {
            Assert.NotEqual(equality, inequality);

            if (a is null)
            {
                if (b is null)
                {
                    Assert.True(equality);
                }
                else
                {
                    Assert.False(equality);
                }
            }
            else
            {
                Assert.Equal(a.Equals(b), equality);
            }
        }
    }
}
