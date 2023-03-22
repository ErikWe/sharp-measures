namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVector4Quantity_Components
{
    private static Vector4 Target(Unhandled4 vector)
    {
        return execute(vector);

        static Vector4 execute(IVector4Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponents(Unhandled4 vector)
    {
        var expected = vector.Components;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
