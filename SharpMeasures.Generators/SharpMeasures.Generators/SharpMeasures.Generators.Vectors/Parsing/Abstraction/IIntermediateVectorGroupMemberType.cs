namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal interface IIntermediateVectorGroupMemberType
{
    public DefinedType Type { get; }

    public abstract IVectorGroupMember Definition { get; }

    public abstract IReadOnlyList<IVectorConstant> Constants { get; }
}
