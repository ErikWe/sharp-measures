namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class DotTests
{
    public static void Vector3_ShouldBeSumOfSquares<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b)
        where TQuantity : IDotableVector3Quantity<TProductQuantity, TFactorQuantity>
        where TProductQuantity : IScalarQuantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Dot(b);

        Assert.Equal(a.MagnitudeX * b.MagnitudeX + a.MagnitudeY * b.MagnitudeY + a.MagnitudeZ * b.MagnitudeZ, result.Magnitude, 2);
    }

    public static void Vector3_ShouldBeSumOfSquares<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b, Func<double, TProductQuantity> factory)
        where TQuantity : IGenericallyDotableVector3Quantity
        where TProductQuantity : IScalarQuantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Dot(b, factory);

        Assert.Equal(a.MagnitudeX * b.MagnitudeX + a.MagnitudeY * b.MagnitudeY + a.MagnitudeZ * b.MagnitudeZ, result.Magnitude, 2);
    }
}