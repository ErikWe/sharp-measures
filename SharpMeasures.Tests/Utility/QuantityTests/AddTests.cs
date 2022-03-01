namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class AddTests
{
    public static void Method_Scalar_MagnitudeShouldEqualAdditionOfMagnitudes<TQuantity, TSumQuantity, TTermQuantity>(TQuantity a, TTermQuantity b)
        where TQuantity : IAddableScalarQuantity<TSumQuantity, TTermQuantity>
        where TSumQuantity : IScalarQuantity
        where TTermQuantity : IScalarQuantity
    {
        TSumQuantity result = a.Add(b);

        Assert.Equal(a.Magnitude + b.Magnitude, result.Magnitude, 2);
    }

    public static void Method_Vector3_ComponentsShouldEqualAdditionOfComponents<TQuantity, TSumQuantity, TTermQuantity>(TQuantity a, TTermQuantity b)
        where TQuantity : IAddableVector3Quantity<TSumQuantity, TTermQuantity>
        where TSumQuantity : IVector3Quantity
        where TTermQuantity : IVector3Quantity
    {
        TSumQuantity result = a.Add(b);

        Assert.Equal(a.MagnitudeX + b.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(a.MagnitudeY + b.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(a.MagnitudeZ + b.MagnitudeZ, result.MagnitudeZ, 2);
    }

    public static void Operator_Scalar_MagnitudeShouldEqualAdditionOfMagnitudes(IScalarQuantity term1, IScalarQuantity term2, IScalarQuantity sum)
    {
        Assert.Equal(term1.Magnitude + term2.Magnitude, sum.Magnitude, 2);
    }

    public static void Operator_Vector3_ComponentsShouldEqualAdditionOfComponents(IVector3Quantity term1, IVector3Quantity term2, IVector3Quantity sum)
    {
        Assert.Equal(term1.MagnitudeX + term2.MagnitudeX, sum.MagnitudeX, 2);
        Assert.Equal(term1.MagnitudeY + term2.MagnitudeY, sum.MagnitudeY, 2);
        Assert.Equal(term1.MagnitudeZ + term2.MagnitudeZ, sum.MagnitudeZ, 2);
    }
}