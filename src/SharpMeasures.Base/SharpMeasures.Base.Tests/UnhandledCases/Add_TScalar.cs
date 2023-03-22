namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Add_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled unhandled, TScalar addend) where TScalar : IScalarQuantity => unhandled.Add(addend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var addend = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(unhandled, addend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeAdd(Unhandled unhandled, Unhandled addend)
    {
        var expected = unhandled.Magnitude.Add(addend.Magnitude);

        var actual = Target(unhandled, addend).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled unhandled, TScalar addend) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(unhandled, addend));

        Assert.IsType<TException>(exception);
    }
}
