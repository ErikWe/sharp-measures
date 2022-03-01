#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class DotTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularVelocity3Dataset, Vector3Dataset>))]
    public void WithVector3(SpinAngularVelocity3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<SpinAngularVelocity3, SpinAngularSpeed, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularVelocity3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(SpinAngularVelocity3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<SpinAngularVelocity3, Unhandled, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Unhandled>? factory = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularVelocity3.Zero.Dot(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularVelocity3.Zero.Dot(factor!, (x) => new Unhandled(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularVelocity3Dataset, Vector3Dataset>))]
    public void Generic(SpinAngularVelocity3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares(quantity, vector, (x) => new Unhandled(x));
    }
}
