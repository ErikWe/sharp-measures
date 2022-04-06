namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using ErikWe.SharpMeasures.SourceGenerators.Utility;

using System;

internal static class AttributeParameters
{
    public static AttributeProperty<T> Name<T>(Func<T, string, T> setter) => new("Name", typeof(string), WithSetter(setter, WithName));
    public static AttributeProperty<T> Plural<T>(Func<T, string, T> setter) => new("Plural", typeof(string), WithSetter(setter, WithPlural));
    public static AttributeProperty<T> Symbol<T>(Func<T, string, T> setter) => new("Symbol", typeof(string), WithSetter(setter, WithSymbol));
    public static AttributeProperty<T> IsSIUnit<T>(Func<T, bool, T> setter) => new("IsSIUnit", typeof(bool), WithSetter(setter, WithIsSIUnit));
    public static AttributeProperty<T> IsConstant<T>(Func<T, bool, T> setter) => new("IsConstant", typeof(bool), WithSetter(setter, WithIsConstant));

    private static Func<T1, object?, T1> WithSetter<T1, T2>(Func<T1, T2, T1> setter, Func<Func<T1, T2, T1>, T1, object?, T1> parser)
        => (x, y) => parser(setter, x, y);

    private static T WithName<T>(Func<T, string, T> setter, T parameters, object? obj)
        => obj is string name ? setter(parameters, name) : parameters;

    private static T WithPlural<T>(Func<T, string, T> setter, T parameters, object? obj)
        => obj is string plural ? setter(parameters, plural) : parameters;

    private static T WithSymbol<T>(Func<T, string, T> setter, T parameters, object? obj)
        => obj is string symbol ? setter(parameters, symbol) : parameters;

    private static T WithIsSIUnit<T>(Func<T, bool, T> setter, T parameters, object? obj)
        => obj is bool isSIUnit ? setter(parameters, isSIUnit) : parameters;

    private static T WithIsConstant<T>(Func<T, bool, T> setter, T parameters, object? obj)
        => obj is bool isConstant ? setter(parameters, isConstant) : parameters;
}
