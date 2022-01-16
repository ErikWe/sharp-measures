using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class NegateTests
    {
        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Scalar_ShouldMatchDouble(Scalar a)
        {
            Scalar result = a.Negate();

            Assert.Equal(-a.Magnitude, result, 2);
        }

        [Theory]
        [ClassData(typeof(Scalar2Dataset))]
        public void Scalar2_ShouldMatchDouble(Scalar2 a)
        {
            Scalar2 result = a.Negate();

            Assert.Equal(-a.X, result.X, 2);
            Assert.Equal(-a.Y, result.Y, 2);
        }

        [Theory]
        [ClassData(typeof(Scalar3Dataset))]
        public void Scalar3_ShouldMatchDouble(Scalar3 a)
        {
            Scalar3 result = a.Negate();

            Assert.Equal(-a.X, result.X, 2);
            Assert.Equal(-a.Y, result.Y, 2);
            Assert.Equal(-a.Z, result.Z, 2);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantityDataset))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Negate();

            Assert.Equal(-a.Magnitude, result.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantity2Dataset))]
        public void IQuantity2_ShouldMatchScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Negate();

            Assert.Equal(-a.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(-a.MagnitudeY, result.MagnitudeY, 2);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantity3Dataset))]
        public void IQuantity3_ShouldMatchScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Negate();

            Assert.Equal(-a.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(-a.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(-a.MagnitudeZ, result.MagnitudeZ, 2);
        }
    }
}
