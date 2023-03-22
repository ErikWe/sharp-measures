namespace SharpMeasures.Tests.MetricPrefixCases;

using System;

using Xunit;

public class Operator_Cast_ToScalar
{
    private static Scalar Target(MetricPrefix prefix) => (Scalar)prefix;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var prefix = Datasets.GetNullMetricPrefix();

        AnyError_TException<ArgumentNullException>(prefix);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void Valid_MatchToScalar(MetricPrefix prefix)
    {
        var expected = prefix.ToScalar();

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(MetricPrefix prefix) where TException : Exception
    {
        var exception = Record.Exception(() => Target(prefix));

        Assert.IsType<TException>(exception);
    }
}
