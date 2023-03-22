namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class ThousandTwentyFourToThePower
{
    private static BinaryPrefix Target(int exponent) => BinaryPrefix.ThousandTwentyFourToThePower(exponent);

    [Theory]
    [ClassData(typeof(Datasets.InvalidExponentInt32))]
    public void TooLarge_ArgumentOutOfRangeException(int exponent) => AnyError_TException<ArgumentOutOfRangeException>(exponent);

    [Theory]
    [ClassData(typeof(Datasets.ValidExponentInt32))]
    public void Valid_MatchFactor(int exponent)
    {
        var expected = Math.Pow(1024, exponent);

        var actual = Target(exponent).Factor.ToDouble();

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(int exponent) where TException : Exception
    {
        var exception = Record.Exception(() => Target(exponent));

        Assert.IsType<TException>(exception);
    }
}
