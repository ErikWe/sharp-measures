namespace SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

public interface IUnresolvedIndividualVectorType : IUnresolvedVectorGroupType
{
    new public abstract IUnresolvedIndividualVector Definition { get; }

    public abstract IReadOnlyList<IUnresolvedVectorConstant> Constants { get; }
}
