namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Torque3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DisplacementTimeEquivalence))]
    public void DisplacementForce_ShouldBeEquivalent(Displacement3 displacement, Force3 force, Torque3 expected)
    {
        IEnumerable<Torque3> actual = new Torque3[]
        {
            Torque3.From(displacement, force),
            displacement.Cross(force)
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> DisplacementTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Length.OneCentimetre, (3, 5, -7) * Force.OneNewton, (-0.3, 0.04, -0.1) * Torque.OneNewtonMetre };
    }
}
