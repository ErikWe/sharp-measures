namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Subtract_TVector
{
    private static Unhandled2 Target<TVector>(Unhandled2 vector, TVector subtrahend) where TVector : IVector2Quantity => vector.Subtract(subtrahend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled2();
        var subtrahend = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(vector, subtrahend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void MatchComponentsSubtract(Unhandled2 vector, Unhandled2 subtrahend)
    {
        var expected = vector.Components.Subtract(subtrahend.Components);

        var actual = Target(vector, subtrahend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled2 vector, TVector sutbrahend) where TException : Exception where TVector : IVector2Quantity
    {
        var exception = Record.Exception(() => Target(vector, sutbrahend));

        Assert.IsType<TException>(exception);
    }
}
