
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class RemainderTests
    {
        public static void Method_ShouldEqualScalar<TQuantity>(IQuantity<TQuantity> a, TQuantity b)
            where TQuantity : IQuantity
        {
            IQuantity result = a.Remainder(b);

            Assert.Equal(a.Magnitude % b.Magnitude, result.Magnitude, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Remainder(b);

            Assert.Equal(a.MagnitudeX % b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.MagnitudeY, result.MagnitudeY, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TComponent b)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Remainder(b);

            Assert.Equal(a.MagnitudeX % b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.Magnitude, result.MagnitudeY, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Remainder(b);

            Assert.Equal(a.MagnitudeX % b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ % b.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Method_ComponentsShouldEqualScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TComponent b)
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Remainder(b);

            Assert.Equal(a.MagnitudeX % b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ % b.Magnitude, result.MagnitudeZ, 2);
        }

        public static void Operator_ShouldEqualScalar(IQuantity a, IQuantity b, IQuantity result)
        {
            Assert.Equal(a.Magnitude % b.Magnitude, result.Magnitude, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, IQuantity2 b, IQuantity2 result)
        {
            Assert.Equal(a.MagnitudeX % b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.MagnitudeY, result.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity2 a, IQuantity b, IQuantity2 result)
        {
            Assert.Equal(a.MagnitudeX % b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.Magnitude, result.MagnitudeY, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, IQuantity3 b, IQuantity3 result)
        {
            Assert.Equal(a.MagnitudeX % b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ % b.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Operator_ComponentsShouldEqualScalar(IQuantity3 a, IQuantity b, IQuantity3 result)
        {
            Assert.Equal(a.MagnitudeX % b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY % b.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ % b.Magnitude, result.MagnitudeZ, 2);
        }
    }
}
