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
            Assert.Equal(a.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ, result.MagnitudeZ, 2);
        }
    }

    public static void Translation_ShouldBeOffsetByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, (float)b.MagnitudeX, (float)b.MagnitudeY, (float)b.MagnitudeZ, 1);

        TQuantity result = a.Transform(matrix);

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite && !b.Magnitude().IsNaN && !b.Magnitude().IsInfinite)
        {
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeX + b.MagnitudeX, 5), Rounding.ToSignificantDigits(result.MagnitudeX, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeY + b.MagnitudeY, 5), Rounding.ToSignificantDigits(result.MagnitudeY, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeZ + b.MagnitudeZ, 5), Rounding.ToSignificantDigits(result.MagnitudeZ, 5), 2);
        }
    }

    public static void Scaling_ShouldBeScaledByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new((float)b.MagnitudeX, 0, 0, 0, 0, (float)b.MagnitudeY, 0, 0, 0, 0, (float)b.MagnitudeZ, 0, 0, 0, 0, 1);

        TQuantity result = a.Transform(matrix);

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite && !b.Magnitude().IsNaN && !b.Magnitude().IsInfinite)
        {
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeX * b.MagnitudeX, 5), Rounding.ToSignificantDigits(result.MagnitudeX, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeY * b.MagnitudeY, 5), Rounding.ToSignificantDigits(result.MagnitudeY, 5), 2);
            Assert.Equal(Rounding.ToSignificantDigits(a.MagnitudeZ * b.MagnitudeZ, 5), Rounding.ToSignificantDigits(result.MagnitudeZ, 5), 2);
        }
    }
}