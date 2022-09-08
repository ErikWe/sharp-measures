#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class CrossTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularMomentum3Dataset, Vector3Dataset>))]
    public void WithVector3(SpinAngularMomentum3 quantity, Vector3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<SpinAngularMomentum3, SpinAngularMomentum3, Vector3>(quantity, vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularMomentum3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(SpinAngularMomentum3 quantity, Unhandled3 vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<SpinAngularMomentum3, Unhandled3, Unhandled3>(quantity, vector);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularMomentum3.Zero.Cross(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularMomentum3.Zero.Cross(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularMomentum3Dataset, Vector3Dataset>))]
    public void Generic(SpinAngularMomentum3 quantity, IVector3Quantity vector)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition(quantity, vector, (x) => new Unhandled3(x));
    }
}