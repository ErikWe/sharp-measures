namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Difference_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled unhandled, TScalar other) where TScalar : IScalarQuantity => unhandled.Difference(other);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var other = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(unhandled, other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeDifference(Unhandled unhandled, Unhandled other)
    {
        var expected = unhandled.Magnitude.Difference(other.Magnitude);

        var actual = Target(unhandled, other).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled unhandled, TScalar other) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(unhandled, other));

        Assert.IsType<TException>(exception);
    }
}
