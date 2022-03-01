namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class SubtractTests
{
    public static void Method_Scalar_MagnitudeShouldEqualSubtractionOfMagnitudes<TQuantity, TDifferenceQuantity, TTermQuantity>(TQuantity a, TTermQuantity b)
        where TQuantity : ISubtractableScalarQuantity<TDifferenceQuantity, TTermQuantity>
        where TDifferenceQuantity : IScalarQuantity
        where TTermQuantity : IScalarQuantity
    {
        TDifferenceQuantity result = a.Subtract(b);

        Assert.Equal(a.Magnitude - b.Magnitude, result.Magnitude, 2);
    }

    public static void Method_Vector3_ComponentsShouldEqualSubtractionOfComponents<TQuantity, TDifferenceQuantity, TTermQuantity>(TQuantity a, TTermQuantity b)
        where TQuantity : ISubtractableVector3Quantity<TDifferenceQuantity, TTermQuantity>
        where TDifferenceQuantity : IVector3Quantity
        where TTermQuantity : IVector3Quantity
    {
        TDifferenceQuantity result = a.Subtract(b);

        Assert.Equal(a.MagnitudeX - b.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(a.MagnitudeY - b.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(a.MagnitudeZ - b.MagnitudeZ, result.MagnitudeZ, 2);
    }

    public static void Operator_Scalar_MagnitudeShouldEqualSubtractionOfMagnitudes(IScalarQuantity term1, IScalarQuantity term2, IScalarQuantity difference)
    {
        Assert.Equal(term1.Magnitude - term2.Magnitude, difference.Magnitude, 2);
    }

    public static void Operator_Vector3_ComponentsShouldEqualSubtractionOfComponents(IVector3Quantity term1, IVector3Quantity term2, IVector3Quantity difference)
    {
        Assert.Equal(term1.MagnitudeX - term2.MagnitudeX, difference.MagnitudeX, 2);
        Assert.Equal(term1.MagnitudeY - term2.MagnitudeY, difference.MagnitudeY, 2);
        Assert.Equal(term1.MagnitudeZ - term2.MagnitudeZ, difference.MagnitudeZ, 2);
    }
}