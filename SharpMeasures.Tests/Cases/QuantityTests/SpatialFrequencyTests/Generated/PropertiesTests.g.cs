#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsNaN(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(quantity, quantity.IsNaN);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsZero(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(quantity, quantity.IsZero);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsPositive(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(quantity, quantity.IsPositive);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void ISNegative(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(quantity, quantity.IsNegative);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsFinite(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(quantity, quantity.IsFinite);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsInfinity(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(quantity, quantity.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsPositiveInfinity(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(quantity, quantity.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void IsNegativeInfinity(SpatialFrequency quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(quantity, quantity.IsNegativeInfinity);
    }
}
