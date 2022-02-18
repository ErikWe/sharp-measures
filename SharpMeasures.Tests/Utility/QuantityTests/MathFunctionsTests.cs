namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class MathFunctionsTests
{
    public static void Absolute_ShouldMatchSystem(IScalarQuantity quantity, double absolute)
    {
        Assert.Equal(Math.Abs(quantity.Magnitude), absolute, 2);
    }

    public static void Floor_ShouldMatchSystem(IScalarQuantity quantity, double floor)
    {
        Assert.Equal(Math.Floor(quantity.Magnitude), floor, 2);
    }

    public static void Ceiling_ShouldMatchSystem(IScalarQuantity quantity, double ceiling)
    {
        Assert.Equal(Math.Ceiling(quantity.Magnitude), ceiling, 2);
    }

    public static void Round_ShouldMatchSystem(IScalarQuantity quantity, double round)
    {
        Assert.Equal(Math.Round(quantity.Magnitude), round, 2);
    }
}