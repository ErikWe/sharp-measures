#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificVolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsNaN(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(quantity, quantity.IsNaN);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsZero(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(quantity, quantity.IsZero);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsPositive(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(quantity, quantity.IsPositive);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void ISNegative(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(quantity, quantity.IsNegative);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsFinite(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(quantity, quantity.IsFinite);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsInfinity(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(quantity, quantity.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsPositiveInfinity(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(quantity, quantity.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(SpecificVolumeDataset))]
    public void IsNegativeInfinity(SpecificVolume quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(quantity, quantity.IsNegativeInfinity);
    }
}
