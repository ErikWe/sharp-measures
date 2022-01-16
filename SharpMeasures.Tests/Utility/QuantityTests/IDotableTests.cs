
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class IDotableTests
    {
        public static void IQuantity2_ShouldBeSumOfSquares<TResultQuantity, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherQuantity quantity2)
            where TResultQuantity : IQuantity
            where TOtherQuantity : IQuantity2
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>, IDotableQuantity2<TResultQuantity, TOtherQuantity>
        {
            TResultQuantity result = quantity1.Dot(quantity2);

            Assert.Equal(quantity1.MagnitudeX * quantity2.MagnitudeX + quantity1.MagnitudeY * quantity2.MagnitudeY, result.Magnitude, 2);
        }

        public static void IQuantity3_ShouldBeSumOfSquares<TResultQuantity, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherQuantity quantity2)
            where TResultQuantity : IQuantity
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>, IDotableQuantity3<TResultQuantity, TOtherQuantity>
        {
            TResultQuantity result = quantity1.Dot(quantity2);

            Assert.Equal(quantity1.MagnitudeX * quantity2.MagnitudeX + quantity1.MagnitudeY * quantity2.MagnitudeY + quantity1.MagnitudeZ * quantity2.MagnitudeZ, result.Magnitude, 2);
        }
    }
}
