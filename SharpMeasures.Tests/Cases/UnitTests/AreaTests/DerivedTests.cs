namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.AreaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class DerivedTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void TwoUnitOfLength_ShouldBeProduct(Scalar scale1, Scalar scale2)
    {
        UnitOfLength unit1 = UnitOfLength.Metre.ScaledBy(scale1);
        UnitOfLength unit2 = UnitOfLength.Metre.ScaledBy(scale2);

        UnitOfArea unitOfArea = UnitOfArea.From(unit1, unit2);

        Assert.Equal(scale1 * scale2, unitOfArea.Area.Magnitude, 2);
    }
}
