#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TranslationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void Absolute(TranslationalKineticEnergy quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void Floor(TranslationalKineticEnergy quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void Ceiling(TranslationalKineticEnergy quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void Round(TranslationalKineticEnergy quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
