#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsNaN(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(quantity, quantity.IsNaN);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsZero(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(quantity, quantity.IsZero);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsPositive(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(quantity, quantity.IsPositive);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void ISNegative(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(quantity, quantity.IsNegative);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsFinite(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(quantity, quantity.IsFinite);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsInfinity(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(quantity, quantity.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsPositiveInfinity(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(quantity, quantity.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void IsNegativeInfinity(SpinAngularMomentum quantity)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(quantity, quantity.IsNegativeInfinity);
    }
}
