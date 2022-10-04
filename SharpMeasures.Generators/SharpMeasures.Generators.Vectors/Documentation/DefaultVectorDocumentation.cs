﻿namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal sealed class DefaultVectorDocumentation : IVectorDocumentationStrategy, IEquatable<DefaultVectorDocumentation>
{
    private DefinedType Type { get; }
    private int Dimension { get; }
    private NamedType Unit { get; }

    private NamedType? Scalar { get; }

    private IUnitInstance? DefaultUnitInstance { get; }
    private string? DefaultUnitInstanceSymbol { get; }

    private string UnitParameterName { get; }

    private IUnitInstance? ExampleScalarUnitBaseInstance { get; }
    private IUnitInstance? ExampleUnitInstance { get; }

    private bool HasComponent => Scalar is not null;

    private SpecificTexts Texts { get; }

    public DefaultVectorDocumentation(VectorDataModel model)
    {
        Type = model.Vector.Type;
        Dimension = model.Vector.Dimension;
        Unit = model.Vector.Unit;

        DefaultUnitInstance = GetDefaultUnitInstance(model);
        DefaultUnitInstanceSymbol = model.Vector.DefaultUnitInstanceSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Name);

        ExampleScalarUnitBaseInstance = GetExampleScalarBase(model);
        ExampleUnitInstance = GetExampleUnit(model);

        Texts = new(Dimension, VectorReference, UnitParameterName);
    }

    private static IUnitInstance? GetDefaultUnitInstance(VectorDataModel model)
    {
        if (model.Vector.DefaultUnitInstanceName is not null && model.UnitPopulation.Units.TryGetValue(model.Vector.Unit, out var unit) && unit.UnitInstancesByName.TryGetValue(model.Vector.DefaultUnitInstanceName, out var defaultUnitInstance))
        {
            return defaultUnitInstance;
        }

        return null;
    }

    private static IUnitInstance? GetExampleScalarBase(VectorDataModel model)
    {
        if (model.Vector.Scalar is not null && model.ScalarPopulation.Scalars.TryGetValue(model.Vector.Scalar.Value, out var scalar) && model.UnitPopulation.Units.TryGetValue(scalar.Unit, out var scalarUnit))
        {
            foreach (var includedBaseName in scalar.IncludedUnitBaseInstanceNames)
            {
                if (scalarUnit.UnitInstancesByName.TryGetValue(includedBaseName, out var includedBase))
                {
                    return includedBase;
                }
            }
        }

        return null;
    }

    private static IUnitInstance? GetExampleUnit(VectorDataModel model)
    {
        if (model.UnitPopulation.Units.TryGetValue(model.Vector.Unit, out var unit) is false)
        {
            return null;
        }

        foreach (var includedUnitName in model.Vector.IncludedUnitInstanceNames)
        {
            if (unit.UnitInstancesByName.TryGetValue(includedUnitName, out var includedUnit))
            {
                return includedUnit;
            }
        }

        return null;
    }

    public string Header() => HasComponent switch
    {
        true => ComponentedHeader(),
        false => UncomponentedHeader()
    };

    private string ComponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {Type.Name}, composed of {ScalarReference} and expressed in {UnitReference}.</summary>
        """;

    private string UncomponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {Type.Name}, expressed in {UnitReference}.</summary>
        """;

    public string Zero() => $$"""/// <summary>The {{VectorReference}} representing { {{ConstantVectorTexts.Zeros(Dimension)}} }.</summary>""";
    public string Constant(IVectorConstant constant)
    {
        StringBuilder componentText = new();
        IterativeBuilding.AppendEnumerable(componentText, components(), ", ");

        return $$"""/// <summary>The {{ScalarReference}} representing { ({{componentText}}) <see cref="{{Unit.FullyQualifiedName}}.{{constant.UnitInstanceName}}"/> }.</summary>""";

        IEnumerable<string> components()
        {
            foreach (double value in constant.Value)
            {
                yield return value switch
                {
                    > 10000 or < 0.0001 and > -0.0001 or < -10000 => value.ToString("0.000E0", CultureInfo.InvariantCulture),
                    _ => value.ToString("0.####", CultureInfo.InvariantCulture)
                };
            }
        }
    }

    public string WithScalarComponents() => InheritDoc;
    public string WithVectorComponents() => InheritDoc;

    public string ComponentsConstructor() => $$"""
        /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }.</summary>
        {{Texts.ComponentsConstructor()}}
        """;

    public string ScalarsConstructor() => $$"""
        /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }, expressed in an arbitrary unit.</summary>
        {{Texts.ScalarsConstructor()}}
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}({{ConstantVectorTexts.UnnamedScalars(Dimension)}}, {{Unit.FullyQualifiedName}})"/>,
        /// where the components are expressed in a specified {{UnitReference}}.</remarks>
        """;

    public string VectorConstructor() => $$"""
        /// <summary>Constructs a new {{VectorReference}} representing { <paramref name="components"/> }, expressed in an arbitrary unit.</summary>
        /// <param name="components">The magnitudes of the components of the constructed {{VectorReference}}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}(global::SharpMeasures.Vector{{Dimension}}, {{Unit.FullyQualifiedName}})"/>,
        /// where the components are expressed in a specified {{UnitReference}}.</remarks>
        """;

    public string ScalarsAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }, when expressed in <paramref name="{{UnitParameterName}}"/>.</summary>
            {{Texts.ScalarsAndUnitConstructor()}}
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which the magnitudes of the components are expressed.</param>
            """;

        if (Scalar is not null && ExampleScalarUnitBaseInstance is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {VectorReference} may also be constructed as demonstrated below.
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.Value.FullyQualifiedName}.One{ExampleScalarUnitBaseInstance.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }
    public string VectorAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{VectorReference}} representing { <paramref name="components"/> }, when expressed in <paramref name="{{UnitParameterName}}"/>.</summary>
            /// <param name="components">The magnitudes of the components of the constructed {{VectorReference}}, expressed in <paramref name="{{UnitParameterName}}"/>.</param>
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which <paramref name="components"/> is expressed.</param>
            """;

        if (Scalar is not null && ExampleScalarUnitBaseInstance is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {VectorReference} may also be constructed as demonstrated below.
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.Value.FullyQualifiedName}.One{ExampleScalarUnitBaseInstance.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string CastFromComponents() => $"""
        /// <summary>Constructs the {VectorReference} with the elements of <paramref name="components"/> as components.</summary>
        """;

    public string Component(int componentIndex) => $"""
        /// <summary>The {VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}-component of <see langword="this"/>.</summary>
        """;

    public string ComponentMagnitude(int componentIndex) => $"""
        /// <summary>The magnitude of the {VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}-component of <see langword="this"/>.</summary>
        """;

    public string Components()
    {
        var commonText = $"""
            /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in an arbitrary unit.</summary>
            /// <remarks>In most cases, expressing the magnitudes in a specified {UnitReference} should be preferred. This is achieved through
            /// <see cref="InUnit({Unit.FullyQualifiedName})"/>
            """;

        if (ExampleUnitInstance is not null)
        {
            return $"""{commonText} or an associated property - such as <see cref="{ExampleUnitInstance.PluralForm}"/>.</remarks>""";
        }

        return $"{commonText}.</remarks>";
    }
    
    public string InUnit() => $"""
        /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
        /// <param name="{UnitParameterName}">The {UnitReference} in which the components of <see langword="this"/> are expressed.</param>
        """;
    public string InConstantMultiples(IVectorConstant constant) => $"""
        /// <summary>The components of <see langword="this"/>, expressed in multiples of <see cref="{VectorReference}.{constant.Name}"/>.</summary>
        """;
    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"""
        /// <summary>The components of <see langword="this"/>, expressed in <see cref="{Unit.FullyQualifiedName}.{unitInstance.Name}"/>.</summary>
        """;

    public string Conversion(NamedType vector) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{vector.Name}"/>.</summary>
        """;

    public string AntidirectionalConversion(NamedType vector) => $"""
        /// <summary>Converts <paramref name="{SourceBuildingUtility.ToParameterName(vector.Name)}"/> to the equivalent {VectorReference}.</summary>
        /// <param name="a">This <see cref="{vector.FullyQualifiedName}"/> is converted to the equivalent {VectorReference}.</param>
        """;
    public string CastConversion(NamedType vector) => $"""
        /// <summary>Converts <paramref name="a"/> to the equivalent <see cref="{vector.Name}"/>.</summary>
        /// <param name="a">This {VectorReference} is converted to the equivalent <see cref="{vector.Name}"/>.</param>
        """;

    public string AntidirectionalCastConversion(NamedType vector) => $"""
        /// <summary>Converts <paramref name="a"/> to the equivalent {VectorReference}.</summary>
        /// <param name="a">This <see cref="{vector.FullyQualifiedName}"/> is converted to the equivalent {VectorReference}.</param>
        """;

    public string OperationMethod(IQuantityOperation operation, NamedType other) => OperationMethod(operation, mirrored: false);
    public string MirroredOperationMethod(IQuantityOperation operation, NamedType other) => OperationMethod(operation, mirrored: true);
    private static string OperationMethod(IQuantityOperation operation, bool mirrored)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(operation.Other.Name);

        if (operation.OperatorType is OperatorType.Addition)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> + <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second term of { <see langword="this"/> + <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Multiplication)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second factor of { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Subtraction)
        {
            if (operation.Position is OperatorPosition.Left && mirrored is false || operation.Position is OperatorPosition.Right && mirrored)
            {
                return $$"""
                    /// <summary>Computes { <see langword="this"/> - <paramref name="{{parameterName}}"/> }.</summary>
                    /// <param name="{{parameterName}}">The subtrahend of { <see langword="this"/> - <paramref name="{{parameterName}}"/> }.</param>
                    """;
            }

            return $$"""
                /// <summary>Computes { <paramref name="{{parameterName}}"/> - <see langword="this"/> }.</summary>
                /// <param name="{{parameterName}}">The minuend of { <paramref name="{{parameterName}}"/> - <see langword="this"/> }.</param>
                """;
        }

        if (operation.Position is OperatorPosition.Left && mirrored is false || operation.Position is OperatorPosition.Right && mirrored)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> / <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The divisor of { <see langword="this"/> / <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        return $$"""
            /// <summary>Computes { <paramref name="{{parameterName}}"/> / <see langword="this"/> }.</summary>
            /// <param name="{{parameterName}}">The dividend of { <paramref name="{{parameterName}}"/> / <see langword="this"/> }.</param>
            """;
    }

    public string VectorOperationMethod(IVectorOperation operation, NamedType other) => VectorOperationMethod(operation, mirrored: false);
    public string MirroredVectorOperationMethod(IVectorOperation operation, NamedType other) => VectorOperationMethod(operation, mirrored: true);
    private static string VectorOperationMethod(IVectorOperation operation, bool mirrored)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(operation.Other.Name);

        if (operation.OperatorType is VectorOperatorType.Dot)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second factor of { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        if (operation.Position is OperatorPosition.Left && mirrored is false || operation.Position is OperatorPosition.Right && mirrored)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> ⨯ <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second factor of { <see langword="this"/> ⨯ <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        return $$"""
                /// <summary>Computes { <paramref name="{{parameterName}}"/> ⨯ <see langword="this"/> }.</summary>
                /// <param name="{{parameterName}}">The first factor of { <paramref name="{{parameterName}}"/> ⨯ <see langword="this"/> }.</param>
                """;
    }

    public string MirroredOperationOperator(IQuantityOperation operation, NamedType other) => OperationOperator(operation, other);
    public string OperationOperator(IQuantityOperation operation, NamedType other)
    {
        if (operation.OperatorType is OperatorType.Addition)
        {
            return """
                /// <summary>Computes { <see langword="a"/> + <paramref name="b"/> }.</summary>
                /// <param name="a">The first term of { <see langword="a"/> + <paramref name="b"/> }.</param>
                /// <param name="b">The second term of { <see langword="a"/> + <paramref name="b"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Multiplication)
        {
            return """
                /// <summary>Computes { <see langword="a"/> * <paramref name="b"/> }.</summary>
                /// <param name="a">The first factor of { <see langword="a"/> * <paramref name="b"/> }.</param>
                /// <param name="b">The second factor of { <see langword="a"/> * <paramref name="b"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Subtraction)
        {
            return """
                /// <summary>Computes { <see langword="a"/> - <paramref name="b"/> }.</summary>
                /// <param name="a">The minuend of { <see langword="a"/> - <paramref name="b"/> }.</param>
                /// <param name="b">The subtrahend of { <see langword="a"/> - <paramref name="b"/> }.</param>
                """;
        }

        return """
            /// <summary>Computes { <see langword="a"/> / <paramref name="b"/> }.</summary>
            /// <param name="a">The dividend of { <see langword="a"/> / <paramref name="b"/> }.</param>
            /// <param name="b">The divisor of { <see langword="a"/> / <paramref name="b"/> }.</param>
            """;
    }

    public string Process(IQuantityProcess process) => $"""/// <summary>Executes a custom process.</summary>""";

    public string IsNaN() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsNaN"/>""";
    public string IsZero() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsZero"/>""";
    public string IsFinite() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsFinite"/>""";
    public string IsInfinite() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsInfinite"/>""";

    public string Magnitude() => $$"""/// <inheritdoc cref="global::SharpMeasures.IVector{{Dimension}}.Magnitude()"/>""";

    public string ScalarMagnitude() => InheritDoc;
    public string ScalarSquaredMagnitude() => InheritDoc;

    public string Normalize() => InheritDoc;
    public string Transform() => InheritDoc;

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the""";

        if (DefaultUnitInstance is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""{commonText} components expressed in <see cref="{Unit.FullyQualifiedName}.{DefaultUnitInstance.Name}"/>, followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstance is not null)
        {
            return $"""{commonText} components expressed in <see cref="{Unit.FullyQualifiedName}.{DefaultUnitInstance.Name}"/>.</summary>""";
        }

        return $"""{commonText} components expressed in an arbitrary unit.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator() => $"""
            /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent components.</summary>
            /// <param name="lhs">The left-hand side of the equality check.</param>
            /// <param name="rhs">The right-hand side of the equality check.</param>
            """;

    public string InequalitySameTypeOperator() => $"""
            /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent components.</summary>
            /// <param name="lhs">The left-hand side of the inequality check.</param>
            /// <param name="rhs">The right-hand side of the inequality check.</param>
            """;

    public string GetHashCodeDocumentation() => InheritDoc;

    public string Deconstruct() => $"""
        /// <summary>Deconstructs <see langword="this"/> into the individual components.</summary>
        {Texts.Deconstruct()}
        """;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.Add(global::SharpMeasures.Vector{{Dimension}})"/>""";
    public string SubtractSameTypeMethod() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.Subtract(global::SharpMeasures.Vector{{Dimension}})"/>""";

    public string AddDifferenceMethod() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.Add(global::SharpMeasures.Vector{{Dimension}})"/>""";
    public string SubtractDifferenceMethod() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.Subtract(global::SharpMeasures.Vector{{Dimension}})"/>""";

    public string MultiplyScalarMethod() => InheritDoc;
    public string DivideScalarMethod() => InheritDoc;

    public string UnaryPlusOperator() => InheritDoc;
    public string NegateOperator() => InheritDoc;

    public string AddSameTypeOperator() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.operator +(global::SharpMeasures.Vector{{Dimension}}, global::SharpMeasures.Vector{{Dimension}})"/>""";
    public string SubtractSameTypeOperator() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.operator -(global::SharpMeasures.Vector{{Dimension}}, global::SharpMeasures.Vector{{Dimension}})"/>""";

    public string AddDifferenceOperatorLHS() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.operator +(global::SharpMeasures.Vector{{Dimension}}, global::SharpMeasures.Vector{{Dimension}})"/>""";
    public string AddDifferenceOperatorRHS() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.operator +(global::SharpMeasures.Vector{{Dimension}}, global::SharpMeasures.Vector{{Dimension}})"/>""";
    public string SubtractDifferenceOperatorLHS() => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{Dimension}}.operator -(global::SharpMeasures.Vector{{Dimension}}, global::SharpMeasures.Vector{{Dimension}})"/>""";

    public string MultiplyScalarOperatorLHS() => InheritDoc;
    public string MultiplyScalarOperatorRHS() => InheritDoc;
    public string DivideScalarOperatorLHS() => InheritDoc;

    private string VectorReference => $"""<see cref="{Type.FullyQualifiedName}"/>""";
    private string UnitReference => $"""<see cref="{Unit.FullyQualifiedName}"/>""";
    private string ScalarReference => $"""<see cref="{Scalar?.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    private sealed class SpecificTexts
    {
        public string ParameterTuple() => ParameterTupleBuilder.GetText(Dimension);
        public string ComponentsConstructor() => ComponentsConstructorBuilder.GetText(Dimension);
        public string ScalarsConstructor() => ScalarsConstructorBuilder.GetText(Dimension);
        public string ScalarsAndUnitConstructor() => ScalarsAndUnitConstructorBuilder.GetText(Dimension);
        public string Deconstruct() => DeconstructBuilder.GetText(Dimension);

        private int Dimension { get; }

        private VectorTextBuilder ParameterTupleBuilder { get; }
        private VectorTextBuilder ComponentsConstructorBuilder { get; }
        private VectorTextBuilder ScalarsConstructorBuilder { get; }
        private VectorTextBuilder ScalarsAndUnitConstructorBuilder { get; }
        private static VectorTextBuilder DeconstructBuilder { get; } = GetDeconstructBuilder();

        public SpecificTexts(int dimension, string vectorReference, string unitParameterName)
        {
            Dimension = dimension;

            ParameterTupleBuilder = GetParameterTupleBuilder();
            ComponentsConstructorBuilder = GetComponentsConstructorBuilder(vectorReference);
            ScalarsConstructorBuilder = GetScalarsConstructorBuilder(vectorReference);
            ScalarsAndUnitConstructorBuilder = GetScalarsAndUnitConstructorBuilder(vectorReference, unitParameterName);
        }

        private static VectorTextBuilder GetParameterTupleBuilder()
        {
            return new VectorTextBuilder(parameterTupleComponent, ", ");

            static string parameterTupleComponent(int componentIndex, int dimension)
            {
                string componentName = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);

                return $"""<paramref name="{componentName}"/>""";
            }
        }

        private static VectorTextBuilder GetComponentsConstructorBuilder(string vectorReference)
        {
            return new VectorTextBuilder(componentsConstructorComponent, Environment.NewLine);

            string componentsConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The {componentUpperCase}-component of the constructed {vectorReference}.</param>""";
            }
        }

        private static VectorTextBuilder GetScalarsConstructorBuilder(string vectorReference)
        {
            return new VectorTextBuilder(scalarsConstructorComponent, Environment.NewLine);

            string scalarsConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The magnitude of the {componentUpperCase}-component of the constructed {vectorReference}, expressed in an arbitrary unit.</param>""";
            }
        }

        private static VectorTextBuilder GetScalarsAndUnitConstructorBuilder(string vectorReference, string unitParameterName)
        {
            return new VectorTextBuilder(scalarsAndUnitConstructorComponent, Environment.NewLine);

            string scalarsAndUnitConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The magnitude of the {componentUpperCase}-component of the constructed {vectorReference}, expressed in <paramref name="{unitParameterName}"/>.</param>""";
            }
        }

        private static VectorTextBuilder GetDeconstructBuilder()
        {
            return new VectorTextBuilder(deconstructComponent, Environment.NewLine);

            static string deconstructComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The {componentUpperCase}-component of <see langword="this"/>.</param>""";
            }
        }
    }

    public bool Equals(DefaultVectorDocumentation? other) => other is not null && Type == other.Type && Dimension == other.Dimension && Unit == other.Unit && Scalar == other.Scalar && DefaultUnitInstance == other.DefaultUnitInstance && DefaultUnitInstanceSymbol == other.DefaultUnitInstanceSymbol;
    public override bool Equals(object? obj) => obj is DefaultVectorDocumentation other && Equals(other);
    
    public static bool operator ==(DefaultVectorDocumentation? lhs, DefaultVectorDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultVectorDocumentation? lhs, DefaultVectorDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Dimension, Unit, Scalar, DefaultUnitInstance, DefaultUnitInstanceSymbol).GetHashCode();
}
