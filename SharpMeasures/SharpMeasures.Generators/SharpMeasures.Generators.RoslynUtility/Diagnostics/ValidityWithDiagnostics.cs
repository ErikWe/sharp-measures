namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public interface IValidityWithDiagnostics : IEnumerable<Diagnostic>
{
    public enum State { Valid, Invalid }
    public enum MergeOperation { AND, OR, XOR, NOR }

    public abstract State Validity { get; }
    public abstract bool IsValid { get; }
    public abstract bool IsInvalid { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }

    public abstract IValidityWithDiagnostics Merge(IValidityWithDiagnostics other);
    public abstract IValidityWithDiagnostics Merge(MergeOperation operation, IValidityWithDiagnostics other);
}

public static class ValidityWithDiagnostics
{
    public delegate IValidityWithDiagnostics DValidity();
    public delegate IValidityWithDiagnostics DValidity<T>(T argument);
    public delegate IValidityWithDiagnostics DValidity<T1, T2>(T1 argument1, T2 argument2);

    private delegate bool DMergeOperation(bool left, bool right);

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

    public static IValidityWithDiagnostics Invalid(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleValidityWithDiagnostics(false, diagnostics);
    }

    public static IValidityWithDiagnostics Invalid(params Diagnostic[] diagnostics) => Invalid(diagnostics as IEnumerable<Diagnostic>);

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation.AND, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(GetMergeOperation(mergeOperation), delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation mergeOperation, params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T argument, IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(AND, argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T argument, params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(GetMergeOperation(mergeOperation), argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 argument1, T2 argument2, IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(AND, argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 argument1, T2 argument2, params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(GetMergeOperation(mergeOperation), argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(DMergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        if (delegatedValidity is null)
        {
            throw new ArgumentNullException(nameof(delegatedValidity));
        }

        return DiagnoseAndMergeWhileValid(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(DMergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        if (delegatedValidity is null)
        {
            throw new ArgumentNullException(nameof(delegatedValidity));
        }

        return DiagnoseAndMergeWhileValid(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T1, T2> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument1, argument2);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(DMergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        if (delegatedValidity is null)
        {
            throw new ArgumentNullException(nameof(delegatedValidity));
        }

        SimpleValidityWithDiagnostics result = new(true, Array.Empty<Diagnostic>());

        foreach (DValidity delegatedDiagnosis in delegatedValidity)
        {
            result = result.Merge(mergeOperation, delegatedDiagnosis());

            if (result.IsInvalid)
            {
                return result;
            }
        }

        return result;
    }

    private static DMergeOperation GetMergeOperation(IValidityWithDiagnostics.MergeOperation mergeOperation)
    {
        return mergeOperation switch
        {
            IValidityWithDiagnostics.MergeOperation.AND => AND,
            IValidityWithDiagnostics.MergeOperation.OR => OR,
            IValidityWithDiagnostics.MergeOperation.XOR => XOR,
            IValidityWithDiagnostics.MergeOperation.NOR => NOR,
            _ => throw new InvalidEnumArgumentException(nameof(mergeOperation), (int)mergeOperation, typeof(IValidityWithDiagnostics.MergeOperation))
        };
    }

    private static bool AND(bool left, bool right) => left & right;
    private static bool OR(bool left, bool right) => left | right;
    private static bool XOR(bool left, bool right) => left ^ right;
    private static bool NOR(bool left, bool right) => (left & right) is false;

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

        public SimpleValidityWithDiagnostics Merge(DMergeOperation mergeOperation, IValidityWithDiagnostics other)
        {
            return new(mergeOperation(IsValid, other.IsValid), Diagnostics.Concat(other.Diagnostics));
        }

        public SimpleValidityWithDiagnostics Merge(IValidityWithDiagnostics other) => Merge(AND, other);

        public SimpleValidityWithDiagnostics Merge(IValidityWithDiagnostics.MergeOperation mergeOperation, IValidityWithDiagnostics other)
            => Merge(GetMergeOperation(mergeOperation), other);

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IValidityWithDiagnostics IValidityWithDiagnostics.Merge(IValidityWithDiagnostics other) => Merge(other);
        IValidityWithDiagnostics IValidityWithDiagnostics.Merge(IValidityWithDiagnostics.MergeOperation mergeOperation, IValidityWithDiagnostics other)
            => Merge(mergeOperation, other);
    }
}
