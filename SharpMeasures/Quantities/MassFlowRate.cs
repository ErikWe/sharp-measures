namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct MassFlowRate
{
    /// <summary>Computes average <see cref="MassFlowRate"/> according to { <paramref name="mass"/> / <paramref name="time"/> },
    /// where <paramref name="mass"/> is the change in <see cref="Mass"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static MassFlowRate From(Mass mass, Time time) => new(mass.Magnitude / time.Magnitude);
}
