#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void Absolute(MomentOfInertia quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(quantity, quantity.Absolute());
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void Floor(MomentOfInertia quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(quantity, quantity.Floor());
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void Ceiling(MomentOfInertia quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(quantity, quantity.Ceiling());
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void Round(MomentOfInertia quantity)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(quantity, quantity.Round());
    }
}
