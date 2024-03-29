﻿namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Globalization;
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

        context.AddSource($"{data.Value.Vector.QualifiedName}.Units.g.cs", SourceText.From(source, Encoding.UTF8));
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
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.Append(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendConstants(indentation);
            AppendConstantMultiples(indentation);
            AppendUnitPlural(indentation);
        }

        private void AppendConstants(Indentation indentation)
        {
            foreach (var constant in Data.Constants)
            {
                SeparationHandler.AddIfNecessary();

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Constant(constant));
                Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} {constant.Name} => new(({ComposeConstant(constant)}), {Data.Unit.FullyQualifiedName}.{constant.UnitInstanceName});");
            }
        }

        private static string ComposeConstant(IVectorConstant constant)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, components(), ", ");

            return source.ToString();

            IEnumerable<string> components()
            {
                if (constant.Locations.ExplicitlySetValue)
                {
                    foreach (var value in constant.Value!)
                    {
                        yield return value.ToString(CultureInfo.InvariantCulture);
                    }
                }

                if (constant.Locations.ExplicitlySetExpressions)
                {
                    foreach (var expression in constant.Expressions!)
                    {
                        yield return expression;
                    }
                }
            }
        }

        private void AppendConstantMultiples(Indentation indentation)
        {
            foreach (var constant in Data.Constants)
            {
                if (constant.GenerateMultiplesProperty)
                {
                    SeparationHandler.AddIfNecessary();

                    AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InConstantMultiples(constant));
                    Builder.AppendLine($"{indentation}public global::SharpMeasures.Vector{Data.Dimension} {constant.Multiples!} => new({ComposeConstantElementwiseDivision(constant)});");
                }
            }
        }

        private string ComposeConstantElementwiseDivision(IVectorConstant constant)
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? components(componentValue: "Value") : components(componentValue: "Magnitude.Value"), ", ");

            return source.ToString();

            IEnumerable<string> components(string componentValue)
            {
                if (constant.Locations.ExplicitlySetValue)
                {
                    for (var i = 0; i < constant.Value!.Count; i++)
                    {
                        yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.{componentValue} / {constant.Value[i]}";
                    }
                }

                if (constant.Locations.ExplicitlySetExpressions)
                {
                    for (var i = 0; i < constant.Expressions!.Count; i++)
                    {
                        yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.{componentValue} / ({constant.Expressions[i]})";
                    }
                }
            }
        }

        private void AppendUnitPlural(Indentation indentation)
        {
            foreach (var includedUnit in Data.IncludedUnits)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InSpecifiedUnit(includedUnit));
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Vector{Data.Dimension} {includedUnit.PluralForm} => InUnit({Data.Unit.FullyQualifiedName}.{includedUnit.Name});");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
