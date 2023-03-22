namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Dot_TVector
{
    private static Unhandled Target<TVector>(Unhandled3 vector, TVector factor) where TVector : IVector3Quantity => vector.Dot(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchComponentsDot(Unhandled3 vector, Unhandled3 factor)
    {
        var expected = vector.Components.Dot(factor.Components);

        var actual = Target(vector, factor).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled3 vector, TVector factor) where TException : Exception where TVector : IVector3Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
