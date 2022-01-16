
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class CrossIQuantityTests
    {
        public static void ShouldMatchDefinition<TOtherQuantity, TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TOtherQuantity b)
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            UnhandledQuantity3 result = a.Cross(b);

            Assert.Equal(a.MagnitudeY * b.MagnitudeZ - a.MagnitudeZ * b.MagnitudeY, result.X.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ * b.MagnitudeX - a.MagnitudeX * b.MagnitudeZ, result.Y.Magnitude, 2);
            Assert.Equal(a.MagnitudeX * b.MagnitudeY - a.MagnitudeY * b.MagnitudeX, result.Z.Magnitude, 2);
        }
    }
}
