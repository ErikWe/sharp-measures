using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledQuantity3Tests
{
    public class ConstructorTests
    {
        [Fact]
        public void Zero_MagnitudeShouldBeZero()
        {
            Unhandled3 quantity = Unhandled3.Zero;

            Assert.Equal(0, quantity.Length().Magnitude);
        }

        [Theory]
        [ClassData(typeof(Scalar3Dataset))]
        public void Scalar3_MagnitudeShouldBeEqual(Scalar3 scalar)
        {
            Unhandled3 quantity = new(scalar);

            Assert.Equal(scalar.X, quantity.X.Magnitude);
            Assert.Equal(scalar.Y, quantity.Y.Magnitude);
            Assert.Equal(scalar.Z, quantity.Z.Magnitude);
        }

        [Theory]
        [ClassData(typeof(Scalar3Dataset))]
        public void IQuantity3_MagnitudeShouldBeEqual(Scalar3 scalar)
        {
            IQuantity3 iquantity = new Length3(scalar);
            Unhandled3 quantity = Unhandled3.FromQuantity(iquantity);

            Assert.Equal(scalar.X, quantity.X.Magnitude);
            Assert.Equal(scalar.Y, quantity.Y.Magnitude);
            Assert.Equal(scalar.Z, quantity.Z.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
        public void Scalar_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2, Scalar scalar3)
        {
            Unhandled3 quantity = new(scalar1, scalar2, scalar3);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
            Assert.Equal(scalar3, quantity.Z.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
        public void IQuantity_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2, Scalar scalar3)
        {
            IScalarQuantity iquantity1 = new UnhandledScalar(scalar1);
            IScalarQuantity iquantity2 = new UnhandledScalar(scalar2);
            IScalarQuantity iquantity3 = new UnhandledScalar(scalar3);
            Unhandled3 quantity = Unhandled3.FromQuantities(iquantity1, iquantity2, iquantity3);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
            Assert.Equal(scalar3, quantity.Z.Magnitude);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
        public void UnhandledQuantity_MagnitudeShouldBeEqual(Scalar scalar1, Scalar scalar2, Scalar scalar3)
        {
            Unhandled quantity1 = new(scalar1);
            Unhandled quantity2 = new(scalar2);
            Unhandled quantity3 = new(scalar3);
            Unhandled3 quantity = new(quantity1, quantity2, quantity3);

            Assert.Equal(scalar1, quantity.X.Magnitude);
            Assert.Equal(scalar2, quantity.Y.Magnitude);
            Assert.Equal(scalar3, quantity.Z.Magnitude);
        }
    }
}
