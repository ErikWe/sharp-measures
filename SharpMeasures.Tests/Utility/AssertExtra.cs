namespace ErikWe.SharpMeasures.Tests.Utility;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class AssertExtra
{
    public static void AssertQuantitiesEqualValue(IScalarQuantity x, IScalarQuantity y)
    {
        Assert.Equal(x.Magnitude, y.Magnitude, 2);
    }

    public static void AssertQuantitiesEqualValue(IVector3Quantity a, IVector3Quantity b)
    {
        Assert.Equal(a.X, b.X, 2);
        Assert.Equal(a.Y, b.Y, 2);
        Assert.Equal(a.Z, b.Z, 2);
    }
}