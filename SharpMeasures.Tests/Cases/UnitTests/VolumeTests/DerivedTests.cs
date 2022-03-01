namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.VolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class DerivedTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void ThreeUnitOfLength_ShouldBeProduct(Scalar scale1, Scalar scale2, Scalar scale3)
    {
        UnitOfLength unit1 = UnitOfLength.Metre.ScaledBy(scale1);
        UnitOfLength unit2 = UnitOfLength.Metre.ScaledBy(scale2);
        UnitOfLength unit3 = UnitOfLength.Metre.ScaledBy(scale3);

        UnitOfVolume unitOfVolume = UnitOfVolume.From(unit1, unit2, unit3);

        Assert.Equal(scale1 * scale2 * scale3, unitOfVolume.Volume.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void UnitOfAreaUnitOfLength_ShouldBeProduct(Scalar scale1, Scalar scale2)
    {
        UnitOfArea unit1 = UnitOfArea.SquareMetre.ScaledBy(scale1);
        UnitOfLength unit2 = UnitOfLength.Metre.ScaledBy(scale2);

        UnitOfVolume unitOfVolume = UnitOfVolume.From(unit1, unit2);

        Assert.Equal(scale1 * scale2, unitOfVolume.Volume.Magnitude, 2);
    }
}
