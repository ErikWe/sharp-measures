using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Double;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using System;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class PowerTests
    {
        [Theory]
        [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
        public void Scalar_ShouldMatchSystem(Scalar a, double exponent)
        {
            Scalar result1 = a.Power(exponent);
            Unhandled result2 = ((IScalarQuantity<Scalar>)a).Power(exponent);

            Assert.Equal(Math.Pow(a.Magnitude, exponent), result1.Magnitude, 2);
            Assert.Equal(result1, result2.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(GenericDataset<UnhandledQuantityDataset, DoubleDataset>))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a, double exponent)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Power(exponent);

            Assert.Equal(a.Magnitude.Power(exponent), result.Magnitude, 2);
        }
    }
}
