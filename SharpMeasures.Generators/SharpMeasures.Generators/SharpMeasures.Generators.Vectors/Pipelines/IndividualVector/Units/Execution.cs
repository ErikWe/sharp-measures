namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

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
                Data.Unit.Type.Namespace
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
            foreach (IVectorConstant constant in Data.Constants)
            {
                AppendDocumentation(indentation, Data.Documentation.Constant(constant));
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} {constant.Name} => " +
                    $"new(({ComposeConstant(constant)}), {Data.Unit.Type.Name}.{constant.Unit.Name});");
            }

            Builder.AppendLine();

            foreach (IVectorConstant constant in Data.Constants)
            {
                if (constant.GenerateMultiplesProperty)
                {
                    AppendDocumentation(indentation, Data.Documentation.InConstantMultiples(constant));
                    Builder.AppendLine($"{indentation}public Vector{Data.Dimension} {constant.Multiples!} => new({ComposeConstantElementwiseDivision(constant)});");
                }
            }

            Builder.AppendLine();

            foreach (IUnresolvedUnitInstance includedUnit in Data.Units)
            {
                AppendDocumentation(indentation, Data.Documentation.InSpecifiedUnit(includedUnit));
                Builder.AppendLine($"{indentation}public static Vector{Data.Dimension} {includedUnit.Plural} => InUnit({Data.Unit.Type.Name}.{includedUnit.Name});");
            }
        }

        private static string ComposeConstant(IVectorConstant constant)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, components(), ", ");

            return source.ToString();

            IEnumerable<string> components()
            {
                foreach (double component in constant.Value)
                {
                    yield return component.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private string ComposeConstantElementwiseDivision(IVectorConstant constant)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? scalarComponents() : typeComponents(), ", ");

            return source.ToString();

            IEnumerable<string> scalarComponents()
            {
                for (int i = 0; i < constant.Value.Count; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Value / {constant.Value[i]}";
                }
            }

            IEnumerable<string> typeComponents()
            {
                for (int i = 0; i < constant.Value.Count; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Magnitude.Value / {constant.Value[i]}";
                }
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
