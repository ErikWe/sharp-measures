namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class ToScalar
{
    private static Scalar Target(BinaryPrefix prefix) => prefix.ToScalar();

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Valid_MatchFactor(BinaryPrefix prefix)
    {
        var expected = prefix.Factor;

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
