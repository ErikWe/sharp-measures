
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility
{
    public static class AssertExtra
    {
        public static void AssertQuantitiesEqualValue(IQuantity x, IQuantity y)
        {
            Assert.Equal(x.Magnitude.Magnitude, y.Magnitude.Magnitude, 2);
        }

        public static void AssertQuantitiesEqualValue(IQuantity2 a, IQuantity2 b)
        {
            Assert.Equal(a.MagnitudeX.Magnitude, b.MagnitudeX.Magnitude, 2);
            Assert.Equal(a.MagnitudeY.Magnitude, b.MagnitudeY.Magnitude, 2);
        }

        public static void AssertQuantitiesEqualValue(IQuantity3 a, IQuantity3 b)
        {
            Assert.Equal(a.MagnitudeX.Magnitude, b.MagnitudeX.Magnitude, 2);
            Assert.Equal(a.MagnitudeY.Magnitude, b.MagnitudeY.Magnitude, 2);
            Assert.Equal(a.MagnitudeZ.Magnitude, b.MagnitudeZ.Magnitude, 2);
        }
    }
}
