namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Power
{
    /// <summary>Computes <see cref="Power"/> according to { <see cref="Power"/> = <paramref name="work"/> / <paramref name="time"/> },
    /// where <paramref name="work"/> is the <see cref="Work"/> being done over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Power From(Work work, Time time) => new(work.Magnitude / time.Magnitude);
}
