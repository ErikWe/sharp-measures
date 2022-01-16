
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class NormalizeTests
    {
        public static void ShouldBeLengthOne<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a)
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Normalize();

            Assert.Equal(1, result.Length().Magnitude, 2);
        }
    }
}
