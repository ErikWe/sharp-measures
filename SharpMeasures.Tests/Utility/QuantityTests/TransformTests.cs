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

        Assert.Equal(a.X, result.X, 2);
        Assert.Equal(a.Y, result.Y, 2);
        Assert.Equal(a.Z, result.Z, 2);
    }

    public static void Translation_ShouldBeOffsetByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new(1, 0, 0, (float)b.X, 0, 1, 0, (float)b.Y, 0, 0, 1, (float)b.Z, 0, 0, 0, 1);

        TQuantity result = a.Transform(matrix);

        Assert.Equal(a.X + b.X, result.X, 2);
        Assert.Equal(a.Y + b.Y, result.Y, 2);
        Assert.Equal(a.Z + b.Z, result.Z, 2);
    }

    public static void Scaling_ShouldBeScaledByBComponentwise<TQuantity>(TQuantity a, Quantities.Vector3 b)
        where TQuantity : ITransformableVector3Quantity<TQuantity>
    {
        Matrix4x4 matrix = new((float)b.X, 0, 0, 0, 0, (float)b.Y, 0, 0, 0, 0, (float)b.Z, 0, 0, 0, 0, 1);

        TQuantity result = a.Transform(matrix);

        Assert.Equal(a.X * b.X, result.X, 2);
        Assert.Equal(a.Y * b.Y, result.Y, 2);
        Assert.Equal(a.Z * b.Z, result.Z, 2);
    }
}