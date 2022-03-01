namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class CrossTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void WithVector3(Vector3 lhs, Vector3 rhs)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<Vector3, Vector3, Vector3>(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(Vector3 vector, Unhandled3 unhandled)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition<Vector3, Unhandled3, Unhandled3>(vector, unhandled);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Cross(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Cross(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void Generic(Vector3 vector, IVector3Quantity quantity)
    {
        Utility.QuantityTests.CrossTests.Vector3_ShouldMatchDefinition(vector, quantity, (x) => new Unhandled3(x));
    }
}
