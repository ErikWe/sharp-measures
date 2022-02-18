namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

public class PropertiesTests
{
    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsNaN(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsNaN_ShouldMatchDouble(scalar, scalar.IsNaN);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsZero(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsZero_ShouldBeTrueWhenZero(scalar, scalar.IsZero);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsPositive(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsPositive_ShouldBeTrueWhenLargerThanZero(scalar, scalar.IsPositive);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void ISNegative(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsNegative_ShouldMatchDouble(scalar, scalar.IsNegative);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsFinite(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsFinite_ShouldMatchDouble(scalar, scalar.IsFinite);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsInfinity(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsInfinity_ShouldMatchDouble(scalar, scalar.IsInfinite);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsPositiveInfinity(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsPositiveInfinity_ShouldMatchDouble(scalar, scalar.IsPositiveInfinity);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void IsNegativeInfinity(Scalar scalar)
    {
        Utility.QuantityTests.PropertiesTests.IsNegativeInfinity_ShouldMatchDouble(scalar, scalar.IsNegativeInfinity);
    }
}
