using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class PlusTests
    {
        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Scalar_ShouldBeUnaltered(Scalar a)
        {
            Scalar result = a.Plus();

            Assert.Equal(a, result);
        }

        [Theory]
        [ClassData(typeof(Scalar2Dataset))]
        public void Scalar2_ShouldBeUnaltered(Scalar2 a)
        {
            Scalar2 result = a.Plus();

            Assert.Equal(a, result);
        }

        [Theory]
        [ClassData(typeof(Scalar3Dataset))]
        public void Scalar3_ShouldBeUnaltered(Scalar3 a)
        {
            Scalar3 result = a.Plus();

            Assert.Equal(a, result);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantityDataset))]
        public void IQuantity_ShouldBeUnaltered<TQuantity>(IScalarQuantity<TQuantity> a)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Plus();

            Assert.Equal(a, result);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantity2Dataset))]
        public void IQuantity2_ShouldBeUnaltered<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Plus();

            Assert.Equal(a, result);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantity3Dataset))]
        public void IQuantity3_ShouldBeUnaltered<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Plus();

            Assert.Equal(a, result);
        }
    }
}
