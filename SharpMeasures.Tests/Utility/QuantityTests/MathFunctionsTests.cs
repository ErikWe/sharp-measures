namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class MathFunctionsTests
{
    public static void Absolute_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity absolute)
    {
        Assert.Equal(Math.Abs(quantity.Magnitude), absolute.Magnitude, 2);
    }

    public static void Floor_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity floor)
    {
        Assert.Equal(Math.Floor(quantity.Magnitude), floor.Magnitude, 2);
    }

    public static void Ceiling_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity ceiling)
    {
        Assert.Equal(Math.Ceiling(quantity.Magnitude), ceiling.Magnitude, 2);
    }

    public static void Round_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity round)
    {
        Assert.Equal(Math.Round(quantity.Magnitude), round.Magnitude, 2);
    }
}