namespace SharpMeasures.Tests.PhysicsProblems.Kinematics;

using SharpMeasures;

using Xunit;

/// <credit>https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions</credit>
public class PhysicsClassroom
{
    [Fact]
    public void Exercise1()
    {
        var acceleration = 3.2 * Acceleration.OneMetrePerSecondSquared;
        var duration = 32.8 * Time.OneSecond;

        var distance = acceleration * duration * duration / 2;

        Assert.Equal(3.2 * 32.8 * 32.8 / 2, distance.Metres, 10);
    }

    [Fact]
    public void Exercise2()
    {
        var duration = 5.21 * Time.OneSecond;
        var distance = 110 * Distance.OneMetre;

        var acceleration = distance / duration / duration * 2;

        Assert.Equal(110 / 5.21 / 5.21 * 2, acceleration.MetresPerSecondSquared, 10);
    }

    [Fact]
    public void Exercise3()
    {
        var duration = 2.6 * Time.OneSecond;
        var acceleration = GravitationalAcceleration.StandardGravity;

        var distance = acceleration * duration * duration / 2;
        var speed = acceleration * duration;

        Assert.Equal(9.81 * 2.6 * 2.6 / 2, distance.Metres, 10);
        Assert.Equal(9.81 * 2.6, speed.MetresPerSecond, 10);
    }

    [Fact]
    public void Exercise4()
    {
        var initialSpeed = 18.5 * Speed.OneMetrePerSecond;
        var finalSpeed = 46.1 * Speed.OneMetrePerSecond;
        var duration = 2.47 * Time.OneSecond;

        var deltaSpeed = finalSpeed - initialSpeed;

        var acceleration = deltaSpeed / duration;
        var distance = initialSpeed * duration + deltaSpeed * duration / 2;

        Assert.Equal((46.1 - 18.5) / 2.47, acceleration.MetresPerSecondSquared, 10);
        Assert.Equal(18.5 * 2.47 + (46.1 - 18.5) * 2.47 / 2, distance.Metres, 10);
    }

    [Fact]
    public void Exercise5()
    {
        var height = 1.4 * Distance.OneMetre;
        var acceleration = 1.67 * GravitationalAcceleration.OneMetrePerSecondSquared;

        var duration = (height * 2 / acceleration);
    }
}
