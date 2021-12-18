using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Units;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngleTests
{
    public class UnitOfAngleTests
    {
        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Radian_MagnitudeShouldBeAsSpecified(Scalar scalar)
        {
            Angle angle = new(scalar, UnitOfAngle.Radian);

            Assert.Equal(scalar, angle.Radians);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Degree_MagnitudeShouldBeAsSpecified(Scalar scalar)
        {
            Angle angle = new(scalar, UnitOfAngle.Degree);

            Assert.Equal(scalar, angle.Degrees);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void ArcMinute_MagnitudeShouldBeAsSpecified(Scalar scalar)
        {
            Angle angle = new(scalar, UnitOfAngle.ArcMinute);

            Assert.Equal(scalar, angle.ArcMinutes);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void ArcSecond_MagnitudeShouldBeAsSpecified(Scalar scalar)
        {
            Angle angle = new(scalar, UnitOfAngle.ArcSecond);

            Assert.Equal(scalar, angle.ArcSeconds);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Turn_MagnitudeShouldBeAsSpecified(Scalar scalar)
        {
            Angle angle = new(scalar, UnitOfAngle.Turn);

            Assert.Equal(scalar, angle.Turns);
        }
    }
}
