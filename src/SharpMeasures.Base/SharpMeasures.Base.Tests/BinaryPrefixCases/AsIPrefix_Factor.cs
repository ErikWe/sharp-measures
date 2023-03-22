namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class AsIPrefix_Factor
{
    private static Scalar Target(BinaryPrefix prefix)
    {
        return execute(prefix);

        static Scalar execute(IPrefix prefix) => prefix.Factor;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Valid_MatchInstanceProperty(BinaryPrefix prefix)
    {
        var expected = prefix.Factor;

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
