﻿namespace SharpMeasures.Generators.Units.Pipeline.DefinitionsPipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Stage4.Result result)
    {
        string source = Compose(result, context.CancellationToken);

        if (string.IsNullOrEmpty(source))
        {
            return;
        }

        context.AddSource($"{result.TypeDefinition.Name.Name}_Definitions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string Compose(Stage4.Result data, CancellationToken _)
    {
        StringBuilder source = new();

        string unitName = data.TypeDefinition.Name.Name;
        bool anyDefinitions = false;

        StaticBuilding.AppendAutoGeneratedHeader(source);
        StaticBuilding.AppendNullableDirective(source);

        NamespaceBuilding.AppendNamespace(source, data.TypeDefinition.Name.NameSpace);

        source.Append(data.TypeDefinition.ComposeDeclaration());

        BlockBuilding.AppendBlock(source, typeBlock, originalIndentationLevel: 0);

        void typeBlock(StringBuilder source, Indentation indentation)
        {
            List<string> definedUnits = new();

            AppendDerived(source, indentation, data.TypeDefinition.Name, data.DerivedUnits, definedUnits);
            AppendFixed(source, indentation, data.TypeDefinition.Name, data.Quantity, data.Biased, data.FixedUnits, definedUnits);
            AppendAliases(source, indentation, data.TypeDefinition.Name, data.UnitAliases, definedUnits);

            IList<IDerivedUnitAttributeParameters> dependantUnits
                = GetDependantInstances(data.ScaledUnits, data.PrefixedUnits, data.OffsetUnits);

            AppendDependantUnits(source, indentation, data.TypeDefinition.Name, dependantUnits, definedUnits);

            if (definedUnits.Count > 0)
            {
                anyDefinitions = true;
            }
        }

        if (anyDefinitions)
        {
            return source.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private static void AppendDerived(StringBuilder source, Indentation indentation, NamedType unitName,
        IEnumerable<CachedDerivedUnitAttributeParameters> derivedUnits, IList<string> definedUnits)
    {
        foreach (CachedDerivedUnitAttributeParameters derivedUnit in derivedUnits)
        {
            definedUnits.Add(derivedUnit.Name);

            source.Append($"{indentation}public static {unitName.Name} {derivedUnit.Name} {{ get; }} = ");

            IterativeBuilding.AppendEnumerable(source, "From(", arguments(), ", ", $");{Environment.NewLine}");

            IEnumerable<string> arguments()
            {
                IEnumerator<string?> signatureIterator = derivedUnit.Signature.GetEnumerator();
                IEnumerator<string> unitIterator = derivedUnit.Units.GetEnumerator();

                while (signatureIterator.MoveNext() && unitIterator.MoveNext())
                {
                    yield return $"{signatureIterator.Current}.{unitIterator.Current}";
                }
            }
        }
    }

    private static void AppendFixed(StringBuilder source, Indentation indentation, NamedType unitName, NamedType quantityName, bool biased,
        IEnumerable<FixedUnitAttributeParameters> fixedUnits, IList<string> definedUnits)
    {
        Action<FixedUnitAttributeParameters> appender = biased ? appendBiased : appendUnbiased;

        foreach (FixedUnitAttributeParameters fixedUnit in fixedUnits)
        {
            if (fixedUnit.Name.Length == 0 || fixedUnit.Plural.Length == 0)
            {
                continue;
            }

            definedUnits.Add(fixedUnit.Name);

            appender(fixedUnit);
        }

        void appendDeclaration(FixedUnitAttributeParameters fixedUnit)
        {
            source.Append($"{indentation}public static {unitName.Name} {fixedUnit.Name} {{ get; }}");
        }

        void appendUnbiased(FixedUnitAttributeParameters fixedUnit)
        {
            appendDeclaration(fixedUnit);
            source.Append($" = new(new {quantityName.FullyQualifiedName}({fixedUnit.Value}));{Environment.NewLine}");
        }

        void appendBiased(FixedUnitAttributeParameters fixedUnit)
        {
            appendDeclaration(fixedUnit);
            source.Append($" = new(new {quantityName.FullyQualifiedName}({fixedUnit.Value}), new SharpMeasures.Scalar({fixedUnit.Bias});{Environment.NewLine}");
        }
    }

    private static void AppendAliases(StringBuilder source, Indentation indentation, NamedType unitName, IEnumerable<UnitAliasAttributeParameters> unitAliases,
        IList<string> definedUnits)
    {
        foreach (UnitAliasAttributeParameters unitAlias in unitAliases)
        {
            definedUnits.Add(unitAlias.Name);

            source.Append($"{indentation}public static {unitName.Name} {unitAlias.Name} => {unitAlias.AliasOf};{Environment.NewLine}");
        }
    }

    private static IList<IDerivedUnitAttributeParameters> GetDependantInstances(IEnumerable<ScaledUnitAttributeParameters> scaledUnits,
        IEnumerable<PrefixedUnitAttributeParameters> prefixedUnits, IEnumerable<OffsetUnitAttributeParameters> offsetUnits)
    {
        List<IDerivedUnitAttributeParameters> result = new();

        foreach (ScaledUnitAttributeParameters scaledUnit in scaledUnits)
        {
            result.Add(scaledUnit);
        }

        foreach (PrefixedUnitAttributeParameters prefixedUnit in prefixedUnits)
        {
            result.Add(prefixedUnit);
        }

        foreach (OffsetUnitAttributeParameters offsetUnit in offsetUnits)
        {
            result.Add(offsetUnit);
        }

        return result;
    }

    private static void AppendDependantUnits(StringBuilder source, Indentation indentation, NamedType unitName,
        IList<IDerivedUnitAttributeParameters> dependantUnits, IList<string> definedUnits)
    {
        int initialLength = dependantUnits.Count;

        for (int i = 0; i < dependantUnits.Count; i++)
        {
            if (definedUnits.Contains(dependantUnits[i].DerivedFrom))
            {
                source.Append($"{indentation}public static {unitName.Name} {dependantUnits[i].Name} {{ get; }} = ");

                if (dependantUnits[i] is ScaledUnitAttributeParameters scaledUnit)
                {
                    AppendScaled(source, scaledUnit);
                }
                else if (dependantUnits[i] is PrefixedUnitAttributeParameters prefixedUnit)
                {
                    AppendPrefixed(source, prefixedUnit);
                }
                else if (dependantUnits[i] is OffsetUnitAttributeParameters offsetUnit)
                {
                    AppendOffset(source, offsetUnit);
                }

                source.Append($";{Environment.NewLine}");
             
                definedUnits.Add(dependantUnits[i].Name);
                dependantUnits.RemoveAt(i);
                i--;
            }
        }

        if (dependantUnits.Count < initialLength)
        {
            AppendDependantUnits(source, indentation, unitName, dependantUnits, definedUnits);
        }
    }

    private static void AppendScaled(StringBuilder source, ScaledUnitAttributeParameters scaledUnit)
    {
        source.Append($"{scaledUnit.From}.ScaledBy({scaledUnit.Scale})");
    }

    private static void AppendPrefixed(StringBuilder source, PrefixedUnitAttributeParameters prefixedUnit)
    {
        source.Append($"{prefixedUnit.From}.WithPrefix(SharpMeasures.MetricPrefix.{prefixedUnit.Prefix})");
    }

    private static void AppendOffset(StringBuilder source, OffsetUnitAttributeParameters offsetUnit)
    {
        source.Append($"{offsetUnit.From}.ScaledBy({offsetUnit.Offset})");
    }
}
