namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class ToDouble
{
    private static double Target(Scalar scalar) => scalar.ToDouble();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Valid_NoException(Scalar scalar)
    {
        var exception = Record.Exception(() => Target(scalar));

        Assert.Null(exception);
    }
}
