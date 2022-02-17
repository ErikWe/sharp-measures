namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class EqualityTests
{
    public static void Method_ShouldBeEqualIfEqualMagnitudeAndType(IScalarQuantity a, IScalarQuantity? b)
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

    public static void Method_ShouldBeEqualIfEqualComponentsAndType(IVector3Quantity a, IVector3Quantity? b)
    {
        if (b is not null && a.GetType() == b.GetType() && a.X == b.X && a.Y == b.Y && a.Z == b.Z)
        {
            Assert.True(a.Equals(b));
        }
        else
        {
            Assert.False(a.Equals(b));
        }
    }

    public static void Operator_ShouldMatchMethodOrEqualIfBothNull(IScalarQuantity? a, IScalarQuantity? b, bool equality, bool inequality)
    {
        VerifyObjectEquality(a, b, equality, inequality);
    }

    public static void Operator_ShouldMatchMethodOrEqualIfBothNull(IVector3Quantity? a, IVector3Quantity? b, bool equality, bool inequality)
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