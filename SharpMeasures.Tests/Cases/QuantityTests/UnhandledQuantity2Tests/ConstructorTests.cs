using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledQuantity2Tests
{
    public class ConstructorTests
    {
        [Fact]
        public void Zero_MagnitudeShouldBeZero()
        {
            UnhandledQuantity2 quantity = UnhandledQuantity2.Zero;

            Assert.Equal(0, quantity.Length().Magnitude);
        }

        [Theory]
        [ClassData(typeof(Scalar2Dataset))]
        public void Scalar2_MagnitudeShouldBeEqual(Scalar2 scalar)
        {
            UnhandledQuantity2 quantity = new(scalar);

            Assert.Equal(scalar.X, quantity.X.Magnitude);
            Assert.Equal(scalar.Y, quantity.Y.Magnitude);
        }

        [Theory]
        [ClassData(typeof(Scalar2Dataset))]
        public void IQuantity2_MagnitudeShouldBeEqual(Scalar2 scalar)
        {
            IQuantity2 iquantity = new Length2(scalar);
            UnhandledQuantity2 quantity = UnhandledQuantity2.FromQuantity(iquantity);

            Assert.Equal(scalar.X, quantity.X.Magnitude);
            Assert.Equal(scalar.Y, quantity.Y.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
        public void Scalar_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2)
        {
            UnhandledQuantity2 quantity = new(scalar1, scalar2);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
        public void IQuantity_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2)
        {
            IScalarQuantity iquantity1 = new UnhandledScalar(scalar1);
            IScalarQuantity iquantity2 = new UnhandledScalar(scalar2);
            UnhandledQuantity2 quantity = UnhandledQuantity2.FromQuantities(iquantity1, iquantity2);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
        public void UnhandledQuantity_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2)
        {
            Unhandled quantity1 = new(scalar1);
            Unhandled quantity2 = new(scalar2);
            UnhandledQuantity2 quantity = new(quantity1, quantity2);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
        }
    }
}
