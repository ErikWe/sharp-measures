namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Vector3 lhs, Vector3 rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void Valid_OppositeOfEqualsMethod(Vector3 lhs, Vector3 rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
