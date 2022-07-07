namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class DerivableUnitDefinition : IAttributeDefinition<DerivableUnitLocations>, IDerivableUnit
{
    public string DerivationID { get; }
    public string Expression { get; }
    public UnitDerivationSignature Signature { get; }
    public IReadOnlyList<string> ParameterNames => parameterNames;

    private ReadOnlyEquatableList<string> parameterNames { get; }

    public DerivableUnitLocations Locations { get; }

    public DerivableUnitDefinition(string derivationID, string expression, UnitDerivationSignature signature, IReadOnlyList<string> parameterNames, DerivableUnitLocations locations)
    {
        DerivationID = derivationID;
        Expression = expression;
        Signature = signature;
        this.parameterNames = parameterNames.AsReadOnlyEquatable();

        Locations = locations;
    }
}
