namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface IRawQuantityOperation
{
    /// <summary>The quantity that is the result of the operation.</summary>
    public abstract ITypeSymbol Result { get; }

    /// <summary>The other quantity in the operation.</summary>
    public abstract ITypeSymbol Other { get; }

    /// <summary>The operator that is applied to the quantities.</summary>
    public abstract OperatorType OperatorType { get; }

    /// <summary>The position of the implementing quantity in the operation.</summary>
    public abstract OperationPosition? Position { get; }

    /// <summary>Determines whether the mirrored operation is also implemented.</summary>
    public abstract OperationMirrorMode? MirrorMode { get; }

    /// <summary>Dictates how the operation is implemented.</summary>
    public abstract OperationImplementation? Implementation { get; }

    /// <summary>Dictates how the mirrored operation is implemented, if it is implemented.</summary>
    public abstract OperationImplementation? MirroredImplementation { get; }

    /// <summary>The name of the instance method, if implemented.</summary>
    public abstract string? MethodName { get; }

    /// <summary>The name of the static method, if implemented.</summary>
    public abstract string? StaticMethodName { get; }

    /// <summary>The name of the mirrored instance method, if implemented.</summary>
    public abstract string? MirroredMethodName { get; }

    /// <summary>The name of the mirrored static method, if implemented.</summary>
    public abstract string? MirroredStaticMethodName { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    public abstract IQuantityOperationSyntax? Syntax { get; }
}
