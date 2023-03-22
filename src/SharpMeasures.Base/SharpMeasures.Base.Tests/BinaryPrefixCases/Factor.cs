namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Factor
{
    private static Scalar Target(BinaryPrefix prefix) => prefix.Factor;

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Valid_NoException(BinaryPrefix prefix)
    {
        var exception = Record.Exception(() => Target(prefix));

        Assert.Null(exception);
    }
}
