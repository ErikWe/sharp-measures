namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class DerivableSignature : ReadOnlyEquatableList<NamedType>
{
    public DerivableSignature(IReadOnlyList<NamedType> types) : base(types) { }
}
