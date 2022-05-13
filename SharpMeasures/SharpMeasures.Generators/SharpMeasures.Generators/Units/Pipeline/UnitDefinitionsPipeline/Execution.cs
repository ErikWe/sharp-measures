﻿namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DefinitionsStage.Result result)
    {
        if (result.DerivedUnits.Count is 0 || result.FixedUnits.Count is 0 || result.UnitAliases.Count is 0 || result.OffsetUnits.Count is 0
            || result.PrefixedUnits.Count is 0 || result.ScaledUnits.Count is 0)
        {
            return;
        }

        string source = Composer.Compose(context, result);

        context.AddSource($"{result.TypeDefinition.Name}_Definitions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(SourceProductionContext context, DefinitionsStage.Result data)
        {
            Composer composer = new(context, data);
            composer.Compose();
            return composer.Retrieve();
        }

        private SourceProductionContext Context { get; }
        private DocumentationFile Documentation { get; }
        private StringBuilder Builder { get; } = new();

        private DefinedType UnitType { get; }
        private NamedType QuantityType { get; }
        private bool Biased { get; }
        private IEnumerable<UnitAliasParameters> UnitAliases { get; }
        private IEnumerable<DerivedUnitParameters> DerivedUnits { get; }
        private IEnumerable<FixedUnitParameters> FixedUnits { get; }
        private IEnumerable<OffsetUnitParameters> OffsetUnits { get; }
        private IEnumerable<PrefixedUnitParameters> PrefixedUnits { get; }
        private IEnumerable<ScaledUnitParameters> ScaledUnits { get; }

        private HashSet<string> ImplementedDefinitions { get; } = new();

        private Composer(SourceProductionContext context, DefinitionsStage.Result data)
        {
            Context = context;
            Documentation = data.Documentation;

            UnitType = data.TypeDefinition;
            QuantityType = data.Quantity;
            Biased = data.Biased;
            UnitAliases = data.UnitAliases;
            DerivedUnits = data.DerivedUnits;
            FixedUnits = data.FixedUnits;
            OffsetUnits = data.OffsetUnits;
            PrefixedUnits = data.PrefixedUnits;
            ScaledUnits = data.ScaledUnits;
        }

        private void Compose()
        {
            StaticBuilding.AppendAutoGeneratedHeader(Builder);
            StaticBuilding.AppendNullableDirective(Builder);

            NamespaceBuilding.AppendNamespace(Builder, UnitType.Namespace);

            Builder.Append(UnitType.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(StringBuilder source, Indentation indentation)
        {
            AppendDerived(indentation);
            AppendFixed(indentation);

            AppendDependantUnits(indentation);
        }

        private void AppendDerived(Indentation indentation)
        {
            foreach (DerivedUnitParameters derivedUnit in DerivedUnits)
            {
                ImplementedDefinitions.Add(derivedUnit.Name);

                DocumentationBuilding.AppendDocumentation(Context, Builder, Documentation, indentation, $"Definition_{derivedUnit.Name}");
                Builder.Append($"{indentation}public static {UnitType.Name} {derivedUnit.Name} {{ get; }} = ");

                IterativeBuilding.AppendEnumerable(Builder, "From(", arguments(), ", ", $");{Environment.NewLine}");

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

        private void AppendFixed(Indentation indentation)
        {
            Action<FixedUnitParameters> appender = Biased ? appendBiased : appendUnbiased;

            foreach (FixedUnitParameters fixedUnit in FixedUnits)
            {
                ImplementedDefinitions.Add(fixedUnit.Name);

                appender(fixedUnit);
            }

            void appendDeclaration(FixedUnitParameters fixedUnit)
            {
                DocumentationBuilding.AppendDocumentation(Context, Builder, Documentation, indentation, $"Definition_{fixedUnit.Name}");
                Builder.Append($"{indentation}public static {UnitType.Name} {fixedUnit.Name} {{ get; }}");
            }

            void appendUnbiased(FixedUnitParameters fixedUnit)
            {
                appendDeclaration(fixedUnit);
                Builder.Append($" = new(new {QuantityType.FullyQualifiedName}({fixedUnit.Value}));{Environment.NewLine}");
            }

            void appendBiased(FixedUnitParameters fixedUnit)
            {
                appendDeclaration(fixedUnit);
                Builder.Append($" = new(new {QuantityType.FullyQualifiedName}({fixedUnit.Value}), new SharpMeasures.Scalar({fixedUnit.Bias}));{Environment.NewLine}");
            }
        }

        private void AppendDependantUnits(Indentation indentation) => AppendDependantUnits(indentation, GetDependantInstances());

        private void AppendDependantUnits(Indentation indentation, IList<IDependantUnitDefinitionParameters> dependantUnits)
        {
            int initialLength = dependantUnits.Count;

            for (int i = 0; i < dependantUnits.Count; i++)
            {
                if (ImplementedDefinitions.Contains(dependantUnits[i].DependantOn))
                {
                    DocumentationBuilding.AppendDocumentation(Context, Builder, Documentation, indentation, $"Definition_{UnitType.Name}");
                    Builder.Append($"{indentation}public static {UnitType.Name} {dependantUnits[i].Name} ");

                    if (dependantUnits[i] is UnitAliasParameters unitAlias)
                    {
                        AppendAlias(unitAlias);
                    }
                    else if (dependantUnits[i] is ScaledUnitParameters scaledUnit)
                    {
                        AppendScaled(scaledUnit);
                    }
                    else if (dependantUnits[i] is PrefixedUnitParameters prefixedUnit)
                    {
                        AppendPrefixed(prefixedUnit);
                    }
                    else if (dependantUnits[i] is OffsetUnitParameters offsetUnit)
                    {
                        AppendOffset(offsetUnit);
                    }

                    Builder.Append($";{Environment.NewLine}");

                    ImplementedDefinitions.Add(dependantUnits[i].Name);
                    dependantUnits.RemoveAt(i);
                    i--;
                }
            }

            if (dependantUnits.Count is 0)
            {
                return;
            }
            else if (dependantUnits.Count < initialLength)
            {
                AppendDependantUnits(indentation, dependantUnits);
            }
            else
            {
                // TODO: Unresolved dependencies
            }
        }

        private void AppendAlias(UnitAliasParameters unitAlias)
        {
            Builder.Append($"=> {unitAlias.AliasOf}");
        }

        private void AppendScaled(ScaledUnitParameters scaledUnit)
        {
            Builder.Append($"{{ get; }} = {scaledUnit.From}.ScaledBy({scaledUnit.Scale})");
        }

        private void AppendPrefixed(PrefixedUnitParameters prefixedUnit)
        {
            Builder.Append($"{{ get; }} = {prefixedUnit.From}.WithPrefix(SharpMeasures.{prefixText()})");

            string prefixText() => prefixedUnit.SpecifiedPrefixType switch
            {
                PrefixedUnitParameters.PrefixType.Metric => metricText(),
                PrefixedUnitParameters.PrefixType.Binary => binaryText(),
                _ => string.Empty
            };

            string metricText() => $"MetricPrefix.{prefixedUnit.MetricPrefixName}";
            string binaryText() => $"BinaryPrefix.{prefixedUnit.BinaryPrefixName}";
        }

        private void AppendOffset(OffsetUnitParameters offsetUnit)
        {
            Builder.Append($"{{ get; }} = {offsetUnit.From}.ScaledBy({offsetUnit.Offset})");
        }

        private List<IDependantUnitDefinitionParameters> GetDependantInstances()
        {
            List<IDependantUnitDefinitionParameters> result = new();

            foreach (UnitAliasParameters unitAlias in UnitAliases)
            {
                result.Add(unitAlias);
            }

            foreach (OffsetUnitParameters offsetUnit in OffsetUnits)
            {
                result.Add(offsetUnit);
            }

            foreach (PrefixedUnitParameters prefixedUnit in PrefixedUnits)
            {
                result.Add(prefixedUnit);
            }

            foreach (ScaledUnitParameters scaledUnit in ScaledUnits)
            {
                result.Add(scaledUnit);
            }

            return result;
        }
    }
}
