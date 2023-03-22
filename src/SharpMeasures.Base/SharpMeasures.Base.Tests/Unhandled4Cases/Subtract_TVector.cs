namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Subtract_TVector
{
    private static Unhandled4 Target<TVector>(Unhandled4 vector, TVector subtrahend) where TVector : IVector4Quantity => vector.Subtract(subtrahend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled4();
        var subtrahend = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(vector, subtrahend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void MatchComponentsSubtract(Unhandled4 vector, Unhandled4 subtrahend)
    {
        var expected = vector.Components.Subtract(subtrahend.Components);

        var actual = Target(vector, subtrahend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled4 vector, TVector sutbrahend) where TException : Exception where TVector : IVector4Quantity
    {
        var exception = Record.Exception(() => Target(vector, sutbrahend));

        Assert.IsType<TException>(exception);
    }
}
