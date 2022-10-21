namespace SharpMeasures.Generators.Units.SourceBuilding.Common;

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

        context.AddSource($"{data.Value.Unit.QualifiedName}.Common.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            if (Data.BiasTerm is false)
            {
                InterfaceCollector.AddInterface($"global::System.IComparable<{Data.Unit.FullyQualifiedName}>");
            }

            if (Data.Unit.IsRecord is false)
            {
                InterfaceCollector.AddInterface($"global::System.IEquatable<{Data.Unit.FullyQualifiedName}>");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            AppendDocumentation(new Indentation(0), Data.SourceBuildingContext.Documentation.Header());
            Builder.Append(Data.Unit.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendProperties(indentation);
            AppendConstructor(indentation);
            AppendScaledBy(indentation);
            AppendWithPrefix(indentation);
            AppendWithBias(indentation);
            AppendToString(indentation);

            if (Data.Unit.IsRecord is false)
            {
                AppendEquality(indentation);
                AppendGetHashCode(indentation);
            }

            if (Data.BiasTerm is false)
            {
                AppendComparisons(indentation);
            }
        }

        private void AppendProperties(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.RepresentedQuantity());
            Builder.AppendLine($"{indentation}public {Data.Quantity.FullyQualifiedName} {Data.Quantity.Name} {{ get; }}");

            if (Data.BiasTerm)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Bias());
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Bias {{ get; }}");
            }
        }

        private void AppendConstructor(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Constructor());

            if (Data.Quantity.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"{indentation}private {Data.Unit.Name}({Data.Quantity.FullyQualifiedName} {Data.QuantityParameterName}, global::SharpMeasures.Scalar bias)");
            }
            else
            {
                Builder.AppendLine($"{indentation}private {Data.Unit.Name}({Data.Quantity.FullyQualifiedName} {Data.QuantityParameterName})");
            }

            BlockBuilding.AppendOpeningBracket(Builder, indentation);

            if (Data.Quantity.IsReferenceType)
            {
                StaticBuilding.AppendNullArgumentGuard(Builder, indentation.Increased, Data.QuantityParameterName);
            }

            Builder.AppendLine($"{indentation.Increased}{Data.Quantity.Name} = {Data.QuantityParameterName};");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"{indentation.Increased}Bias = bias;");
            }

            BlockBuilding.AppendClosingBracket(Builder, indentation, finalNewLine: true);
        }

        private void AppendScaledBy(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string expression = Data.BiasTerm ? $"{Data.Quantity.Name} * scale, Bias / scale" : $"{Data.Quantity.Name} * scale";

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScaledBy());
            Builder.AppendLine($"{indentation}public {Data.Unit.FullyQualifiedName} ScaledBy(global::SharpMeasures.Scalar scale) => new({expression});");
        }

        private void AppendWithPrefix(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string expression = Data.BiasTerm ? $"{Data.Quantity.Name} * prefix.Factor, Bias / prefix.Factor" : $"{Data.Quantity.Name} * prefix.Factor";

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.WithPrefix());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);

            Builder.AppendLine($$"""
                {{indentation}}public {{Data.Unit.FullyQualifiedName}} WithPrefix<TPrefix>(TPrefix prefix) where TPrefix : global::SharpMeasures.IPrefix
                {{indentation}}{
                {{indentation.Increased}}{{StaticBuilding.NullArgumentGuard("prefix")}}

                {{indentation.Increased}}return new({{expression}});
                {{indentation}}}
                """);
        }

        private void AppendWithBias(Indentation indentation)
        {
            if (Data.BiasTerm is false)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.WithBias());
            Builder.AppendLine($"{indentation}public {Data.Unit.FullyQualifiedName} WithBias(global::SharpMeasures.Scalar bias) => new({Data.Quantity.Name}, Bias + bias);");
        }

        private void AppendToString(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string expression = Data.BiasTerm
                ? $"\"{{{Data.Quantity.Name}}} + {{Bias}}\""
                : $"{Data.Quantity.Name}.ToString()";

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringDocumentation());
            Builder.AppendLine($"{indentation}public override string ToString() => {expression};");
        }

        private void AppendEquality(Indentation indentation)
        {
            if (Data.Unit.IsReferenceType)
            {
                AppendReferenceTypeEquality(indentation);
            }
            else
            {
                AppendValueTypeEquality(indentation);
            }
        }

        private void AppendReferenceTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string virtualText = Data.Unit.IsSealed ? string.Empty : " virtual";
            string biasEqualityText = Data.BiasTerm ? " && Bias == other.Bias" : string.Empty;

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public{virtualText} bool Equals({Data.Unit.FullyQualifiedName}? other) => other is not null && {Data.Quantity.Name} == other.{Data.Quantity.Name}{biasEqualityText};");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Unit.FullyQualifiedName);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            StaticBuilding.AppendReferenceTypeEqualityOperator(Builder, indentation, Data.Unit.FullyQualifiedName);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            StaticBuilding.AppendReferenceTypeInequalityOperator(Builder, indentation, Data.Unit.FullyQualifiedName);
        }

        private void AppendValueTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string biasEqualityText = Data.BiasTerm ? " && Bias == other.Bias" : string.Empty;

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($$"""
                {{indentation}}public bool Equals({{Data.Unit.FullyQualifiedName}} other)
                {{indentation.Increased}}=> {{Data.Quantity.Name}} == other.{{Data.Quantity.Name}}{{biasEqualityText}};
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Unit.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            StaticBuilding.AppendValueTypeEqualityOperator(Builder, indentation, Data.Unit.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            StaticBuilding.AppendValueTypeInequalityOperator(Builder, indentation, Data.Unit.Name);
        }

        private void AppendGetHashCode(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GetHashCodeDocumentation());
            Builder.Append($"{indentation}public override int GetHashCode() => ");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"({Data.Quantity.Name}, Bias).GetHashCode();");
            }
            else
            {
                Builder.AppendLine($"{Data.Quantity.Name}.GetHashCode();");
            }
        }

        private void AppendComparisons(Indentation indentation)
        {
            if (Data.Unit.IsReferenceType)
            {
                AppendReferenceTypeComparisons(indentation);
            }
            else
            {
                AppendValueTypeComparisons(indentation);
            }
        }

        private void AppendReferenceTypeComparisons(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Unit.FullyQualifiedName}? other) " +
                $"=> {Data.Quantity.Name}.Magnitude.Value.CompareTo(other?.{Data.Quantity.Name}.Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Unit.FullyQualifiedName}? x, {Data.Unit.FullyQualifiedName}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value < y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Unit.FullyQualifiedName}? x, {Data.Unit.FullyQualifiedName}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value > y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Unit.FullyQualifiedName}? x, {Data.Unit.FullyQualifiedName}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value <= y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Unit.FullyQualifiedName}? x, {Data.Unit.FullyQualifiedName}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value >= y?.{Data.Quantity.Name}.Magnitude.Value;");
        }

        private void AppendValueTypeComparisons(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Unit.FullyQualifiedName} other) " +
                $"=> {Data.Quantity.Name}.Magnitude.Value.CompareTo(other.{Data.Quantity.Name}.Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Unit.FullyQualifiedName} x, {Data.Unit.FullyQualifiedName} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value < y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Unit.FullyQualifiedName} x, {Data.Unit.FullyQualifiedName} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value > y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Unit.FullyQualifiedName} x, {Data.Unit.FullyQualifiedName} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value <= y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Unit.FullyQualifiedName} x, {Data.Unit.FullyQualifiedName} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value >= y.{Data.Quantity.Name}.Magnitude.Value;");
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
