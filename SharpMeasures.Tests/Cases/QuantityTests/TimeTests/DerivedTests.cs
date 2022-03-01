namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DistanceSpeedEquivalence))]
    public void DistanceSpeed_ShouldBeEquivalent(Distance distance, Speed speed, Time expected)
    {
        IEnumerable<Time> actual = new Time[]
        {
            Time.From(distance, speed),
            distance.Divide(speed),
            distance / speed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [MemberData(nameof(SpeedAccelerationEquivalence))]
    public void SpeedAcceleration_ShouldBeEquivalent(Speed speed, Acceleration acceleration, Time expected)
    {
        IEnumerable<Time> actual = new Time[]
        {
            Time.From(speed, acceleration),
            speed.Divide(acceleration),
            speed / acceleration
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeDataset, TimeDataset>))]
    public void TimeAddition_ShouldBeSum(Time time1, Time time2)
    {
        Time result_method = time1.Add(time2);
        Time result_operator = time1 + time2;

        Assert.Equal(time1.Magnitude + time2.Magnitude, result_method.Magnitude, 2);
        Assert.Equal(time1.Magnitude + time2.Magnitude, result_operator.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeDataset, TimeDataset>))]
    public void TimeSubtraction_ShouldBeDifference(Time time1, Time time2)
    {
        Time result_method = time1.Subtract(time2);
        Time result_operator = time1 - time2;

        Assert.Equal(time1.Magnitude - time2.Magnitude, result_method.Magnitude, 2);
        Assert.Equal(time1.Magnitude - time2.Magnitude, result_operator.Magnitude, 2);
    }

    public static IEnumerable<object[]> DistanceSpeedEquivalence()
    {
        yield return new object[] { 37 * Distance.OneCentimetre, 4 * Speed.OneMetrePerSecond, 0.37 / 4 * Time.OneSecond };
    }

    public static IEnumerable<object[]> SpeedAccelerationEquivalence()
    {
        yield return new object[] { 37 * Speed.OneKilometrePerSecond, 4 * Acceleration.OneMetrePerSecondSquared, 37000 / 4 * Time.OneSecond };
    }
}
