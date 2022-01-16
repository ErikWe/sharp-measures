using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class CrossScalarTests
    {
        public static void ShouldMatchDefinition<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, Scalar3 b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Cross(b);

            Assert.Equal(a.MagnitudeY * b.Z - a.MagnitudeZ * b.Y, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeZ * b.X - a.MagnitudeX * b.Z, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeX * b.Y - a.MagnitudeY * b.X, result.MagnitudeZ, 2);
        }
    }
}
