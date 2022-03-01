namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class DotTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void WithVector3(Vector3 lhs, Vector3 rhs)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<Vector3, Scalar, Vector3>(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Unhandled3Dataset>))]
    public void WithUnhandled3(Vector3 vector, Unhandled3 unhandled)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares<Vector3, Unhandled, Unhandled3>(vector, unhandled);
    }

    [Fact]
    public void Generic_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Unhandled>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Dot(Vector3.Zero, factory!));
    }

    [Fact]
    public void Generic_NullFactor_ShouldThrowArgumentNull()
    {
        IVector3Quantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Dot(factor!, (x) => new Unhandled(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void Generic(Vector3 vector, IVector3Quantity quantity)
    {
        Utility.QuantityTests.DotTests.Vector3_ShouldBeSumOfSquares(vector, quantity, (x) => new Unhandled(x));
    }
}
