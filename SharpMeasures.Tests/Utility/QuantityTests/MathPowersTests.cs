namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class MathPowersTests
{
    public static void Power_ShouldMatchSystem(IScalarQuantity quantity, double exponent, double result)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, exponent), result, 2);
    }

    public static void Invert_ShouldMatchDefinition(IScalarQuantity quantity, double inverted)
    {
        Assert.Equal(1 / quantity.Magnitude, inverted, 2);
    }

    public static void Square_ShouldMatchPower2(IScalarQuantity quantity, double square)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, 2), square, 2);
    }

    public static void Cube_ShouldMatchPower3(IScalarQuantity quantity, double cube)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, 3), cube, 2);
    }

    public static void SquareRoot_ShouldMatchSystem(IScalarQuantity quantity, double squareRoot)
    {
        Assert.Equal(Math.Sqrt(quantity.Magnitude), squareRoot, 2);
    }

    public static void CubeRoot_ShouldMatchSystem(IScalarQuantity quantity, double cubeRoot)
    {
        Assert.Equal(Math.Cbrt(quantity.Magnitude), cubeRoot, 2);
    }
}