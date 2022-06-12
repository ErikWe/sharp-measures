namespace SharpMeasures.Generators.Units.Refinement.DerivableUnit;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal readonly record struct RefinedDerivableUnitDefinition
{
    public string Expression { get; }

    public DerivableSignature Signature { get; }
    public ReadOnlyEquatableList<string> ParameterNames { get; }

    public RefinedDerivableUnitDefinition(string expression, DerivableSignature signature, IReadOnlyList<string> parameterNames)
    {
        Expression = expression;

        Signature = signature;
        ParameterNames = parameterNames.AsReadOnlyEquatable();
    }
}
