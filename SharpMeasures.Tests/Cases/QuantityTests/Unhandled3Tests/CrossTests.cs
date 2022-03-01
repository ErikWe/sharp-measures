namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Unhandled3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class CrossTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void WithVector3(Unhandled3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<Unhandled3, Unhandled3, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(Unhandled3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<Unhandled3, Unhandled3, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Cross(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Cross(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void Generic(Unhandled3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition(quantity, vector, (x) => new Unhandled3(x));
    }

    [Fact]
    public void SemiGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Cross(factor!));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void SemiGeneric_ShouldMatchDefinition(Unhandled3 quantity, IVector3Quantity vector)
    {
        Unhandled3 result = quantity.Cross(vector);

        Assert.Equal(quantity.Y * vector.Z - quantity.Z * vector.Y, result.X, 2);
        Assert.Equal(quantity.Z * vector.X - quantity.X * vector.Z, result.Y, 2);
        Assert.Equal(quantity.X * vector.Y - quantity.Y * vector.X, result.Z, 2);
    }
}
