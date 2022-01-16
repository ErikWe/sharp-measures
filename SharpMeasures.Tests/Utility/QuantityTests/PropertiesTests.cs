
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class PropertiesTests
    {
        public static void IsNaN_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsNaN(quantity.Magnitude), quantity.IsNaN);
        }

        public static void IsZero_ShouldBeTrueWhenZero(IQuantity quantity)
        {
            Assert.Equal(quantity.Magnitude == 0, quantity.IsZero);
        }

        public static void IsPositive_ShouldBeTrueWhenLargerThanZero(IQuantity quantity)
        {
            Assert.Equal(quantity.Magnitude > 0, quantity.IsPositive);
        }

        public static void IsNegative_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsNegative(quantity.Magnitude), quantity.IsNegative);
        }

        public static void IsFinite_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsFinite(quantity.Magnitude), quantity.IsFinite);
        }

        public static void IsInfinity_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsInfinity(quantity.Magnitude), quantity.IsInfinity);
        }

        public static void IsPositiveInfinity_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsPositiveInfinity(quantity.Magnitude), quantity.IsPositiveInfinity);
        }

        public static void IsNegativeInfinity_ShouldMatchDouble(IQuantity quantity)
        {
            Assert.Equal(double.IsNegativeInfinity(quantity.Magnitude), quantity.IsNegativeInfinity);
        }
    }
}
