#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SolidAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(SolidAngleDataset))]
    public void Absolute(SolidAngle quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(SolidAngleDataset))]
    public void Floor(SolidAngle quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(SolidAngleDataset))]
    public void Ceiling(SolidAngle quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(SolidAngleDataset))]
    public void Round(SolidAngle quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
