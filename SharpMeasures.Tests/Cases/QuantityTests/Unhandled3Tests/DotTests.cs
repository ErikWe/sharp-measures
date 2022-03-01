namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Unhandled3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class DotTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void WithVector3(Unhandled3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<Unhandled3, Unhandled, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(Unhandled3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<Unhandled3, Unhandled, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Unhandled>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Dot(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Dot(factor!, (x) => new Unhandled(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void Generic(Unhandled3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares(quantity, vector, (x) => new Unhandled(x));
    }

    [Fact]
    public void SemiGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled3.Zero.Dot(factor!));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Unhandled3Dataset, Vector3Dataset>))]
    public void SemiGeneric_ShouldBeSumOfSquares(Unhandled3 quantity, IVector3Quantity vector)
    {
        Unhandled result = quantity.Dot(vector);

        Assert.Equal(quantity.X * vector.X + quantity.Y * vector.Y + quantity.Z * vector.Z, result.Magnitude, 2);
    }
}
