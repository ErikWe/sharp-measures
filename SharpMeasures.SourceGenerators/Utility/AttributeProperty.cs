﻿namespace ErikWe.SharpMeasures.SourceGenerators.Utility;

using System;

internal readonly record struct AttributeProperty<T>(string Name, string ParameterName, Type Type, object? DefaultValue, Func<T, object?, T> Setter)
{
    public AttributeProperty(string name, Type type, Func<T, object?, T> setter)
        : this(name, ToParameterName(name), type, null, setter) { }

    public AttributeProperty(string name, string parameterName, Type type, Func<T, object?, T> setter)
        : this(name, parameterName, type, null, setter) { }

    public AttributeProperty(string name, Type type, object defaultValue, Func<T, object?, T> setter)
        : this(name, ToParameterName(name), type, defaultValue, setter) { }

    private static string ToParameterName(string propertyName)
        => propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
}