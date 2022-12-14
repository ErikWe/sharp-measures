namespace SharpMeasures.Tests.PhysicsProblems.Kinematics;

using SharpMeasures;

using System;

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

        Assert.Equal(9.80665 * 2.6 * 2.6 / 2, distance.Metres, 10);
        Assert.Equal(9.80665 * 2.6, speed.MetresPerSecond, 10);
    }

    [Fact]
    public void Exercise4()
    {
        var initialSpeed = 18.5 * Speed.OneMetrePerSecond;
        var finalSpeed = 46.1 * Speed.OneMetrePerSecond;
        var duration = 2.47 * Time.OneSecond;

        var deltaSpeed = finalSpeed - initialSpeed;

        var acceleration = deltaSpeed / duration;
        var distance = (initialSpeed * duration) + (deltaSpeed * duration / 2);

        Assert.Equal((46.1 - 18.5) / 2.47, acceleration.MetresPerSecondSquared, 10);
        Assert.Equal((18.5 * 2.47) + ((46.1 - 18.5) * 2.47 / 2), distance.Metres, 10);
    }

    [Fact]
    public void Exercise5()
    {
        var height = 1.4 * Distance.OneMetre;
        var acceleration = 1.67 * GravitationalAcceleration.OneMetrePerSecondSquared;

        var duration = (height * 2 / acceleration).SquareRoot();

        Assert.Equal(Math.Sqrt(1.4 * 2 / 1.67), duration.Seconds, 10);
    }

    [Fact]
    public void Exercise6()
    {
        var finalSpeed = 444 * Speed.OneMetrePerSecond;
        var duration = 1.83 * Time.OneSecond;

        var acceleration = finalSpeed / duration;
        var distance = finalSpeed * duration / 2;

        Assert.Equal(444 / 1.83, acceleration.MetresPerSecondSquared, 10);
        Assert.Equal(444 * 1.83 / 2, distance.Metres, 10);
    }

    [Fact]
    public void Exercise7()
    {
        var finalSpeed = 7.1 * Speed.OneMetrePerSecond;
        var distance = 35.4 * Distance.OneMetre;

        var acceleration = finalSpeed.Square() / distance / 2;

        Assert.Equal(7.1 * 7.1 / 35.4 / 2, acceleration.MetresPerSecondSquared, 10);
    }

    [Fact]
    public void Exercise8()
    {
        var acceleration = 3 * Acceleration.OneMetrePerSecondSquared;
        var finalSpeed = 65 * Speed.OneMetrePerSecond;

        var length = finalSpeed.Square() / acceleration / 2;

        Assert.Equal(65d * 65 / 3 / 2, length.Metres, 10);
    }

    [Fact]
    public void Exercise9()
    {
        var initialSpeed = 22.4 * Speed.OneMetrePerSecond;
        var duration = 2.55 * Time.OneSecond;

        var distance = initialSpeed / 2 * duration;

        Assert.Equal(22.4 / 2 * 2.55, distance.Metres, 10);
    }

    [Fact]
    public void Exercise10()
    {
        var height = 2.62 * Distance.OneMetre;
        var acceleration = -GravitationalAcceleration.StandardGravity;

        var initialSpeed = (-2 * acceleration * height).SquareRoot();

        Assert.Equal(Math.Sqrt(2 * 9.80665 * 2.62), initialSpeed.MetresPerSecond, 10);
    }

    [Fact]
    public void Exercise11()
    {
        var height = 1.29 * Distance.OneMetre;
        var acceleration = -GravitationalAcceleration.StandardGravity;

        var initialSpeed = (-2 * acceleration * height).SquareRoot();

        var duration = -initialSpeed / acceleration;

        Assert.Equal(Math.Sqrt(2 * 9.80665 * 1.29), initialSpeed.MetresPerSecond, 10);
        Assert.Equal(Math.Sqrt(2 * 9.80665 * 1.29) / 9.80665, duration.Seconds, 10);
    }

    [Fact]
    public void Exercise12()
    {
        var finalSpeed = 521 * Speed.OneMetrePerSecond;
        var distance = 0.84 * Distance.OneMetre;

        var acceleration = finalSpeed.Square() / distance / 2;

        Assert.Equal(521 * 521 / 0.84 / 2, acceleration.MetresPerSecondSquared, 10);
    }

    [Fact]
    public void Exercise13()
    {
        var duration = 6.25 * Time.OneSecond;
        var acceleration = GravitationalAcceleration.StandardGravity;

        var finalSpeed = acceleration * duration / 2;

        var distance = finalSpeed.Square() / acceleration / 2;

        Assert.Equal(9.80665 * 6.25 / 2 * (9.80665 * 6.25 / 2) / 9.80665 / 2, distance.Metres, 10);
    }

    [Fact]
    public void Exercise14()
    {
        var height = 370 * Length.OneMetre;
        var acceleration = GravitationalAcceleration.StandardGravity;

        var duration = (height / acceleration * 2).SquareRoot();

        Assert.Equal(Math.Sqrt(370 / 9.80665 * 2), duration.Seconds, 10);
    }

    [Fact]
    public void Exercise15()
    {
        var initialSpeed = 367 * Speed.OneMetrePerSecond;
        var distance = 0.0621 * Distance.OneMetre;

        var acceleration = -initialSpeed.Square() / 2 / distance;

        Assert.Equal(-367d * 367 / 2 / 0.0621, acceleration.MetresPerSecondSquared, 10);
    }

    [Fact]
    public void Exercise16()
    {
        var duration = 3.41 * Time.OneSecond;
        var acceleration = GravitationalAcceleration.StandardGravity;

        var length = duration.Square() * acceleration / 2;

        Assert.Equal(3.41 * 3.41 * 9.80665 / 2, length.Metres, 10);
    }

    [Fact]
    public void Exercise17()
    {
        var distance = 290 * Distance.OneMetre;
        var acceleration = -3.9 * Acceleration.OneMetrePerSecondSquared;

        var initialSpeed = (-2 * distance * acceleration).SquareRoot();

        Assert.Equal(Math.Sqrt(2 * 290 * 3.9), initialSpeed.MetresPerSecond, 10);
    }

    [Fact]
    public void Exercise18()
    {
        var finalSpeed = 88.3 * Speed.OneMetrePerSecond;
        var distance = 1365 * Distance.OneMetre;

        var acceleration = finalSpeed.Square() / distance / 2;

        var duration = finalSpeed / acceleration;

        Assert.Equal(88.3 * 88.3 / 1365 / 2, acceleration.MetresPerSecondSquared, 10);
        Assert.Equal(1 / (88.3 / 1365 / 2), duration.Seconds, 10);
    }

    [Fact]
    public void Exercise19()
    {
        var finalSpeed = 112 * Speed.OneMetrePerSecond;
        var distance = 398 * Distance.OneMetre;

        var acceleration = finalSpeed.Square() / distance / 2;

        Assert.Equal(112d * 112 / 398 / 2, acceleration.MetresPerSecondSquared, 10);
    }

    [Fact]
    public void Exercise20()
    {
        var height = 91.5 * Distance.OneMetre;
        var acceleration = -GravitationalAcceleration.StandardGravity;

        var initialSpeed = (-2 * acceleration * height).SquareRoot();

        Assert.Equal(Math.Sqrt(2 * 9.80665 * 91.5) * (3600 / 1609.344), initialSpeed.MilesPerHour, 10);
    }
}
