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

        Assert.Equal(a.X - b.X, result.X, 2);
        Assert.Equal(a.Y - b.Y, result.Y, 2);
        Assert.Equal(a.Z - b.Z, result.Z, 2);
    }

    public static void Operator_Scalar_MagnitudeShouldEqualSubtractionOfMagnitudes(IScalarQuantity term1, IScalarQuantity term2, IScalarQuantity difference)
    {
        Assert.Equal(term1.Magnitude - term2.Magnitude, difference.Magnitude, 2);
    }

    public static void Operator_Vector3_ComponentsShouldEqualSubtractionOfComponents(IVector3Quantity term1, IVector3Quantity term2, IVector3Quantity difference)
    {
        Assert.Equal(term1.X - term2.X, difference.X, 2);
        Assert.Equal(term1.Y - term2.Y, difference.Y, 2);
        Assert.Equal(term1.Z - term2.Z, difference.Z, 2);
    }
}