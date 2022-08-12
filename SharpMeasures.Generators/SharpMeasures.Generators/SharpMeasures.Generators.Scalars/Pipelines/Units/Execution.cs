namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Units.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (var constant in Data.Constants)
            {
                AppendDocumentation(indentation, Data.Documentation.Constant(constant));
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} {constant.Name} => new({constant.Value}, {Data.Unit.FullyQualifiedName}.{constant.Unit.Name});");
            }

            if (Data.Constants.Count > 0)
            {
                Builder.AppendLine();
            }

            foreach (var includedBase in Data.IncludedBases)
            {
                AppendDocumentation(indentation, Data.Documentation.UnitBase(includedBase));
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} One{includedBase.Name} => {Data.Unit.FullyQualifiedName}.{includedBase.Name}.{Data.UnitQuantity.Name};");
            }

            if (Data.IncludedBases.Count > 0)
            {
                Builder.AppendLine();
            }

            bool anyAccessedConstant = false;

            foreach (var constant in Data.Constants)
            {
                if (constant.GenerateMultiplesProperty)
                {
                    anyAccessedConstant = true;

                    AppendDocumentation(indentation, Data.Documentation.InConstantMultiples(constant));
                    Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {constant.Multiples!} => Magnitude.Value / {constant.Name}.Magnitude.Value;");
                }
            }

            if (anyAccessedConstant)
            {
                Builder.AppendLine();
            }

            foreach (var includedUnit in Data.IncluedUnits)
            {
                AppendDocumentation(indentation, Data.Documentation.InSpecifiedUnit(includedUnit));
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {includedUnit.Plural} => InUnit({Data.Unit.FullyQualifiedName}.{includedUnit.Name});");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
