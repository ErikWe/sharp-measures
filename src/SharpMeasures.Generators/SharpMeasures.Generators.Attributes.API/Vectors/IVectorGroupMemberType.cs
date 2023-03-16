namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorGroupMemberType : IQuantityType
{
    new public abstract IVectorGroupMember Definition { get; }

    public abstract IReadOnlyList<IVectorOperation> VectorOperations { get; }
    public abstract IReadOnlyList<IQuantityProcess> Processes { get; }
    public abstract IReadOnlyList<IVectorConstant> Constants { get; }

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }
}
