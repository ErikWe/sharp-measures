namespace ErikWe.SharpMeasures.Tests.Cases.MathProblems.PhysicsClassroom;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

/// <summary>Problems taken from <see href="https://www.physicsclassroom.com/calcpad/1dkin/problems"/>.</summary>
public class Kinematics
{
    [Fact]
    public void One()
    {
        Distance distance = 100 * Distance.OneMetre;
        Time time = 9.69 * Time.OneSecond;

        Speed averageSpeed = distance / time;

        Assert.Equal(10.3, averageSpeed.MetresPerSecond, 1);
    }

    [Fact]
    public void Two()
    {
        Distance distance = 0.25 * Distance.OneMile;
        Time time = 4.437 * Time.OneSecond;

        Speed averageSpeed = distance / time;

        Assert.Equal(202.8, averageSpeed.MilesPerHour, 0);
        Assert.Equal(90.7, averageSpeed.MetresPerSecond, 1);
    }

    [Fact]
    public void Three()
    {
        Distance distancePerLeg = 25 * Distance.OneYard;
        Time firstLeg = 10.01 * Time.OneSecond;
        Time secondLeg = 10.22 * Time.OneSecond;

        Speed averageSpeed = distancePerLeg * 2 / (firstLeg + secondLeg);
        Speed averageSpeedFirstLeg = distancePerLeg / firstLeg;

        Assert.Equal(2.47, averageSpeed.YardsPerSecond, 2);
        Assert.Equal(2.50, averageSpeedFirstLeg.YardsPerSecond, 2);
    }

    [Fact]
    public void Four()
    {
        Speed averageSpeed = 9.8 * Speed.OneMetrePerSecond;
        Distance distance = 80 * Distance.OneYard;

        Time time = distance / averageSpeed;

        Assert.Equal(7.4, time.Seconds, 0);
    }

    [Fact]
    public void Five()
    {
        Speed initialSpeed = 9.32 * Speed.OneMetrePerSecond;
        Speed finalSpeed = Speed.Zero;
        Acceleration acceleration = -4.06 * Acceleration.OneMetrePerSecondSquared;

        Speed speedDifference = finalSpeed - initialSpeed;
        Time time = speedDifference / acceleration;

        Assert.Equal(2.30, time.Seconds, 2);

        Speed averageSpeed = speedDifference.Absolute() / 2;
        Distance distance = averageSpeed * time;

        Assert.Equal(10.7, distance.Metres, 1);
    }

    [Fact]
    public void Six()
    {
        Speed averageSpeed1 = 5.8 * Speed.OneMetrePerSecond;
        Time time1 = 12.9 * Time.OneMinute;

        Speed averageSpeed2 = 6.1 * Speed.OneMetrePerSecond;
        Time time2 = 7.1 * Time.OneMinute;

        Distance totalDistance = averageSpeed1 * time1 + averageSpeed2 * time2;

        Assert.Equal(7088, totalDistance.Metres, 0);
    }

    [Fact]
    public void Seven()
    {
        Speed deltaSpeed = 27.8 * Speed.OneMetrePerSecond;
        Time time = 3.4 * Time.OneSecond;

        Acceleration acceleration = deltaSpeed / time;

        Assert.Equal(8.18, acceleration.MetresPerSecondSquared, 2);
        Assert.Equal(18, acceleration.MilesPerHourPerSecond, 0);
    }

    [Fact]
    public void Eight()
    {
        Speed deltaSpeed = 96 * Speed.OneMilePerHour + 56 * Speed.OneMilePerHour;
        Time duration = 0.75 * Time.OneMillisecond;

        Acceleration acceleration = deltaSpeed / duration;

        Assert.Equal(202667, acceleration.MilesPerHourPerSecond, 0);
        Assert.Equal(90600, acceleration.MetresPerSecondSquared, 0);
    }

    [Fact]
    public void Nine()
    {
        Speed initialSpeed = 27.8 * Speed.OneMetrePerSecond;
        Speed finalSpeed = Speed.Zero;

        Distance brakingDistance = 17 * Distance.OneMetre;

        Speed averageSpeed = (initialSpeed + finalSpeed) / 2;

        Time duration = brakingDistance / averageSpeed;

        Acceleration acceleration = (finalSpeed - initialSpeed) / duration;

        Assert.Equal(22.7, acceleration.MetresPerSecondSquared.Absolute(), 1);
    }
}
