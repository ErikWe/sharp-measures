#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class DotTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularVelocity3Dataset, Vector3Dataset>))]
    public void WithVector3(OrbitalAngularVelocity3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<OrbitalAngularVelocity3, OrbitalAngularSpeed, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularVelocity3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(OrbitalAngularVelocity3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<OrbitalAngularVelocity3, Unhandled, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Unhandled>? factory = null;

        Assert.Throws<ArgumentNullException>(() => OrbitalAngularVelocity3.Zero.Dot(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => OrbitalAngularVelocity3.Zero.Dot(factor!, (x) => new Unhandled(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularVelocity3Dataset, Vector3Dataset>))]
    public void Generic(OrbitalAngularVelocity3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares(quantity, vector, (x) => new Unhandled(x));
    }
}
