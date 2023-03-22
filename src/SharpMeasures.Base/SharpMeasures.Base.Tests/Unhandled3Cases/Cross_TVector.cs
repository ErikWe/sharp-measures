namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Cross_TVector
{
    private static Unhandled3 Target<TVector>(Unhandled3 vector, TVector factor) where TVector : IVector3Quantity => vector.Cross(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchComponentsCross(Unhandled3 vector, Unhandled3 factor)
    {
        var expected = vector.Components.Cross(factor.Components);

        var actual = Target(vector, factor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled3 vector, TVector factor) where TException : Exception where TVector : IVector3Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
