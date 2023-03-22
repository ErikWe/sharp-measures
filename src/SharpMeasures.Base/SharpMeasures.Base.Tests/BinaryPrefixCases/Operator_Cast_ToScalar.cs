namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class Operator_Cast_ToScalar
{
    private static Scalar Target(BinaryPrefix prefix) => (Scalar)prefix;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var prefix = Datasets.GetNullBinaryPrefix();

        AnyError_TException<ArgumentNullException>(prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Valid_MatchToScalar(BinaryPrefix prefix)
    {
        var expected = prefix.ToScalar();

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(BinaryPrefix prefix) where TException : Exception
    {
        var exception = Record.Exception(() => Target(prefix));

        Assert.IsType<TException>(exception);
    }
}
