namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Plus
{
    private static Scalar Target(Scalar scalar) => +scalar;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchMethod(Scalar scalar)
    {
        var expected = scalar.Plus();

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
