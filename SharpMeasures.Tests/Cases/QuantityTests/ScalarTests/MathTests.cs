using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests
{
    public class MathTests
    {
        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Abs_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Abs(scalar.Magnitude), scalar.Abs(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Floor_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Floor(scalar.Magnitude), scalar.Floor(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Ceiling_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Ceiling(scalar.Magnitude), scalar.Ceiling(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Round_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Round(scalar.Magnitude), scalar.Round(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Sqrt_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Sqrt(scalar.Magnitude), scalar.Sqrt(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Square_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(Math.Pow(scalar.Magnitude, 2), scalar.Square(), 2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Pow_ShouldMatchDouble(Scalar scalar)
        {
            double[] exponents = new double[] { 3, -5.56, 0.003, 1, 0, -2 };

            foreach (double exponent in exponents)
            {
                Assert.Equal(Math.Pow(scalar.Magnitude, exponent), scalar.Pow(exponent), 2);
            }
        }
    }
}
