
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class SquaredLengthTests
    {
        public static void ShouldBeSumOfSquares<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TComponent result = a.Length();

            Assert.Equal(a.MagnitudeX.Power(2) + a.MagnitudeY.Power(2), result.Magnitude, 2);
        }

        public static void ShouldBeSquareRootOfSumOfSquares<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a)
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TComponent result = a.Length();

            Assert.Equal(a.MagnitudeX.Power(2) + a.MagnitudeY.Power(2) + a.MagnitudeZ.Power(2), result.Magnitude, 2);
        }
    }
}
