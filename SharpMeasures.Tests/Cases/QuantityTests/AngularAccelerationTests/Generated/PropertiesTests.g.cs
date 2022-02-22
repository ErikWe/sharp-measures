#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsNaN(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(quantity, quantity.IsNaN);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsZero(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(quantity, quantity.IsZero);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsPositive(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(quantity, quantity.IsPositive);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void ISNegative(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(quantity, quantity.IsNegative);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsFinite(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(quantity, quantity.IsFinite);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsInfinity(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(quantity, quantity.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsPositiveInfinity(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(quantity, quantity.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void IsNegativeInfinity(AngularAcceleration quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(quantity, quantity.IsNegativeInfinity);
    }
}
