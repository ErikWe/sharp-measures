namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.SolidAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class DerivedTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void TwoUnitOfAngle_ShouldBeProduct(Scalar scale1, Scalar scale2)
    {
        UnitOfAngle unit1 = UnitOfAngle.Radian.ScaledBy(scale1);
        UnitOfAngle unit2 = UnitOfAngle.Radian.ScaledBy(scale2);

        UnitOfSolidAngle unitOfSolidAngle = UnitOfSolidAngle.From(unit1, unit2);

        Assert.Equal(scale1 * scale2, unitOfSolidAngle.SolidAngle.Magnitude, 2);
    }
}
