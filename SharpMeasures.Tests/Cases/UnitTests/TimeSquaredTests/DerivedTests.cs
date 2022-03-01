namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.TimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class DerivedTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void TwoUnitOfTime_ShouldBeProduct(Scalar scale1, Scalar scale2)
    {
        UnitOfTime unit1 = UnitOfTime.Second.ScaledBy(scale1);
        UnitOfTime unit2 = UnitOfTime.Second.ScaledBy(scale2);

        UnitOfTimeSquared unitOfTimeSquared = UnitOfTimeSquared.From(unit1, unit2);

        Assert.Equal(scale1 * scale2, unitOfTimeSquared.TimeSquared.Magnitude, 2);
    }
}
