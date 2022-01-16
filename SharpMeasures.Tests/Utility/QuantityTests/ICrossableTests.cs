
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class ICrossableTests
    {
        public static void ShouldMatchDefinition<TResultQuantity, TOtherQuantity, TComponent, TQuantity>(TQuantity a, TOtherQuantity b)
            where TResultQuantity : IQuantity3
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>, ICrossableQuantity3<TResultQuantity, TOtherQuantity>
        {
            TResultQuantity result = a.Cross(b);

            Assert.Equal(a.MagnitudeY * b.MagnitudeZ - a.MagnitudeZ * b.MagnitudeY, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeZ * b.MagnitudeX - a.MagnitudeX * b.MagnitudeZ, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeX * b.MagnitudeY - a.MagnitudeY * b.MagnitudeX, result.MagnitudeZ, 2);
        }
    }
}
