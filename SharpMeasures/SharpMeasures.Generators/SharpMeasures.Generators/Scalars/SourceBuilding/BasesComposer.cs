namespace SharpMeasures.Generators.Scalars.SourceBuilding;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Scalars.Pipeline;
using SharpMeasures.Generators.SourceBuilding;

using System.Text;
using System.Threading;

internal static class BasesComposer
{
    public static void Append(StringBuilder source, Indentation indentation, Stage5.Result data, CancellationToken _)
    {
        foreach (UnitAliasDefinition unitAlias in data.UnitAliases)
        {
            if (shouldInclude(unitAlias.Name))
            {
                source.Append($"{indentation}public {data.TypeDefinition.Name} One{unitAlias.Name}");
            }
        }

        bool shouldInclude(string candidate) => ShouldInclude(candidate, data.IncludedBases, data.ExcludedBases);
    }

    private static bool ShouldInclude(string candidate, IncludeBasesDefinition? included, ExcludeBasesDefinition? excluded)
    {
        if (included is null)
        {
            if (excluded is null)
            {
                return true;
            }

            foreach (string excludedBase in excluded.Value.ExcludedBases)
            {
                if (excludedBase == candidate)
                {
                    return false;
                }
            }

            return true;
        }
        else if (excluded is null)
        {
            foreach (string includedBase in included.Value.IncludedBases)
            {
                if (includedBase == candidate)
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }
}
