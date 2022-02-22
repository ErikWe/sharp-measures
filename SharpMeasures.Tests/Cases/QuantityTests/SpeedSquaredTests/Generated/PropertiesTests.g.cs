#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsNaN(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(quantity, quantity.IsNaN);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsZero(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(quantity, quantity.IsZero);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsPositive(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(quantity, quantity.IsPositive);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void ISNegative(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(quantity, quantity.IsNegative);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsFinite(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(quantity, quantity.IsFinite);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsInfinity(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(quantity, quantity.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsPositiveInfinity(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(quantity, quantity.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void IsNegativeInfinity(SpeedSquared quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(quantity, quantity.IsNegativeInfinity);
    }
}
