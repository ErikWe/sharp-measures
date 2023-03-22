namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Dot_TVector
{
    private static Unhandled Target<TVector>(Unhandled4 vector, TVector factor) where TVector : IVector4Quantity => vector.Dot(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled4();
        var factor = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void MatchComponentsDot(Unhandled4 vector, Unhandled4 factor)
    {
        var expected = vector.Components.Dot(factor.Components);

        var actual = Target(vector, factor).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled4 vector, TVector factor) where TException : Exception where TVector : IVector4Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
