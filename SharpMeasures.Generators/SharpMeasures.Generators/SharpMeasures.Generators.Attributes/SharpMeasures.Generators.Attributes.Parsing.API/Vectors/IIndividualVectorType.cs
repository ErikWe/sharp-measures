namespace SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

public interface IIndividualVectorType : IVectorGroupType
{
    new public abstract IIndividualVector Definition { get; }

    public abstract IReadOnlyList<IVectorConstant> Constants { get; }

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }
}
