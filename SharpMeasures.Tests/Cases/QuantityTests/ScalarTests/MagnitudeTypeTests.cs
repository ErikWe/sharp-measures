using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests
{
    public class MagnitudeTypeTests
    {
        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsNaN_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsNaN, double.IsNaN(scalar.Magnitude));
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsZero_ShouldBeTrueWhenZero(Scalar scalar)
        {
            Assert.Equal(scalar.IsZero, scalar.Magnitude == 0);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsPositive_ShouldBeTrueWhenGreaterThanZero(Scalar scalar)
        {
            Assert.Equal(scalar.IsPositive, scalar.Magnitude > 0);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsNegative_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsNegative, double.IsNegative(scalar.Magnitude));
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsFinite_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsFinite, double.IsFinite(scalar.Magnitude));
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsInfinity_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsInfinity, double.IsInfinity(scalar.Magnitude));
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsPositiveInfinity_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsPositiveInfinity, double.IsPositiveInfinity(scalar.Magnitude));
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void IsNegativeInfinity_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(scalar.IsNegativeInfinity, double.IsNegativeInfinity(scalar.Magnitude));
        }
    }
}
