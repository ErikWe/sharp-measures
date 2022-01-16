using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class DivideScalarTests
    {
        public static void Method_ShouldEqualScalar<TQuantity>(IScalarQuantity<TQuantity> a, Scalar b)
            where TQuantity : IScalarQuantity
        {
            TQuantity result1 = a.DivideBy(b);
            Unhandled result2 = a.DivideInto(b);

            Assert.Equal(a.Magnitude / b, result1.Magnitude, 2);
            Assert.Equal(b / a.Magnitude, result2.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, Scalar2 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.DivideElementwise(b);
            UnhandledQuantity2 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.X, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Y, result1.MagnitudeY, 2);
            Assert.Equal(b.X / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Y / a.MagnitudeY, result2.Y.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, Scalar b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.Divide(b);
            UnhandledQuantity2 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b, result1.MagnitudeY, 2);
            Assert.Equal(b / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeY, result2.Y.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.DivideElementwise(b);
            Unhandled3 result2 = a.DivideIntoElementwise(b);

            Assert.Equal(a.MagnitudeX / b.X, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Y, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b.Z, result1.MagnitudeZ, 2);
            Assert.Equal(b.X / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b.Y / a.MagnitudeY, result2.Y.Magnitude, 2);
            Assert.Equal(b.Z / a.MagnitudeZ, result2.Z.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.Divide(b);
            Unhandled3 result2 = a.DivideInto(b);

            Assert.Equal(a.MagnitudeX / b, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b, result1.MagnitudeZ, 2);
            Assert.Equal(b / a.MagnitudeX, result2.X.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeY, result2.Y.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeZ, result2.Z.Magnitude, 2);
        }

        public static void Operator_ShouldEqualScalar(IScalarQuantity a, Scalar b, IScalarQuantity result, Unhandled inverseResult)
        {
            Assert.Equal(a.Magnitude / b, result.Magnitude, 2);
            Assert.Equal(b / a.Magnitude, inverseResult.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, Scalar2 b, IQuantity2 result, UnhandledQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Y, result.MagnitudeY, 2);
            Assert.Equal(b.X / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Y / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, Scalar b, IQuantity2 result, UnhandledQuantity2 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b, result.MagnitudeY, 2);
            Assert.Equal(b / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, Scalar3 b, IQuantity3 result, Unhandled3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b.X, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b.Y, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b.Z, result.MagnitudeZ, 2);
            Assert.Equal(b.X / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b.Y / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
            Assert.Equal(b.Z / a.MagnitudeZ, inverseResult.Z.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, Scalar b, IQuantity3 result, Unhandled3 inverseResult)
        {
            Assert.Equal(a.MagnitudeX / b, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY / b, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ / b, result.MagnitudeZ, 2);
            Assert.Equal(b / a.MagnitudeX, inverseResult.X.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeY, inverseResult.Y.Magnitude, 2);
            Assert.Equal(b / a.MagnitudeZ, inverseResult.Z.Magnitude, 2);
        }
    }
}
