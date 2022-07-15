namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

public interface IVectorGroup : ISharpMeasuresObject
{
    public abstract IReadOnlyList<IUnresolvedVectorType> Members { get; }

    public abstract bool? ImplementSum { get; }
    public abstract bool? ImplementDifference { get; }

    public abstract IUnresolvedVectorGroup? Difference { get; }

    public abstract IUnresolvedUnitInstance? DefaultUnit { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
