namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Force3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassAccelerationEquivalence))]
    public void MassAcceleration_ShouldBeEquivalent(Mass mass, Acceleration3 acceleration, Force3 expected)
    {
        IEnumerable<Force3> actual = new Force3[]
        {
            Force3.From(mass, acceleration),
            mass.Multiply(acceleration),
            mass * acceleration,
            acceleration.Multiply(mass),
            acceleration * mass
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MassAccelerationEquivalence()
    {
        yield return new object[] { 19 * Mass.OneGram, (1, 5, -1) * Acceleration.OneMetrePerSecondSquared, (0.019, 0.019 * 5, -0.019) * Force.OneNewton };
    }
}
