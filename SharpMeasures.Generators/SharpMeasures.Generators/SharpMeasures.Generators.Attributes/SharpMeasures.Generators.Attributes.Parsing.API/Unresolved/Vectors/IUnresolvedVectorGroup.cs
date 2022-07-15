namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;
public interface IUnresolvedVectorGroup
{
    public abstract IReadOnlyList<IUnresolvedRegisteredVectorGroupMember> Members { get; }

    public abstract bool? ImplementSum { get; }
    public abstract bool? ImplementDifference { get; }

    public abstract NamedType? Difference { get; }

    public abstract NamedType? DefaultUnit { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
