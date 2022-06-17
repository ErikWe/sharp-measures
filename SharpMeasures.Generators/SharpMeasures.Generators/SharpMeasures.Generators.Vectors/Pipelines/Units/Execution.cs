namespace SharpMeasures.Generators.Vectors.Pipelines.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_Units.g.cs", SourceText.From(source, Encoding.UTF8));
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

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            UsingsBuilding.AppendUsings(Builder, Data.Vector.Namespace, new string[]
            {
                "SharpMeasures",
                Data.Unit.UnitType.Namespace
            });

            Builder.Append(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (RefinedVectorConstantDefinition constant in Data.Constants)
            {
                AppendDocumentation(indentation, Data.Documentation.Constant(constant));
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} {constant.Name} => " +
                    $"new(({ComposeConstant(constant)}), {Data.Unit.UnitType.Name}.{constant.Unit.Name});");
            }

            Builder.AppendLine();

            foreach (RefinedVectorConstantDefinition constant in Data.Constants)
            {
                if (constant.GenerateMultiplesProperty)
                {
                    AppendDocumentation(indentation, Data.Documentation.InConstantMultiples(constant));
                    Builder.AppendLine($"{indentation}public Vector{Data.Dimension} {constant.MultiplesName!} => new({ComposeConstantElementwiseDivision(constant)});");
                }
            }

            Builder.AppendLine();

            foreach (UnitInstance includedUnit in Data.Units)
            {
                AppendDocumentation(indentation, Data.Documentation.InSpecifiedUnit(includedUnit));
                Builder.AppendLine($"{indentation}public static Vector{Data.Dimension} {includedUnit.Plural} => InUnit({Data.Unit.UnitType.Name}.{includedUnit.Name});");
            }
        }

        private static string ComposeConstant(RefinedVectorConstantDefinition definition)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, components(), ", ");

            return source.ToString();

            IEnumerable<string> components()
            {
                foreach (double component in definition.Value)
                {
                    yield return component.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private string ComposeConstantElementwiseDivision(RefinedVectorConstantDefinition definition)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? scalarComponents() : typeComponents(), ", ");

            return source.ToString();

            IEnumerable<string> scalarComponents()
            {
                for (int i = 0; i < definition.Value.Count; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Value / {definition.Value[i]}";
                }
            }

            IEnumerable<string> typeComponents()
            {
                for (int i = 0; i < definition.Value.Count; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Magnitude.Value / {definition.Value[i]}";
                }
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
