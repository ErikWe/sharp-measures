namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct DerivedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    INamedTypeSymbol?[] Signature, string[] InstanceNames)
    : IAttributeParameters
{
    private static Dictionary<string, AttributeProperty<DerivedUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivedUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<DerivedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            Signature,
            InstanceNames
        };

        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<DerivedUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<DerivedUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<DerivedUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<DerivedUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<DerivedUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> Signature { get; } = new("Signature", typeof(INamedTypeSymbol[]), WithSignature);
        public static AttributeProperty<DerivedUnitInstanceAttributeParameters> InstanceNames { get; } = new("InstanceNames", typeof(string[]), WithInstanceNames);

        private static DerivedUnitInstanceAttributeParameters WithSignature(DerivedUnitInstanceAttributeParameters parameters, object? obj)
            => obj is object[] signature ? parameters with { Signature = Array.ConvertAll(signature, static (x) => x as INamedTypeSymbol) } : parameters;

        private static DerivedUnitInstanceAttributeParameters WithInstanceNames(DerivedUnitInstanceAttributeParameters parameters, object? obj)
            => obj is object[] instanceNames ? parameters with { InstanceNames = Array.ConvertAll(instanceNames, static (x) => x as string ?? string.Empty) } : parameters;
    }

    public static DerivedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        DerivedUnitInstanceAttributeParameters values = new(
            Properties.Name.DefaultValue as string ?? string.Empty,
            Properties.Plural.DefaultValue as string ?? string.Empty,
            Properties.Symbol.DefaultValue as string ?? string.Empty,
            Properties.IsSIUnit.DefaultValue as bool? ?? false,
            Properties.IsConstant.DefaultValue as bool? ?? false,
            Properties.Signature.DefaultValue as INamedTypeSymbol?[] ?? Array.Empty<INamedTypeSymbol?>(),
            Properties.InstanceNames.DefaultValue as string[] ?? Array.Empty<string>()
        ) ;

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}