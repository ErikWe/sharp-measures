namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Add_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar addend) => scalar.Add(addend);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchToDoubleAddition(Scalar scalar, Scalar addend)
    {
        var expected = scalar.ToDouble() + addend.ToDouble();

        var actual = Target(scalar, addend).ToDouble();

        Assert.Equal(expected, actual);
    }
}
