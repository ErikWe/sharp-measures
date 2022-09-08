#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class DotTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<AngularAcceleration3Dataset, Vector3Dataset>))]
    public void WithVector3(AngularAcceleration3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<AngularAcceleration3, AngularAcceleration, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularAcceleration3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(AngularAcceleration3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<AngularAcceleration3, Unhandled, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Unhandled>? factory = null;

        Assert.Throws<ArgumentNullException>(() => AngularAcceleration3.Zero.Dot(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => AngularAcceleration3.Zero.Dot(factor!, (x) => new Unhandled(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularAcceleration3Dataset, Vector3Dataset>))]
    public void Generic(AngularAcceleration3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares(quantity, vector, (x) => new Unhandled(x));
    }
}