namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Subtract_TVector
{
    private static Unhandled3 Target<TVector>(Unhandled3 vector, TVector subtrahend) where TVector : IVector3Quantity => vector.Subtract(subtrahend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var subtrahend = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(vector, subtrahend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchComponentsSubtract(Unhandled3 vector, Unhandled3 subtrahend)
    {
        var expected = vector.Components.Subtract(subtrahend.Components);

        var actual = Target(vector, subtrahend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled3 vector, TVector sutbrahend) where TException : Exception where TVector : IVector3Quantity
    {
        var exception = Record.Exception(() => Target(vector, sutbrahend));

        Assert.IsType<TException>(exception);
    }
}
