namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System.Numerics;

using Xunit;

public static class TransformTests
{
    public static void Identity_ShouldBeUnaltered<TQuantity>(TQuantity a)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        TQuantity result = a.Transform(Matrix4x4.Identity);

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite)
        {
            Assert.Equal(a.X, result.X, 2);
            Assert.Equal(a.Y, result.Y, 2);
            Assert.Equal(a.Z, result.Z, 2);
        }
    }

    public static void Translation_ShouldBeOffsetByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, (float)b.X, (float)b.Y, (float)b.Z, 1);

        TQuantity result = a.Transform(matrix);

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite && !b.Magnitude().IsNaN && !b.Magnitude().IsInfinite)
        {
            Assert.Equal(Rounding.ToSignificantDigits(a.X + b.X, 5), Rounding.ToSignificantDigits(result.X, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.Y + b.Y, 5), Rounding.ToSignificantDigits(result.Y, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.Z + b.Z, 5), Rounding.ToSignificantDigits(result.Z, 5), 2);
        }
    }

    public static void Scaling_ShouldBeScaledByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new((float)b.X, 0, 0, 0, 0, (float)b.Y, 0, 0, 0, 0, (float)b.Z, 0, 0, 0, 0, 1);

        TQuantity result = a.Transform(matrix);

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite && !b.Magnitude().IsNaN && !b.Magnitude().IsInfinite)
        {
            Assert.Equal(Rounding.ToSignificantDigits(a.X * b.X, 5), Rounding.ToSignificantDigits(result.X, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.Y * b.Y, 5), Rounding.ToSignificantDigits(result.Y, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.Z * b.Z, 5), Rounding.ToSignificantDigits(result.Z, 5), 2);
        }
    }
}