
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class DivideUnhandledTests
    {
        public static void Method_ShouldEqualScalar<TQuantity>(IQuantity<TQuantity> a, UnhandledQuantity b)
            where TQuantity : IQuantity
        {
            UnhandledQuantity result1 = a.Divide(b);
            UnhandledQuantity result2 = a.DivideInto(b);

            Assert.Equal(a.Magnitude / b.Magnitude, result1.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.Magnitude, result2.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, UnhandledQuantity2 b)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            UnhandledQuantity2 result1 = a.DivideElementwise(b);
            UnhandledQuantity2 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.X.Magnitude, result1.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Y.Magnitude, result1.Y.Magnitude, 2);
            Assert.Equal(b.X.Magnitude / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Y.Magnitude / a.MagnitudeY, result2.Y.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, UnhandledQuantity b)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            UnhandledQuantity2 result1 = a.Divide(b);
            UnhandledQuantity2 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b.Magnitude, result1.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result1.Y.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, result2.Y.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, UnhandledQuantity3 b)
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            UnhandledQuantity3 result1 = a.DivideElementwise(b);
            UnhandledQuantity3 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.X.Magnitude, result1.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Y.Magnitude, result1.Y.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ / b.Z.Magnitude, result1.Z.Magnitude, 2);
            Assert.Equal(b.X.Magnitude / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Y.Magnitude / a.MagnitudeY, result2.Y.Magnitude, 2);
            Assert.Equal(b.Z.Magnitude / a.MagnitudeZ, result2.Z.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, UnhandledQuantity b)
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            UnhandledQuantity3 result1 = a.Divide(b);
            UnhandledQuantity3 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b.Magnitude, result1.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result1.Y.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ / b.Magnitude, result1.Z.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, result2.Y.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeZ, result2.Z.Magnitude, 2);
        }

        public static void Operator_ShouldEqualScalar(IQuantity a, UnhandledQuantity b, UnhandledQuantity result, UnhandledQuantity inverseResult)
        {
            Assert.Equal(a.Magnitude / b.Magnitude, result.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.Magnitude, inverseResult.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, UnhandledQuantity2 b, UnhandledQuantity2 result, UnhandledQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.X.Magnitude, result.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Y.Magnitude, result.Y.Magnitude, 2);
            Assert.Equal(b.X.Magnitude / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Y.Magnitude / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, UnhandledQuantity b, UnhandledQuantity2 result, UnhandledQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.Magnitude, result.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result.Y.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, UnhandledQuantity3 b, UnhandledQuantity3 result, UnhandledQuantity3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.X.Magnitude, result.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Y.Magnitude, result.Y.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ / b.Z.Magnitude, result.Z.Magnitude, 2);
            Assert.Equal(b.X.Magnitude / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Y.Magnitude / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
            Assert.Equal(b.Z.Magnitude / a.MagnitudeZ, inverseResult.Z.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, UnhandledQuantity b, UnhandledQuantity3 result, UnhandledQuantity3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.Magnitude, result.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeY / b.Magnitude, result.Y.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ / b.Magnitude, result.Z.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
            Assert.Equal(b.Magnitude / a.MagnitudeZ, inverseResult.Z.Magnitude, 2);
        }
    }
}
