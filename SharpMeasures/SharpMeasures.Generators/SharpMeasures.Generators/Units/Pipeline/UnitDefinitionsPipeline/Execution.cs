﻿namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Attributes.Parsing.Units;
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

        string documentedSource = result.Documentation.ResolveTextAndReportDiagnostics(context, source);

        context.AddSource($"{result.TypeDefinition.Name.Name}_Definitions.g.cs", SourceText.From(documentedSource, Encoding.UTF8));
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
            HashSet<string> definedUnits = new();

            AppendDerived(source, indentation, data.TypeDefinition.Name, data.DerivedUnits, definedUnits);
            AppendFixed(source, indentation, data.TypeDefinition.Name, data.Quantity, data.Biased, data.FixedUnits, definedUnits);

            IList<IDependantUnitDefinitionParameters> dependantUnits
                = GetDependantInstances(data.UnitAliases, data.ScaledUnits, data.PrefixedUnits, data.OffsetUnits);

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
        IEnumerable<DerivedUnitParameters> derivedUnits, ICollection<string> definedUnits)
    {
        foreach (DerivedUnitParameters derivedUnit in derivedUnits)
        {
            if (definedUnits.Contains(derivedUnit.Name))
            {
                continue;
            }

            definedUnits.Add(derivedUnit.Name);

            source.Append($"{indentation}public static {unitName.Name} {derivedUnit.Name} {{ get; }} = ");

            IterativeBuilding.AppendEnumerable(source, "From(", arguments(), ", ", $");{Environment.NewLine}");

            IEnumerable<string> arguments()
            {
                IEnumerator<INamedTypeSymbol> signatureIterator = derivedUnit.Signature.GetEnumerator();
                IEnumerator<string> unitIterator = derivedUnit.Units.GetEnumerator();

                while (signatureIterator.MoveNext() && unitIterator.MoveNext())
                {
                    yield return $"{signatureIterator.Current}.{unitIterator.Current}";
                }
            }
        }
    }

    private static void AppendFixed(StringBuilder source, Indentation indentation, NamedType unitName, NamedType quantityName, bool biased,
        IEnumerable<FixedUnitParameters> fixedUnits, ICollection<string> definedUnits)
    {
        Action<FixedUnitParameters> appender = biased ? appendBiased : appendUnbiased;

        foreach (FixedUnitParameters fixedUnit in fixedUnits)
        {
            if (definedUnits.Contains(fixedUnit.Name))
            {
                continue;
            }

            definedUnits.Add(fixedUnit.Name);

            appender(fixedUnit);
        }

        void appendDeclaration(FixedUnitParameters fixedUnit)
        {
            source.Append($"{indentation}public static {unitName.Name} {fixedUnit.Name} {{ get; }}");
        }

        void appendUnbiased(FixedUnitParameters fixedUnit)
        {
            appendDeclaration(fixedUnit);
            source.Append($" = new(new {quantityName.FullyQualifiedName}({fixedUnit.Value}));{Environment.NewLine}");
        }

        void appendBiased(FixedUnitParameters fixedUnit)
        {
            appendDeclaration(fixedUnit);
            source.Append($" = new(new {quantityName.FullyQualifiedName}({fixedUnit.Value}), new SharpMeasures.Scalar({fixedUnit.Bias}));{Environment.NewLine}");
        }
    }

    private static IList<IDependantUnitDefinitionParameters> GetDependantInstances(IEnumerable<UnitAliasParameters> unitAliases,
        IEnumerable<ScaledUnitParameters> scaledUnits, IEnumerable<PrefixedUnitParameters> prefixedUnits,
        IEnumerable<OffsetUnitParameters> offsetUnits)
    {
        List<IDependantUnitDefinitionParameters> result = new();

        foreach (UnitAliasParameters unitAlias in unitAliases)
        {
            result.Add(unitAlias);
        }

        foreach (ScaledUnitParameters scaledUnit in scaledUnits)
        {
            result.Add(scaledUnit);
        }

        foreach (PrefixedUnitParameters prefixedUnit in prefixedUnits)
        {
            result.Add(prefixedUnit);
        }

        foreach (OffsetUnitParameters offsetUnit in offsetUnits)
        {
            result.Add(offsetUnit);
        }

        return result;
    }

    private static void AppendDependantUnits(StringBuilder source, Indentation indentation, NamedType unitName,
        IList<IDependantUnitDefinitionParameters> dependantUnits, HashSet<string> definedUnits)
    {
        int initialLength = dependantUnits.Count;

        for (int i = 0; i < dependantUnits.Count; i++)
        {
            if (definedUnits.Contains(dependantUnits[i].Name))
            {
                dependantUnits.RemoveAt(i);
                i--;
                continue;
            }

            if (definedUnits.Contains(dependantUnits[i].DependantOn))
            {
                source.Append($"{indentation}public static {unitName.Name} {dependantUnits[i].Name} ");

                if (dependantUnits[i] is UnitAliasParameters unitAlias)
                {
                    AppendAlias(source, unitAlias);
                }
                else if (dependantUnits[i] is ScaledUnitParameters scaledUnit)
                {
                    AppendScaled(source, scaledUnit);
                }
                else if (dependantUnits[i] is PrefixedUnitParameters prefixedUnit)
                {
                    AppendPrefixed(source, prefixedUnit);
                }
                else if (dependantUnits[i] is OffsetUnitParameters offsetUnit)
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

    private static void AppendAlias(StringBuilder source, UnitAliasParameters unitAlias)
    {
        source.Append($"=> {unitAlias.AliasOf}");
    }

    private static void AppendScaled(StringBuilder source, ScaledUnitParameters scaledUnit)
    {
        source.Append($"{{ get; }} = {scaledUnit.From}.ScaledBy({scaledUnit.Scale})");
    }

    private static void AppendPrefixed(StringBuilder source, PrefixedUnitParameters prefixedUnit)
    {
        source.Append($"{{ get; }} = {prefixedUnit.From}.WithPrefix(SharpMeasures.{prefixText()})");

        string prefixText() => prefixedUnit.SpecifiedPrefixType switch
        {
            PrefixedUnitParameters.PrefixType.Metric => metricText(),
            PrefixedUnitParameters.PrefixType.Binary => binaryText(),
            _ => string.Empty
        };

        string metricText() => $"MetricPrefix.{prefixedUnit.MetricPrefixName}";
        string binaryText() => $"BinaryPrefix.{prefixedUnit.BinaryPrefixName}";
    }

    private static void AppendOffset(StringBuilder source, OffsetUnitParameters offsetUnit)
    {
        source.Append($"{{ get; }} = {offsetUnit.From}.ScaledBy({offsetUnit.Offset})");
    }
}
