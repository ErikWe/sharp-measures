namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Position3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(PositionDisplacementEquivalence))]
    public void PositionDisplacement_ShouldBeEquivalent(Position3 position, Displacement3 displacement, Position3 expected)
    {
        IEnumerable<Position3> actual = new Position3[]
        {
            Position3.From(position, displacement),
            position.Add(displacement),
            position + displacement,
            displacement.Add(position),
            displacement + position
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> PositionDisplacementEquivalence()
    {
        yield return new object[] { ((1, 5, -1) * Length.OneMetre).AsPosition, (3, -9, 0) * Length.OneKilometre, ((3001, -8995, -1) * Length.OneMetre).AsPosition };
    }
}
