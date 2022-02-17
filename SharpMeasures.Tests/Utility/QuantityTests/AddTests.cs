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

        Assert.Equal(a.X + b.X, result.X, 2);
        Assert.Equal(a.Y + b.Y, result.Y, 2);
        Assert.Equal(a.Z + b.Z, result.Z, 2);
    }

    public static void Operator_Scalar_MagnitudeShouldEqualAdditionOfMagnitudes(IScalarQuantity term1, IScalarQuantity term2, IScalarQuantity sum)
    {
        Assert.Equal(term1.Magnitude + term2.Magnitude, sum.Magnitude, 2);
    }

    public static void Operator_Vector3_ComponentsShouldEqualAdditionOfComponents(IVector3Quantity term1, IVector3Quantity term2, IVector3Quantity sum)
    {
        Assert.Equal(term1.X + term2.X, sum.X, 2);
        Assert.Equal(term1.Y + term2.Y, sum.Y, 2);
        Assert.Equal(term1.Z + term2.Z, sum.Z, 2);
    }
}