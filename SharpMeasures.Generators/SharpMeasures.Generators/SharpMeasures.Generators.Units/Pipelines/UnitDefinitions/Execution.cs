namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel result)
    {
        if (result.FixedUnit is null && result.DerivedUnits.Any() is false && result.UnitAliases.Any() is false && result.BiasedUnits.Any() is false
            && result.PrefixedUnits.Any() is false && result.ScaledUnits.Any() is false)
        {
            return;
        }

        string source = Composer.Compose(context, result);

        context.AddSource($"{result.Unit.Name}_Definitions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(SourceProductionContext context, DataModel data)
        {
            Composer composer = new(context, data);
            composer.Compose();
            composer.ReportDiagnostics();
            return composer.Retrieve();
        }

        private SourceProductionContext Context { get; }
        private StringBuilder Builder { get; } = new();
        private UsingsCollector UsingsCollector { get; }

        private DataModel Data { get; }

        private HashSet<string> ImplementedDefinitions { get; } = new();

        private List<Diagnostic> Diagnostics { get; } = new();

        private Composer(SourceProductionContext context, DataModel data)
        {
            Context = context;
            Data = data;
            UsingsCollector = UsingsCollector.Delayed(Builder, data.Unit.Namespace);

            UsingsCollector.AddUsing("SharpMeasures");
            UsingsCollector.AddUsing(Data.Quantity.Namespace);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.Append(Data.Unit.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            UsingsCollector.InsertUsings();
        }

        private void ReportDiagnostics()
        {
            Context.ReportDiagnostics(Diagnostics);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
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

            ImplementedDefinitions.Add(Data.FixedUnit.Name);

            AppendDocumentation(indentation, Data.Documentation.Definition(Data.FixedUnit));
            Builder.Append($"{indentation}public static {Data.Unit.Name} {Data.FixedUnit.Name} {{ get; }}");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($" = new(new {Data.Quantity.Name}(1), new Scalar(0));");
            }
            else
            {
                Builder.AppendLine($" = new(new {Data.Quantity.Name}(1));");
            }
        }

        private void AppendDerived(Indentation indentation)
        {
            foreach (DerivedUnitDefinition derivedUnit in Data.DerivedUnits)
            {
                ImplementedDefinitions.Add(derivedUnit.Name);

                AppendDocumentation(indentation, Data.Documentation.Definition(derivedUnit));
                Builder.Append($"{indentation}public static {Data.Unit.Name} {derivedUnit.Name} {{ get; }} = ");

                IterativeBuilding.AppendEnumerable(Builder, "From(", arguments(), ", ", $");{Environment.NewLine}");

                IEnumerable<string> arguments()
                {
                    IEnumerator<NamedType> signatureIterator = derivedUnit.Signature.GetEnumerator();
                    IEnumerator<IUnresolvedUnitInstance> unitIterator = derivedUnit.Units.GetEnumerator();

                    while (signatureIterator.MoveNext() && unitIterator.MoveNext())
                    {
                        UsingsCollector.AddUsing(signatureIterator.Current.Namespace);
                        yield return $"{signatureIterator.Current.Name}.{unitIterator.Current}";
                    }
                }
            }
        }

        private void AppendDependantUnits(Indentation indentation) => AppendDependantUnits(indentation, GetDependantInstances());

        private void AppendDependantUnits(Indentation indentation, IList<IDependantUnitDefinition<IDependantUnitLocations>> dependantUnits)
        {
            int initialLength = dependantUnits.Count;

            for (int i = 0; i < dependantUnits.Count; i++)
            {
                if (ImplementedDefinitions.Contains(dependantUnits[i].DependantOn.Name))
                {
                    AppendDocumentation(indentation, Data.Documentation.Definition(dependantUnits[i]));
                    Builder.Append($"{indentation}public static {Data.Unit.Name} {dependantUnits[i].Name} ");

                    if (dependantUnits[i] is UnresolvedUnitAliasDefinition unitAlias)
                    {
                        AppendAlias(unitAlias);
                    }
                    else if (dependantUnits[i] is UnresolvedScaledUnitDefinition scaledUnit)
                    {
                        AppendScaled(scaledUnit);
                    }
                    else if (dependantUnits[i] is UnresolvedPrefixedUnitDefinition prefixedUnit)
                    {
                        AppendPrefixed(prefixedUnit);
                    }
                    else if (dependantUnits[i] is UnresolvedBiasedUnitDefinition biasedUnit)
                    {
                        AppendBiased(biasedUnit);
                    }

                    Builder.AppendLine($";");

                    ImplementedDefinitions.Add(dependantUnits[i].Name);
                    dependantUnits.RemoveAt(i);
                    i--;
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

            CreateCyclicDependencyDiagnostics(dependantUnits);
        }

        private void AppendAlias(UnresolvedUnitAliasDefinition unitAlias)
        {
            Builder.Append($"=> {unitAlias.AliasOf}");
        }

        private void AppendScaled(UnresolvedScaledUnitDefinition scaledUnit)
        {
            Builder.Append($"{{ get; }} = {scaledUnit.From}.ScaledBy({scaledUnit.Expression})");
        }

        private void AppendPrefixed(UnresolvedPrefixedUnitDefinition prefixedUnit)
        {
            string prefixText = prefixedUnit.Locations.ExplicitlySetMetricPrefixName
                ? $"MetricPrefix.{prefixedUnit.MetricPrefix}"
                : $"BinaryPrefix.{prefixedUnit.BinaryPrefix}";

            Builder.Append($"{{ get; }} = {prefixedUnit.From}.WithPrefix({prefixText})");
        }

        private void AppendBiased(UnresolvedBiasedUnitDefinition biasedUnit)
        {
            Builder.Append($"{{ get; }} = {biasedUnit.From}.WithBias({biasedUnit.Expression})");
        }

        private List<IDependantUnitDefinition<IDependantUnitLocations>> GetDependantInstances()
        {
            List<IDependantUnitDefinition<IDependantUnitLocations>> result = new();

            foreach (UnitAliasDefinition unitAlias in Data.UnitAliases)
            {
                result.Add(unitAlias);
            }

            foreach (BiasedUnitDefinition biasedUnit in Data.BiasedUnits)
            {
                result.Add(biasedUnit);
            }

            foreach (PrefixedUnitDefinition prefixedUnit in Data.PrefixedUnits)
            {
                result.Add(prefixedUnit);
            }

            foreach (ScaledUnitDefinition scaledUnit in Data.ScaledUnits)
            {
                result.Add(scaledUnit);
            }

            return result;
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }

        private void CreateCyclicDependencyDiagnostics(IList<IDependantUnitDefinition<IDependantUnitLocations>> dependantUnits)
        {
            foreach (var dependantUnit in dependantUnits)
            {
                Diagnostic diagnostics = DiagnosticConstruction.CyclicUnitDependency(dependantUnit.Locations.DependantOn?.AsRoslynLocation(),
                    dependantUnit.Name, Data.Unit.Name);

                Diagnostics.Add(diagnostics);
            }
        }
    }
}
