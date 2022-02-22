#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void Absolute(MassFlowRate quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void Floor(MassFlowRate quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void Ceiling(MassFlowRate quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void Round(MassFlowRate quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
