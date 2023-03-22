namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Sign
{
    private static int Target(Scalar scalar) => scalar.Sign();

    [Theory]
    [ClassData(typeof(Datasets.ValidExceptNaNScalar))]
    public void NotNaN_MatchSystemSign(Scalar scalar)
    {
        var expected = Math.Sign(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NaN_ArithmeticException()
    {
        var scalar = Datasets.GetNaNScalar();

        AnyError_TException<ArithmeticException>(scalar);
    }

    private static void AnyError_TException<TException>(Scalar scalar) where TException : Exception
    {
        var exception = Record.Exception(() => Target(scalar));

        Assert.IsType<TException>(exception);
    }
}
