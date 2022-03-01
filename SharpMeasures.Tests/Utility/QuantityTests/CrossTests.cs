namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class CrossTests
{
    public static void Vector3_ShouldMatchDefinition<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b)
        where TQuantity : ICrossableVector3Quantity<TProductQuantity, TFactorQuantity>
        where TProductQuantity : IVector3Quantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Cross(b);

        Assert.Equal(a.MagnitudeY * b.MagnitudeZ - a.MagnitudeZ * b.MagnitudeY, result.MagnitudeX, 2);
        Assert.Equal(a.MagnitudeZ * b.MagnitudeX - a.MagnitudeX * b.MagnitudeZ, result.MagnitudeY, 2);
        Assert.Equal(a.MagnitudeX * b.MagnitudeY - a.MagnitudeY * b.MagnitudeX, result.MagnitudeZ, 2);
    }

    public static void Vector3_ShouldMatchDefinition<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b, Func<(double, double, double), TProductQuantity> factory)
        where TQuantity : IGenericallyCrossableVector3Quantity
        where TProductQuantity : IVector3Quantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Cross(b, factory);

        Assert.Equal(a.MagnitudeY * b.MagnitudeZ - a.MagnitudeZ * b.MagnitudeY, result.MagnitudeX, 2);
        Assert.Equal(a.MagnitudeZ * b.MagnitudeX - a.MagnitudeX * b.MagnitudeZ, result.MagnitudeY, 2);
        Assert.Equal(a.MagnitudeX * b.MagnitudeY - a.MagnitudeY * b.MagnitudeX, result.MagnitudeZ, 2);
    }
}