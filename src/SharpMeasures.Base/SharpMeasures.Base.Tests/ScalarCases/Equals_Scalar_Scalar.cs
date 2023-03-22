namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Equals_Scalar_Scalar
{
    private static bool Target(Scalar lhs, Scalar rhs) => Scalar.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchInstanceMethod(Scalar lhs, Scalar rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
