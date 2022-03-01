namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.VelocitySquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class DerivedTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void TwoUnitOfVelocity_ShouldBeProduct(Scalar scale1, Scalar scale2)
    {
        UnitOfVelocity unit1 = UnitOfVelocity.MetrePerSecond.ScaledBy(scale1);
        UnitOfVelocity unit2 = UnitOfVelocity.MetrePerSecond.ScaledBy(scale2);

        UnitOfVelocitySquared unitOfVelocitySquared = UnitOfVelocitySquared.From(unit1, unit2);

        Assert.Equal(scale1 * scale2, unitOfVelocitySquared.SpeedSquared.Magnitude, 2);
    }
}
