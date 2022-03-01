namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ForceTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassAccelerationEquivalence))]
    public void MassAcceleration_ShouldBeEquivalent(Mass mass, Acceleration acceleration, Force expected)
    {
        IEnumerable<Force> actual = new Force[]
        {
            Force.From(mass, acceleration),
            mass.Multiply(acceleration),
            mass * acceleration,
            acceleration.Multiply(mass),
            acceleration * mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassAccelerationEquivalence()
    {
        yield return new object[] { 19 * Mass.OneGram, 7 * Acceleration.OneMetrePerSecondSquared, 0.019 * 7 * Force.OneNewton };
    }
}
