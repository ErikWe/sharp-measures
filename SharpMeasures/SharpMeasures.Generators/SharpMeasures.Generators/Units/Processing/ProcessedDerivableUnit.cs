namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct ProcessedDerivableUnit
{
    public string Expression { get; }

    public DerivableSignature Signature { get; }
    public IReadOnlyList<string> ParameterNames { get; }

    public ProcessedDerivableUnit(string expression, DerivableSignature signature, IReadOnlyList<string> parameterNames)
    {
        Expression = expression;

        Signature = signature;
        ParameterNames = parameterNames;
    }

    public bool Equals(ProcessedDerivableUnit other)
    {
        return Expression == other.Expression && Signature == other.Signature && ParameterNames.SequenceEqual(other.ParameterNames);
    }

    public override int GetHashCode()
    {
        return Expression.GetHashCode() ^ Signature.GetHashCode() ^ ParameterNames.GetSequenceHashCode();
    }
}
