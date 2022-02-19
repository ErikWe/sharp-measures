namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class MathPowersTests
{
    public static void Power_ShouldMatchSystem(IScalarQuantity quantity, double exponent, IScalarQuantity result)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, exponent), result.Magnitude, 2);
    }

    public static void Invert_ShouldMatchDefinition(IScalarQuantity quantity, IScalarQuantity inverted)
    {
        Assert.Equal(1 / quantity.Magnitude, inverted.Magnitude, 2);
    }

    public static void Square_ShouldMatchPower2(IScalarQuantity quantity, IScalarQuantity square)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, 2), square.Magnitude, 2);
    }

    public static void Cube_ShouldMatchPower3(IScalarQuantity quantity, IScalarQuantity cube)
    {
        Assert.Equal(Math.Pow(quantity.Magnitude, 3), cube.Magnitude, 2);
    }

    public static void SquareRoot_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity squareRoot)
    {
        Assert.Equal(Math.Sqrt(quantity.Magnitude), squareRoot.Magnitude, 2);
    }

    public static void CubeRoot_ShouldMatchSystem(IScalarQuantity quantity, IScalarQuantity cubeRoot)
    {
        Assert.Equal(Math.Cbrt(quantity.Magnitude), cubeRoot.Magnitude, 2);
    }
}