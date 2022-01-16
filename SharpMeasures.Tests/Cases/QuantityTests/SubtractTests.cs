using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class SubtractTests
    {
        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
        public void Scalar_ShouldMatchDouble(Scalar a, Scalar b)
        {
            Scalar result1 = a.Subtract(b);
            Scalar result2 = a.SubtractFrom(b);

            Assert.Equal(a.Magnitude - b.Magnitude, result1, 2);

            Assert.Equal(b.Magnitude - a.Magnitude, result2, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar2Dataset, Scalar2Dataset>))]
        public void Scalar2_WithScalar2_ComponentsShouldMatchScalar1(Scalar2 a, Scalar2 b)
        {
            Scalar2 result1 = a.Subtract(b);
            Scalar2 result2 = a.SubtractFrom(b);

            Assert.Equal(a.X - b.X, result1.X, 2);
            Assert.Equal(a.Y - b.Y, result1.Y, 2);

            Assert.Equal(b.X - a.X, result2.X, 2);
            Assert.Equal(b.Y - a.Y, result2.Y, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar2Dataset, ScalarDataset>))]
        public void Scalar2_WithScalar_ComponentsShouldMatchScalar1(Scalar2 a, Scalar b)
        {
            Scalar2 result1 = a.Subtract(b);
            Scalar2 result2 = a.SubtractFrom(b);

            Assert.Equal(a.X - b, result1.X, 2);
            Assert.Equal(a.Y - b, result1.Y, 2);

            Assert.Equal(b - a.X, result2.X, 2);
            Assert.Equal(b - a.Y, result2.Y, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar3Dataset, Scalar3Dataset>))]
        public void Scalar3_WithScalar3_ComponentsShouldMatchScalar1(Scalar3 a, Scalar3 b)
        {
            Scalar3 result1 = a.Subtract(b);
            Scalar3 result2 = a.SubtractFrom(b);

            Assert.Equal(a.X - b.X, result1.X, 2);
            Assert.Equal(a.Y - b.Y, result1.Y, 2);
            Assert.Equal(a.Z - b.Z, result1.Z, 2);

            Assert.Equal(b.X - a.X, result2.X, 2);
            Assert.Equal(b.Y - a.Y, result2.Y, 2);
            Assert.Equal(b.Z - a.Z, result2.Z, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<Scalar3Dataset, ScalarDataset>))]
        public void Scalar3_WithScalar_ComponentsShouldMatchScalar1(Scalar3 a, Scalar b)
        {
            Scalar3 result1 = a.Subtract(b);
            Scalar3 result2 = a.SubtractFrom(b);

            Assert.Equal(a.X - b, result1.X, 2);
            Assert.Equal(a.Y - b, result1.Y, 2);
            Assert.Equal(a.Z - b, result1.Z, 2);

            Assert.Equal(b - a.X, result2.X, 2);
            Assert.Equal(b - a.Y, result2.Y, 2);
            Assert.Equal(b - a.Z, result2.Z, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantityDataset, UnhandledQuantityDataset>))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a, TQuantity b)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result1 = a.Subtract(b);
            IScalarQuantity result2 = a.SubtractFrom(b);

            Assert.Equal(a.Magnitude - b.Magnitude, result1.Magnitude, 2);

            Assert.Equal(b.Magnitude - a.Magnitude, result2.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity2Dataset, UnhandledQuantity2Dataset>))]
        public void IQuantity2_WithIQuantity2_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.Subtract(b);
            TQuantity result2 = a.SubtractFrom(b);

            Assert.Equal(a.MagnitudeX - b.MagnitudeX, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY - b.MagnitudeY, result1.MagnitudeY, 2);

            Assert.Equal(b.MagnitudeX - a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.MagnitudeY - a.MagnitudeY, result2.MagnitudeY, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity2Dataset, UnhandledQuantityDataset>))]
        public void IQuantity2_WithIQuantity_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity2<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>
        {
            TQuantity result1 = a.Subtract(b);
            TQuantity result2 = a.SubtractFrom(b);

            Assert.Equal(a.MagnitudeX - b.Magnitude, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY - b.Magnitude, result1.MagnitudeY, 2);

            Assert.Equal(b.Magnitude - a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.Magnitude - a.MagnitudeY, result2.MagnitudeY, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity3Dataset, UnhandledQuantity3Dataset>))]
        public void IQuantity3_WithIQuantity3_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TQuantity b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.Subtract(b);
            TQuantity result2 = a.SubtractFrom(b);

            Assert.Equal(a.MagnitudeX - b.MagnitudeX, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY - b.MagnitudeY, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ - b.MagnitudeZ, result1.MagnitudeZ, 2);

            Assert.Equal(b.MagnitudeX - a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.MagnitudeY - a.MagnitudeY, result2.MagnitudeY, 2);
            Assert.Equal(b.MagnitudeZ - a.MagnitudeZ, result2.MagnitudeZ, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantity3Dataset, UnhandledQuantityDataset>))]
        public void IQuantity3_WithIQuantity_ComponentsShouldMatchScalar<TComponent, TQuantity>(IQuantity3<TComponent, TQuantity> a, TComponent b)
            where TComponent : IScalarQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>
        {
            TQuantity result1 = a.Subtract(b);
            TQuantity result2 = a.SubtractFrom(b);

            Assert.Equal(a.MagnitudeX - b.Magnitude, result1.MagnitudeX, 2);
            Assert.Equal(a.MagnitudeY - b.Magnitude, result1.MagnitudeY, 2);
            Assert.Equal(a.MagnitudeZ - b.Magnitude, result1.MagnitudeZ, 2);

            Assert.Equal(b.Magnitude - a.MagnitudeX, result2.MagnitudeX, 2);
            Assert.Equal(b.Magnitude - a.MagnitudeY, result2.MagnitudeY, 2);
            Assert.Equal(b.Magnitude - a.MagnitudeZ, result2.MagnitudeZ, 2);
        }
    }
}
