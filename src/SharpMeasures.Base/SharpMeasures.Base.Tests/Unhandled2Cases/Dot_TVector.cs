namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Dot_TVector
{
    private static Unhandled Target<TVector>(Unhandled2 vector, TVector factor) where TVector : IVector2Quantity => vector.Dot(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled2();
        var factor = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void MatchComponentsDot(Unhandled2 vector, Unhandled2 factor)
    {
        var expected = vector.Components.Dot(factor.Components);

        var actual = Target(vector, factor).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled2 vector, TVector factor) where TException : Exception where TVector : IVector2Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
