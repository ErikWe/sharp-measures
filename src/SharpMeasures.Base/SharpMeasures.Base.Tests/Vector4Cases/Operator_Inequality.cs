namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Vector4 lhs, Vector4 rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void Valid_OppositeOfEqualsMethod(Vector4 lhs, Vector4 rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
