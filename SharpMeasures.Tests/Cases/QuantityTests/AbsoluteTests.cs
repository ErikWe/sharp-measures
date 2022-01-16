﻿using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity;

using System;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests
{
    public class AbsoluteTests
    {
        [Theory]
        [ClassData(typeof(ScalarDataset))]
        public void Scalar_ShouldMatchSystem(Scalar a)
        {
            Scalar result = a.Absolute();

            Assert.Equal(Math.Abs(a.Magnitude), result.Magnitude, 2);
        }

        [Theory]
        [ClassData(typeof(UnhandledQuantityDataset))]
        public void IQuantity_ShouldMatchScalar<TQuantity>(IScalarQuantity<TQuantity> a)
            where TQuantity : IScalarQuantity
        {
            IScalarQuantity result = a.Absolute();

            Assert.Equal(a.Magnitude.Absolute(), result.Magnitude, 2);
        }
    }
}
