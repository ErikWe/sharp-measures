namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Transform
{
    private static Unhandled3 Target(Unhandled3 vector, System.Numerics.Matrix4x4 transform) => vector.Transform(transform);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidMatrix4x4))]
    public void MatchComponents(Unhandled3 vector, System.Numerics.Matrix4x4 transform)
    {
        var expected = vector.Components.Transform(transform);

        var actual = Target(vector, transform).Components;

        Assert.Equal(expected, actual);
    }
}
