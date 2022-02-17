namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class PropertiesTests
{
    public static void IsNaN_ShouldMatchDouble(IScalarQuantity a, bool isNaN)
    {
        Assert.Equal(double.IsNaN(a.Magnitude), isNaN);
    }

    public static void IsZero_ShouldBeTrueWhenZero(IScalarQuantity a, bool isZero)
    {
        Assert.Equal(a.Magnitude == 0, isZero);
    }

    public static void IsPositive_ShouldBeTrueWhenLargerThanZero(IScalarQuantity a, bool isPositive)
    {
        Assert.Equal(a.Magnitude > 0, isPositive);
    }

    public static void IsNegative_ShouldMatchDouble(IScalarQuantity a, bool isNegative)
    {
        Assert.Equal(double.IsNegative(a.Magnitude), isNegative);
    }

    public static void IsFinite_ShouldMatchDouble(IScalarQuantity a, bool isFinite)
    {
        Assert.Equal(double.IsFinite(a.Magnitude), isFinite);
    }

    public static void IsInfinity_ShouldMatchDouble(IScalarQuantity a, bool isInfinity)
    {
        Assert.Equal(double.IsInfinity(a.Magnitude), isInfinity);
    }

    public static void IsPositiveInfinity_ShouldMatchDouble(IScalarQuantity a, bool isPositiveInfinity)
    {
        Assert.Equal(double.IsPositiveInfinity(a.Magnitude), isPositiveInfinity);
    }

    public static void IsNegativeInfinity_ShouldMatchDouble(IScalarQuantity a, bool isNegativeInfinity)
    {
        Assert.Equal(double.IsNegativeInfinity(a.Magnitude), isNegativeInfinity);
    }
}