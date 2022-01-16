using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class DotScalarTests
    {
        public static void ShouldBeSumOfSquares<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, Scalar2 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2
        {
            TComponent result1 = a.Dot(b);

            Assert.Equal(a.MagnitudeX * b.X + a.MagnitudeY * b.Y, result1.Magnitude, 2);
        }

        public static void ShouldBeSumOfSquares<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3
        {
            TComponent result1 = a.Dot(b);

            Assert.Equal(a.MagnitudeX * b.X + a.MagnitudeY * b.Y + a.MagnitudeZ * b.Z, result1.Magnitude, 2);
        }
    }
}
