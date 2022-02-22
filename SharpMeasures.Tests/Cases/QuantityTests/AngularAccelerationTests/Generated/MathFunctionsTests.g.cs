#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void Absolute(AngularAcceleration quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void Floor(AngularAcceleration quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void Ceiling(AngularAcceleration quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void Round(AngularAcceleration quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
