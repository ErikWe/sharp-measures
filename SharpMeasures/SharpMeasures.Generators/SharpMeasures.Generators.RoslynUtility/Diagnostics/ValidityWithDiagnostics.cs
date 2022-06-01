namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public interface IValidityWithDiagnostics : IEnumerable<Diagnostic>
{
    public delegate bool DMergeOperation(bool left, bool right);

    public enum State { Valid, Invalid }
    public enum MergeOperation { AND, OR, XOR, NOR }

    public abstract State Validity { get; }
    public abstract bool IsValid { get; }
    public abstract bool IsInvalid { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }
    public abstract IValidityWithDiagnostics AddDiagnostics(IEnumerable<Diagnostic> diagnostics);
    public abstract IValidityWithDiagnostics AddDiagnostics(params Diagnostic[] diagnostics);
    public abstract IValidityWithDiagnostics ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics);
    public abstract IValidityWithDiagnostics ReplaceDiagnostics(params Diagnostic[] diagnostics);

    public abstract IValidityWithDiagnostics Merge(DMergeOperation operation, IValidityWithDiagnostics other);
    public abstract IValidityWithDiagnostics Merge(IValidityWithDiagnostics other);
    public abstract IValidityWithDiagnostics Merge(MergeOperation operation, IValidityWithDiagnostics other);
}

public static class ValidityWithDiagnostics
{
    public static IValidityWithDiagnostics Valid => SimpleValidityWithDiagnostics.Valid;
    public static IValidityWithDiagnostics InvalidWithoutDiagnostics => SimpleValidityWithDiagnostics.InvalidWithoutDiagnostics;

    public static IValidityWithDiagnostics ValidWithDiagnostics(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleValidityWithDiagnostics(true, diagnostics);
    }

    public static IValidityWithDiagnostics ValidWithDiagnostics(params Diagnostic[] diagnostics)
    {
        return ValidWithDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    }

    public static IValidityWithDiagnostics ValidWithDiagnostics(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Valid;
        }

        return ValidWithDiagnostics(new[] { diagnostics });
    }

    public static IValidityWithDiagnostics Invalid(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleValidityWithDiagnostics(false, diagnostics);
    }

    public static IValidityWithDiagnostics Invalid(params Diagnostic[] diagnostics) => Invalid(diagnostics as IEnumerable<Diagnostic>);

    public static IValidityWithDiagnostics Invalid(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return InvalidWithoutDiagnostics;
        }

        return Invalid(new[] { diagnostics });
    }

    internal static IValidityWithDiagnostics.DMergeOperation GetMergeOperationDelegate(IValidityWithDiagnostics.MergeOperation mergeOperation)
    {
        return mergeOperation switch
        {
            IValidityWithDiagnostics.MergeOperation.AND => MergeOperations.AND,
            IValidityWithDiagnostics.MergeOperation.OR => MergeOperations.OR,
            IValidityWithDiagnostics.MergeOperation.XOR => MergeOperations.XOR,
            IValidityWithDiagnostics.MergeOperation.NOR => MergeOperations.NOR,
            _ => throw new InvalidEnumArgumentException(nameof(mergeOperation), (int)mergeOperation, typeof(IValidityWithDiagnostics.MergeOperation))
        };
    }

    internal static class MergeOperations
    {
        public static bool AND(bool left, bool right) => left & right;
        public static bool OR(bool left, bool right) => left | right;
        public static bool XOR(bool left, bool right) => left ^ right;
        public static bool NOR(bool left, bool right) => (left & right) is false;
    }

    private class SimpleValidityWithDiagnostics : IValidityWithDiagnostics
    {
        public static SimpleValidityWithDiagnostics Valid => WithoutDiagnositics(true);
        public static SimpleValidityWithDiagnostics InvalidWithoutDiagnostics => WithoutDiagnositics(false);
        public static SimpleValidityWithDiagnostics WithoutDiagnositics(bool isValid) => new(isValid, Array.Empty<Diagnostic>());

        public bool IsValid { get; }
        public bool IsInvalid => IsValid is false;
        public IValidityWithDiagnostics.State Validity => IsValid ? IValidityWithDiagnostics.State.Valid : IValidityWithDiagnostics.State.Invalid;

        public IEnumerable<Diagnostic> Diagnostics { get; }

        public SimpleValidityWithDiagnostics(bool isValid, IEnumerable<Diagnostic> diagnostics)
        {
            IsValid = isValid;
            Diagnostics = diagnostics;
        }

        public SimpleValidityWithDiagnostics Merge(IValidityWithDiagnostics.DMergeOperation mergeOperation, IValidityWithDiagnostics other)
        {
            return new(mergeOperation(IsValid, other.IsValid), Diagnostics.Concat(other.Diagnostics));
        }

        public SimpleValidityWithDiagnostics Merge(IValidityWithDiagnostics other) => Merge(MergeOperations.AND, other);

        public SimpleValidityWithDiagnostics Merge(IValidityWithDiagnostics.MergeOperation operation, IValidityWithDiagnostics other)
            => Merge(GetMergeOperationDelegate(operation), other);

        public SimpleValidityWithDiagnostics AddDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return new(IsValid, Diagnostics.Concat(diagnostics));
        }

        public SimpleValidityWithDiagnostics AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics as IEnumerable<Diagnostic>);

        public SimpleValidityWithDiagnostics ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return new(IsValid, diagnostics);
        }

        public SimpleValidityWithDiagnostics ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics as IEnumerable<Diagnostic>);

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IValidityWithDiagnostics IValidityWithDiagnostics.Merge(IValidityWithDiagnostics.DMergeOperation operation, IValidityWithDiagnostics other)
            => Merge(operation, other);
        IValidityWithDiagnostics IValidityWithDiagnostics.Merge(IValidityWithDiagnostics other) => Merge(other);
        IValidityWithDiagnostics IValidityWithDiagnostics.Merge(IValidityWithDiagnostics.MergeOperation operation, IValidityWithDiagnostics other)
            => Merge(operation, other);

        IValidityWithDiagnostics IValidityWithDiagnostics.AddDiagnostics(IEnumerable<Diagnostic> diagnostics) => AddDiagnostics(diagnostics);
        IValidityWithDiagnostics IValidityWithDiagnostics.AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics);
        IValidityWithDiagnostics IValidityWithDiagnostics.ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics) => ReplaceDiagnostics(diagnostics);
        IValidityWithDiagnostics IValidityWithDiagnostics.ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics);
    }
}
