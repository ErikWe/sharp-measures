namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel result)
    {
        string source = Composer.Compose(result);

        context.AddSource($"{result.Unit.Name}_Definitions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private HashSet<string> ImplementedDefinitions { get; } = new();

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            Builder.Append(Data.Unit.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendFixed(indentation);
            AppendDerived(indentation);

            AppendDependantUnits(indentation);
        }

        private void AppendFixed(Indentation indentation)
        {
            if (Data.FixedUnit is null)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            ImplementedDefinitions.Add(Data.FixedUnit.Name);

            AppendDocumentation(indentation, Data.Documentation.FixedDefinition(Data.FixedUnit));
            Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {Data.FixedUnit.Name} {{ get; }}");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($" = new(new {Data.Quantity.FullyQualifiedName}(1), new global::SharpMeasures.Scalar(0));");
            }
            else
            {
                Builder.AppendLine($" = new(new {Data.Quantity.FullyQualifiedName}(1));");
            }
        }

        private void AppendDerived(Indentation indentation)
        {
            foreach (DerivedUnitDefinition derivedUnit in Data.DerivedUnits)
            {
                SeparationHandler.AddIfNecessary();

                ImplementedDefinitions.Add(derivedUnit.Name);

                AppendDocumentation(indentation, Data.Documentation.DerivedDefinition(derivedUnit));
                Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {derivedUnit.Name} {{ get; }} = ");

                IterativeBuilding.AppendEnumerable(Builder, "From(", arguments(), ", ", $");{Environment.NewLine}");

                IEnumerable<string> arguments()
                {
                    IEnumerator<NamedType> signatureIterator = getDerivation(derivedUnit).Signature.GetEnumerator();
                    IEnumerator<string> unitIterator = derivedUnit.Units.GetEnumerator();

                    while (signatureIterator.MoveNext() && unitIterator.MoveNext())
                    {
                        yield return $"{signatureIterator.Current.FullyQualifiedName}.{unitIterator.Current}";
                    }
                }
            }

            IDerivableUnit getDerivation(DerivedUnitDefinition derivedUnit)
            {
                if (derivedUnit.DerivationID is not null and { Length: > 0 })
                {
                    return Data.DerivationsByID[derivedUnit.DerivationID];
                }

                return Data.DerivationsByID.Values.Single();
            }
        }

        private void AppendDependantUnits(Indentation indentation) => AppendDependantUnits(indentation, GetDependantInstances());

        private void AppendDependantUnits(Indentation indentation, IList<IDependantUnitDefinition<IDependantUnitLocations>> dependantUnits)
        {
            int initialLength = dependantUnits.Count;

            for (int i = 0; i < dependantUnits.Count; i++)
            {
                if (ImplementedDefinitions.Contains(dependantUnits[i].DependantOn))
                {
                    SeparationHandler.AddIfNecessary();

                    var commonDelegate = () => Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {dependantUnits[i].Name} ");

                    if (dependantUnits[i] is UnitAliasDefinition unitAlias)
                    {
                        AppendDocumentation(indentation, Data.Documentation.AliasDefinition(unitAlias));
                        commonDelegate();
                        AppendAlias(unitAlias);
                    }
                    else if (dependantUnits[i] is ScaledUnitDefinition scaledUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.ScaledDefinition(scaledUnit));
                        commonDelegate();
                        AppendScaled(scaledUnit);
                    }
                    else if (dependantUnits[i] is PrefixedUnitDefinition prefixedUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.PrefixedDefinition(prefixedUnit));
                        commonDelegate();
                        AppendPrefixed(prefixedUnit);
                    }
                    else if (dependantUnits[i] is BiasedUnitDefinition biasedUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.BiasedDefinition(biasedUnit));
                        commonDelegate();
                        AppendBiased(biasedUnit);
                    }

                    Builder.AppendLine($";");

                    ImplementedDefinitions.Add(dependantUnits[i].Name);
                    dependantUnits.RemoveAt(i);
                    i -= 1;
                }
            }

            if (dependantUnits.Count is 0)
            {
                return;
            }
            
            if (dependantUnits.Count < initialLength)
            {
                AppendDependantUnits(indentation, dependantUnits);
                return;
            }
        }

        private void AppendAlias(UnitAliasDefinition unitAlias)
        {
            Builder.Append($"=> {unitAlias.AliasOf}");
        }

        private void AppendScaled(ScaledUnitDefinition scaledUnit)
        {
            Builder.Append($"{{ get; }} = {scaledUnit.From}.ScaledBy({scaledUnit.Expression})");
        }

        private void AppendPrefixed(PrefixedUnitDefinition prefixedUnit)
        {
            string prefixText = prefixedUnit.Locations.ExplicitlySetMetricPrefix
                ? $"global::SharpMeasures.MetricPrefix.{prefixedUnit.MetricPrefix}"
                : $"global::SharpMeasures.BinaryPrefix.{prefixedUnit.BinaryPrefix}";

            Builder.Append($"{{ get; }} = {prefixedUnit.From}.WithPrefix({prefixText})");
        }

        private void AppendBiased(BiasedUnitDefinition biasedUnit)
        {
            Builder.Append($"{{ get; }} = {biasedUnit.From}.WithBias({biasedUnit.Expression})");
        }

        private List<IDependantUnitDefinition<IDependantUnitLocations>> GetDependantInstances()
        {
            List<IDependantUnitDefinition<IDependantUnitLocations>> result = new();

            foreach (var unitAlias in Data.UnitAliases)
            {
                result.Add(unitAlias);
            }

            foreach (var biasedUnit in Data.BiasedUnits)
            {
                result.Add(biasedUnit);
            }

            foreach (var prefixedUnit in Data.PrefixedUnits)
            {
                result.Add(prefixedUnit);
            }

            foreach (var scaledUnit in Data.ScaledUnits)
            {
                result.Add(scaledUnit);
            }

            return result;
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
