namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Absolute(Unhandled quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Floor(Unhandled quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Ceiling(Unhandled quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Round(Unhandled quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
