#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class CrossTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAcceleration3Dataset, Vector3Dataset>))]
    public void WithVector3(OrbitalAngularAcceleration3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<OrbitalAngularAcceleration3, OrbitalAngularAcceleration3, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAcceleration3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(OrbitalAngularAcceleration3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<OrbitalAngularAcceleration3, Unhandled3, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => OrbitalAngularAcceleration3.Zero.Cross(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => OrbitalAngularAcceleration3.Zero.Cross(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAcceleration3Dataset, Vector3Dataset>))]
    public void Generic(OrbitalAngularAcceleration3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition(quantity, vector, (x) => new Unhandled3(x));
    }
}