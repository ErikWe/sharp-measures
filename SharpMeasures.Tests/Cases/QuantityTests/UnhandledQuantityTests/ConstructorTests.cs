using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledQuantityTests
{
    public class ConstructorTests
    {
        [Fact]
        public void Zero_MagnitudeShouldBeZero()
        {
            Unhandled quantity = Unhandled.Zero;

            Assert.Equal(0, quantity.Magnitude);
        }

        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Scalar_MagnitudeShouldBeEqual(Scalar scalar)
        {
            Unhandled quantity = new(scalar);

            Assert.Equal(scalar, quantity.Magnitude);
        }

        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void IQuantity_MagnitudeShouldBeEqual(Scalar scalar)
        {
            IScalarQuantity iquantity = new UnhandledScalar(scalar);
            Unhandled quantity = Unhandled.FromQuantity(iquantity);

            Assert.Equal(scalar, quantity.Magnitude);
        }
    }
}
