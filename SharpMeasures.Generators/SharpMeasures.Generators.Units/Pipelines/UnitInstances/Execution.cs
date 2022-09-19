namespace SharpMeasures.Generators.Units.Pipelines.UnitInstances;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        var source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Unit.QualifiedName}.Instances.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private sealed class Composer
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

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendFixedUnitInstances(indentation);
            AppendDerivedUnitInstances(indentation);

            AppendModifiedUnitInstances(indentation);
        }

        private void AppendFixedUnitInstances(Indentation indentation)
        {
            if (Data.FixedUnitInstance is null)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            ImplementedDefinitions.Add(Data.FixedUnitInstance.Name);

            AppendDocumentation(indentation, Data.Documentation.FixedUnitInstance(Data.FixedUnitInstance));
            Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {Data.FixedUnitInstance.Name} {{ get; }}");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($" = new(new {Data.Quantity.FullyQualifiedName}(1), new global::SharpMeasures.Scalar(0));");
            }
            else
            {
                Builder.AppendLine($" = new(new {Data.Quantity.FullyQualifiedName}(1));");
            }
        }

        private void AppendDerivedUnitInstances(Indentation indentation)
        {
            foreach (var derivedUnit in Data.DerivedUnitInstances)
            {
                SeparationHandler.AddIfNecessary();

                ImplementedDefinitions.Add(derivedUnit.Name);

                AppendDocumentation(indentation, Data.Documentation.DerivedUnitInstance(derivedUnit));
                Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {derivedUnit.Name} {{ get; }} = ");

                IterativeBuilding.AppendEnumerable(Builder, "From(", arguments(), ", ", $");{Environment.NewLine}");

                IEnumerable<string> arguments()
                {
                    var signatureIterator = getDerivation(derivedUnit).Signature.GetEnumerator();
                    var unitIterator = derivedUnit.Units.GetEnumerator();

                    while (signatureIterator.MoveNext() && unitIterator.MoveNext())
                    {
                        yield return $"{signatureIterator.Current.FullyQualifiedName}.{unitIterator.Current}";
                    }
                }
            }

            IDerivableUnit getDerivation(IDerivedUnitInstance derivedUnit)
            {
                if (derivedUnit.DerivationID is not null and { Length: > 0 })
                {
                    return Data.DerivationsByID[derivedUnit.DerivationID];
                }

                return Data.DerivationsByID.Values.Single();
            }
        }

        private void AppendModifiedUnitInstances(Indentation indentation) => AppendDependantUnits(indentation, GetDependantInstances());

        private void AppendDependantUnits(Indentation indentation, IList<IModifiedUnitInstance> dependantUnits)
        {
            var initialLength = dependantUnits.Count;

            for (int i = 0; i < dependantUnits.Count; i++)
            {
                if (ImplementedDefinitions.Contains(dependantUnits[i].OriginalUnitInstance))
                {
                    SeparationHandler.AddIfNecessary();

                    var commonDelegate = () => Builder.Append($"{indentation}public static {Data.Unit.FullyQualifiedName} {dependantUnits[i].Name} ");

                    if (dependantUnits[i] is IUnitInstanceAlias unitAlias)
                    {
                        AppendDocumentation(indentation, Data.Documentation.UnitAliasInstance(unitAlias));
                        commonDelegate();
                        AppendAlias(unitAlias);
                    }
                    else if (dependantUnits[i] is IScaledUnitInstance scaledUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.ScaledUnitInstance(scaledUnit));
                        commonDelegate();
                        AppendScaled(scaledUnit);
                    }
                    else if (dependantUnits[i] is IPrefixedUnitInstance prefixedUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.PrefixedUnitInstance(prefixedUnit));
                        commonDelegate();
                        AppendPrefixed(prefixedUnit);
                    }
                    else if (dependantUnits[i] is IBiasedUnitInstance biasedUnit)
                    {
                        AppendDocumentation(indentation, Data.Documentation.BiasedUnitInstance(biasedUnit));
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

        private void AppendAlias(IUnitInstanceAlias unitAlias)
        {
            Builder.Append($"=> {unitAlias.OriginalUnitInstance}");
        }

        private void AppendScaled(IScaledUnitInstance scaledUnit)
        {
            Builder.Append($"{{ get; }} = {scaledUnit.OriginalUnitInstance}.ScaledBy(");

            if (scaledUnit.Locations.ExplicitlySetScale)
            {
                Builder.Append($"{scaledUnit.Scale!.Value})");

                return;
            }

            Builder.Append($"{scaledUnit.Expression!})");
        }

        private void AppendPrefixed(IPrefixedUnitInstance prefixedUnit)
        {
            var prefixText = prefixedUnit.Locations.ExplicitlySetMetricPrefix
                ? $"global::SharpMeasures.MetricPrefix.{prefixedUnit.MetricPrefix!.Value}"
                : $"global::SharpMeasures.BinaryPrefix.{prefixedUnit.BinaryPrefix!.Value}";

            Builder.Append($"{{ get; }} = {prefixedUnit.OriginalUnitInstance}.WithPrefix({prefixText})");
        }

        private void AppendBiased(IBiasedUnitInstance biasedUnit)
        {
            Builder.Append($"{{ get; }} = {biasedUnit.OriginalUnitInstance}.WithBias(");

            if (biasedUnit.Locations.ExplicitlySetBias)
            {
                Builder.Append($"{biasedUnit.Bias!.Value})");

                return;
            }

            Builder.Append($"{biasedUnit.Expression!})");
        }

        private List<IModifiedUnitInstance> GetDependantInstances()
        {
            List<IModifiedUnitInstance> result = new();

            foreach (var unitAlias in Data.UnitInstanceAliases)
            {
                result.Add(unitAlias);
            }

            foreach (var biasedUnit in Data.BiasedUnitInstances)
            {
                result.Add(biasedUnit);
            }

            foreach (var prefixedUnit in Data.PrefixedUnitInstances)
            {
                result.Add(prefixedUnit);
            }

            foreach (var scaledUnit in Data.ScaledUnitInstances)
            {
                result.Add(scaledUnit);
            }

            return result;
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
