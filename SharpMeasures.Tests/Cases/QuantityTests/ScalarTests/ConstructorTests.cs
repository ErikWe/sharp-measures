using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests
{
    public class ConstructorTests
    {
        [Fact]
        public void Zero_MagnitudeShouldBeZero()
        {
            Scalar quantity = Scalar.Zero;

            Assert.Equal(0, quantity, 2);
        }

        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Double_MagnitudeShouldBeEqualToDouble(Scalar a)
        {
            Scalar quantity = new(a.Magnitude);

            Assert.Equal(a, quantity, 2);
        }
    }
}
