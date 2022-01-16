using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class AddTests
    {
        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
        public void Scalar_ShouldMatchDouble(Scalar a, Scalar b)
        {
            Scalar result = a.Add(b);

            Assert.Equal(a.Magnitude + b.Magnitude, result, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar2Dataset, Scalar2Dataset>))]
        public void Scalar2_WithScalar2_ComponentsShouldMatchScalar1(Scalar2 a, Scalar2 b)
        {
            Scalar2 result = a.Add(b);

            Assert.Equal(a.X + b.X, result.X, 2);
            Assert.Equal(a.Y + b.Y, result.Y, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar2Dataset, ScalarDataset>))]
        public void Scalar2_WithScalar_ComponentsShouldMatchScalar1(Scalar2 a, Scalar b)
        {
            Scalar2 result = a.Add(b);

            Assert.Equal(a.X + b, result.X, 2);
            Assert.Equal(a.Y + b, result.Y, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar3Dataset, Scalar3Dataset>))]
        public void Scalar3_WithScalar3_ComponentsShouldMatchScalar1(Scalar3 a, Scalar3 b)
        {
            Scalar3 result = a.Add(b);

            Assert.Equal(a.X + b.X, result.X, 2);
            Assert.Equal(a.Y + b.Y, result.Y, 2);
            Assert.Equal(a.Z + b.Z, result.Z, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar3Dataset, ScalarDataset>))]
        public void Scalar3_WithScalar_ComponentsShouldMatchScalar1(Scalar3 a, Scalar b)
        {
            Scalar3 result = a.Add(b);

            Assert.Equal(a.X + b, result.X, 2);
            Assert.Equal(a.Y + b, result.Y, 2);
            Assert.Equal(a.Z + b, result.Z, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantityDataset, UnhandledQuantityDataset>))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a, TQuantity b)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Add(b);

            Assert.Equal(a.Magnitude + b.Magnitude, result.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity2Dataset, UnhandledQuantity2Dataset>))]
        public void IQuantity2_WithIQuantity2_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Add(b);

            Assert.Equal(a.MagnitudeX + b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY + b.MagnitudeY, result.MagnitudeY, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity2Dataset, UnhandledQuantityDataset>))]
        public void IQuantity2_WithIQuantity_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result = a.Add(b);

            Assert.Equal(a.MagnitudeX + b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY + b.Magnitude, result.MagnitudeY, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity3Dataset, UnhandledQuantity3Dataset>))]
        public void IQuantity3_WithIQuantity3_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Add(b);

            Assert.Equal(a.MagnitudeX + b.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY + b.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ + b.MagnitudeZ, result.MagnitudeZ, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity3Dataset, UnhandledQuantityDataset>))]
        public void IQuantity3_WithIQuantity_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result = a.Add(b);

            Assert.Equal(a.MagnitudeX + b.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY + b.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ + b.Magnitude, result.MagnitudeZ, 2);
        }
    }
}
