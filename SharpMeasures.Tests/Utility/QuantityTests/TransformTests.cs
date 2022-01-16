using ErikWe.SharpMeasures.Quantities;

using System.Numerics;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class TransformTests
    {
        public static void Identity_ShouldBeUnaltered<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Transform(Matrix4x4.Identity);

            Assert.Equal(a.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Translation_ShouldMatchDefinition<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            Matrix4x4 matrix = new(1, 0, 0, (float)b.X, 0, 1, 0, (float)b.Y, 0, 0, 1, (float)b.Z, 0, 0, 0, 1);

            TQuantity result = a.Transform(matrix);

            Assert.Equal(a.MagnitudeX + b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY + b.Y, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ + b.Z, result.MagnitudeZ, 2);
        }

        public static void Scaling_ShouldMatchDefinition<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            Matrix4x4 matrix = new((float)b.X, 0, 0, 0, 0, (float)b.Y, 0, 0, 0, 0, (float)b.Z, 0, 0, 0, 0, 1);

            TQuantity result = a.Transform(matrix);

            Assert.Equal(a.MagnitudeX * b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b.Y, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ * b.Z, result.MagnitudeZ, 2);
        }
    }
}
