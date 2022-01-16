using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using System;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class RoundTests
    {
        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Scalar_ShouldMatchSystem(Scalar a)
        {
            Scalar result = a.Round();

            Assert.Equal(Math.Round(a.Magnitude), result.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantityDataset))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Round();

            Assert.Equal(a.Magnitude.Round(), result.Magnitude, 2);
        }
    }
}
