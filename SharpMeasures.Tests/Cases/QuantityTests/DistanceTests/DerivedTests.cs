namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DistanceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(SpeedTimeEquivalence))]
    public void SpeedTime_ShouldBeEquivalent(Speed speed, Time time, Distance expected)
    {
        IEnumerable<Distance> actual = new Distance[]
        {
            Distance.From(speed, time),
            speed.Multiply(time),
            speed * time,
            time.Multiply(speed),
            time * speed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DistanceDataset, DistanceDataset>))]
    public void DistanceAddition_ShouldBeSum(Distance distance1, Distance distance2)
    {
        Distance result_method = distance1.Add(distance2);
        Distance result_operator = distance1 + distance2;

        Assert.Equal(distance1.Magnitude + distance2.Magnitude, result_method.Magnitude, 2);
        Assert.Equal(distance1.Magnitude + distance2.Magnitude, result_operator.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DistanceDataset, DistanceDataset>))]
    public void DistanceSubtraction_ShouldBeDifference(Distance distance1, Distance distance2)
    {
        Distance result_method = distance1.Subtract(distance2);
        Distance result_operator = distance1 - distance2;

        Assert.Equal(distance1.Magnitude - distance2.Magnitude, result_method.Magnitude, 2);
        Assert.Equal(distance1.Magnitude - distance2.Magnitude, result_operator.Magnitude, 2);
    }

    public static IEnumerable<object[]> SpeedTimeEquivalence()
    {
        yield return new object[] { 37 * Speed.OneFootPerSecond, Time.OneMinute, 37 * 60 * Distance.OneFoot };
    }
}
