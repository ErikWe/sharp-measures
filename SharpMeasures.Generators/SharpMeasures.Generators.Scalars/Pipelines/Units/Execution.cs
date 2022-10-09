namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        string source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Units.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendConstantBases(indentation);
            AppendUnitBases(indentation);

            AppendConstantMultiples(indentation);
            AppendUnitPlurals(indentation);
        }

        private void AppendConstantBases(Indentation indentation)
        {
            foreach (var constant in Data.Constants)
            {
                SeparationHandler.AddIfNecessary();

                AppendDocumentation(indentation, Data.Documentation.Constant(constant));
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} {constant.Name} => new({constant.Value}, {Data.Unit.FullyQualifiedName}.{constant.UnitInstanceName});");
            }
        }

        private void AppendUnitBases(Indentation indentation)
        {
            foreach (var includedBase in Data.IncludedUnitBases)
            {
                SeparationHandler.AddIfNecessary();

                AppendDocumentation(indentation, Data.Documentation.UnitBase(includedBase));
                Builder.Append($"{indentation}public static {Data.Scalar.FullyQualifiedName} {UnitBaseInstanceNameInterpreter.InterpretName(includedBase.Name)} => ");

                if (Data.UnitQuantity == Data.Scalar.AsNamedType())
                {
                    Builder.Append($"{Data.Unit.FullyQualifiedName}.{includedBase.Name}.{Data.UnitQuantity.Name}");
                }
                else
                {
                    Builder.Append($"new({Data.Unit.FullyQualifiedName}.{includedBase.Name}.{Data.UnitQuantity.Name}.Magnitude)");
                }

                Builder.AppendLine(";");
            }
        }

        private void AppendConstantMultiples(Indentation indentation)
        {
            foreach (var constant in Data.Constants)
            {
                if (constant.GenerateMultiplesProperty)
                {
                    SeparationHandler.AddIfNecessary();

                    AppendDocumentation(indentation, Data.Documentation.InConstantMultiples(constant));
                    Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {constant.Multiples!} => Magnitude.Value / {constant.Name}.Magnitude.Value;");
                }
            }
        }

        private void AppendUnitPlurals(Indentation indentation)
        {
            foreach (var includedUnit in Data.IncludedUnits)
            {
                SeparationHandler.AddIfNecessary();

                AppendDocumentation(indentation, Data.Documentation.InSpecifiedUnit(includedUnit));
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {includedUnit.PluralForm} => InUnit({Data.Unit.FullyQualifiedName}.{includedUnit.Name});");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
