using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class DivideSameTypeTests
    {
        public static void Method_ShouldEqualScalar<TQuantity>(IScalarQuantity<TQuantity> a, TQuantity b)
            where TQuantity : IScalarQuantity
        {
            Scalar result1 = a.DivideBy(b);
            Scalar result2 = a.DivideInto(b);

            Assert.Equal(a.Magnitude / b.Magnitude, result1.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.Magnitude, result2.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            Scalar2 result1 = a.DivideElementwise(b);
            Scalar2 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.MagnitudeX, result1.X, 2);
            Assert.Equal(a.MagnitudeY / b.MagnitudeY, result1.Y, 2);
            Assert.Equal(b.MagnitudeX / a.MagnitudeX, result2.X, 2);
            Assert.Equal(b.MagnitudeY / a.MagnitudeY, result2.Y, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            Scalar2 result1 = a.Divide(b);
            Scalar2 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b.Magnitude, result1.X, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result1.Y, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, result2.X, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, result2.Y, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            Scalar3 result1 = a.DivideElementwise(b);
            Scalar3 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.MagnitudeX, result1.X, 2);
            Assert.Equal(a.MagnitudeY / b.MagnitudeY, result1.Y, 2);
            Assert.Equal(a.MagnitudeZ / b.MagnitudeZ, result1.Z, 2);
            Assert.Equal(b.MagnitudeX / a.MagnitudeX, result2.X, 2);
            Assert.Equal(b.MagnitudeY / a.MagnitudeY, result2.Y, 2);
            Assert.Equal(b.MagnitudeZ / a.MagnitudeZ, result2.Z, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            Scalar3 result1 = a.Divide(b);
            Scalar3 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b.Magnitude, result1.X, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result1.Y, 2);
            Assert.Equal(a.MagnitudeZ / b.Magnitude, result1.Z, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, result2.X, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, result2.Y, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeZ, result2.Z, 2);
        }

        public static void Operator_ShouldEqualScalar(IScalarQuantity a, IScalarQuantity b, IScalarQuantity result)
        {
            Assert.Equal(a.Magnitude / b.Magnitude, result.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, IQuantity2 b, IQuantity2 result)
        {
            Assert.Equal(a.MagnitudeX / b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.MagnitudeY, result.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, IScalarQuantity b, IQuantity2 result)
        {
            Assert.Equal(a.MagnitudeX / b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, IQuantity3 b, IQuantity3 result)
        {
            Assert.Equal(a.MagnitudeX / b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, IScalarQuantity b, IQuantity3 result)
        {
            Assert.Equal(a.MagnitudeX / b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b.Magnitude, result.MagnitudeZ, 2);
        }
    }
}
