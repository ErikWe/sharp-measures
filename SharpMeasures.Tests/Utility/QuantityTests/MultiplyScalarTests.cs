using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class MultiplyScalarTests
    {
        public static void Method_ShouldEqualScalar<TQuantity>(IScalarQuantity<TQuantity> a, Scalar b)
            where TQuantity : IScalarQuantity
        {
            TQuantity result1 = a.Multiply(b);
            TQuantity result2 = a.MultiplyInto(b);

            Assert.Equal(a.Magnitude * b, result1.Magnitude, 2);
            Assert.Equal(b * a.Magnitude, result2.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, Scalar2 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.MultiplyElementwise(b);
            TQuantity result2 = a.MultiplyIntoElementwise(b);

            Assert.Equal(a.MagnitudeX * b.X, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b.Y, result1.MagnitudeY, 2);
            Assert.Equal(b.X * a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.Y * a.MagnitudeY, result2.MagnitudeY, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, Scalar b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.Multiply(b);
            TQuantity result2 = a.MultiplyInto(b);

            Assert.Equal(a.MagnitudeX * b, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b, result1.MagnitudeY, 2);
            Assert.Equal(b * a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b * a.MagnitudeY, result2.MagnitudeY, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.MultiplyElementwise(b);
            TQuantity result2 = a.MultiplyIntoElementwise(b);

            Assert.Equal(a.MagnitudeX * b.X, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b.Y, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ * b.Z, result1.MagnitudeZ, 2);
            Assert.Equal(b.X * a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.Y * a.MagnitudeY, result2.MagnitudeY, 2);
            Assert.Equal(b.Z * a.MagnitudeZ, result2.MagnitudeZ, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.Multiply(b);
            TQuantity result2 = a.MultiplyInto(b);

            Assert.Equal(a.MagnitudeX * b, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ * b, result1.MagnitudeZ, 2);
            Assert.Equal(b * a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b * a.MagnitudeY, result2.MagnitudeY, 2);
            Assert.Equal(b * a.MagnitudeZ, result2.MagnitudeZ, 2);
        }

        public static void Operator_ShouldEqualScalar(IScalarQuantity a, Scalar b, IScalarQuantity result, IScalarQuantity inverseResult)
        {
            Assert.Equal(a.Magnitude * b, result.Magnitude, 2);
            Assert.Equal(b * a.Magnitude, inverseResult.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, Scalar2 b, IQuantity2 result, IQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX * b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b.Y, result.MagnitudeY, 2);
            Assert.Equal(b.X * a.MagnitudeX, inverseResult.MagnitudeX, 2);
            Assert.Equal(b.Y * a.MagnitudeY, inverseResult.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, Scalar b, IQuantity2 result, IQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX * b, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b, result.MagnitudeY, 2);
            Assert.Equal(b * a.MagnitudeX, inverseResult.MagnitudeX, 2);
            Assert.Equal(b * a.MagnitudeY, inverseResult.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, Scalar3 b, IQuantity3 result, IQuantity3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX * b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b.Y, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ * b.Z, result.MagnitudeZ, 2);
            Assert.Equal(b.X * a.MagnitudeX, inverseResult.MagnitudeX, 2);
            Assert.Equal(b.Y * a.MagnitudeY, inverseResult.MagnitudeY, 2);
            Assert.Equal(b.Z * a.MagnitudeZ, inverseResult.MagnitudeZ, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, Scalar b, IQuantity3 result, IQuantity3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX * b, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY * b, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ * b, result.MagnitudeZ, 2);
            Assert.Equal(b * a.MagnitudeX, inverseResult.MagnitudeX, 2);
            Assert.Equal(b * a.MagnitudeY, inverseResult.MagnitudeY, 2);
            Assert.Equal(b * a.MagnitudeZ, inverseResult.MagnitudeZ, 2);
        }
    }
}
