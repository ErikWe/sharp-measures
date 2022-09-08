﻿namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class ComparisonTests
{
    public static void Method_ShouldMatchScalar<TQuantity>(TQuantity a, TQuantity b)
        where TQuantity : IScalarQuantity, IComparable<TQuantity>
    {
        if (a.Magnitude.CompareTo(b.Magnitude) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Magnitude.CompareTo(b.Magnitude) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    public static void Operators_ShouldMatchDouble<TQuantity>(TQuantity a, TQuantity b, bool greaterThan, bool greaterThanOrEqual, bool lesserThanOrEqual, bool lesserThan)
        where TQuantity : IScalarQuantity, IComparable<TQuantity>
    {
        if (a.Magnitude > b.Magnitude)
        {
            Assert.True(greaterThan);
            Assert.True(greaterThanOrEqual);
            Assert.False(lesserThanOrEqual);
            Assert.False(lesserThan);
        }
        else if (a.Magnitude < b.Magnitude)
        {
            Assert.False(greaterThan);
            Assert.False(greaterThanOrEqual);
            Assert.True(lesserThanOrEqual);
            Assert.True(lesserThan);
        }
        else if (a.Magnitude == b.Magnitude)
        {
            Assert.False(greaterThan);
            Assert.True(greaterThanOrEqual);
            Assert.True(lesserThanOrEqual);
            Assert.False(lesserThan);
        }
        else
        {
            Assert.False(greaterThan);
            Assert.False(greaterThanOrEqual);
            Assert.False(lesserThanOrEqual);
            Assert.False(lesserThan);
        }
    }
}