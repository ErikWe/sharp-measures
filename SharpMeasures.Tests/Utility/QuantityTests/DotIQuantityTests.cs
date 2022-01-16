
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class DotIQuantityTests
    {
        public static void ShouldBeSumOfSquares<TOtherQuantity, TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TOtherQuantity b)
            where TOtherQuantity : IQuantity2
            where TComponent : IQuantity
            where TQuantity : IQuantity2
        {
            UnhandledQuantity result1 = a.Dot(b);

            Assert.Equal(a.MagnitudeX * b.MagnitudeX + a.MagnitudeY * b.MagnitudeY, result1.Magnitude, 2);
        }

        public static void ShouldBeSumOfSquares<TOtherQuantity, TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TOtherQuantity b)
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3
        {
            UnhandledQuantity result1 = a.Dot(b);

            Assert.Equal(a.MagnitudeX * b.MagnitudeX + a.MagnitudeY * b.MagnitudeY + a.MagnitudeZ * b.MagnitudeZ, result1.Magnitude, 2);
        }
    }
}
