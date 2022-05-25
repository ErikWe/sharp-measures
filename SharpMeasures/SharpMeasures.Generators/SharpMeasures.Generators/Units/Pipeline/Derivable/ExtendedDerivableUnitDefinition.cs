namespace SharpMeasures.Generators.Units.Pipeline.Derivable;

using SharpMeasures.Generators.Attributes.Parsing.Units;

using System.Collections.Generic;
using System.Linq;

internal record class ExtendedDerivableUnitDefinition
{
    private DerivableUnitDefinition OriginalDefinition { get; }

    public string Expression => OriginalDefinition.Expression;
    public IReadOnlyList<NamedType> Signature => OriginalDefinition.Signature;
    public IReadOnlyList<NamedType> QuantitiesOfSignatureUnits { get; }

    public ExtendedDerivableUnitDefinition(DerivableUnitDefinition originalDefinition, IReadOnlyList<NamedType> quantitiesOfSignatureUnits)
    {
        OriginalDefinition = originalDefinition;
        QuantitiesOfSignatureUnits = quantitiesOfSignatureUnits;
    }

    public virtual bool Equals(ExtendedDerivableUnitDefinition other)
    {
        if (other is null)
        {
            return false;
        }

        return OriginalDefinition.Equals(other.OriginalDefinition) && QuantitiesOfSignatureUnits.SequenceEqual(other.QuantitiesOfSignatureUnits);
    }

    public override int GetHashCode()
    {
        return OriginalDefinition.GetHashCode() ^ QuantitiesOfSignatureUnits.GetSequenceHashCode();
    }
}
