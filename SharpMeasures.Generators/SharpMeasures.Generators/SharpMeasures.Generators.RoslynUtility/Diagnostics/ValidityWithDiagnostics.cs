namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IValidityWithDiagnostics : IEnumerable<Diagnostic>
{
    public enum ValidityState { Valid, Invalid }

    public delegate IValidityWithDiagnostics DValidity();
    public delegate IValidityWithDiagnostics DValidity<T>(T parameter);
    public delegate IValidityWithDiagnostics DValidity<T1, T2>(T1 parameter1, T2 parameter2);
    public delegate IValidityWithDiagnostics DValidity<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3);

    public delegate IOptionalWithDiagnostics<T> DOptional<T>();
    public delegate IOptionalWithDiagnostics<T> DOptional<T, T1>(T1 parameter);
    public delegate IOptionalWithDiagnostics<T> DOptional<T, T1, T2>(T1 parameter1, T2 parameter2);
    public delegate IOptionalWithDiagnostics<T> DOptional<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3);

    public delegate T DResult<T>();
    public delegate T DResult<T, T1>(T1 parameter);
    public delegate T DResult<T, T1, T2>(T1 parameter1, T2 parameter2);
    public delegate T DResult<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3);

    public abstract ValidityState Validity { get; }
    public abstract bool IsValid { get; }
    public abstract bool IsInvalid { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }

    public abstract IValidityWithDiagnostics Validate(IValidityWithDiagnostics other);
    public abstract IValidityWithDiagnostics Validate(DValidity otherDelegate);
    public abstract IValidityWithDiagnostics Validate<T>(T parameter, DValidity<T> otherDelegate);
    public abstract IValidityWithDiagnostics Validate<T1, T2>(T1 parameter1, T2 parameter2, DValidity<T1, T2> otherDelegate);
    public abstract IValidityWithDiagnostics Validate<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, DValidity<T1, T2, T3> otherDelegate);

    public abstract IOptionalWithDiagnostics<T> Merge<T>(IOptionalWithDiagnostics<T> transform);
    public abstract IOptionalWithDiagnostics<T> Merge<T>(DOptional<T> transform);
    public abstract IOptionalWithDiagnostics<T> Merge<T, T1>(T1 parameter, DOptional<T, T1> transform);
    public abstract IOptionalWithDiagnostics<T> Merge<T, T1, T2>(T1 parameter1, T2 parameter2, DOptional<T, T1, T2> transform);
    public abstract IOptionalWithDiagnostics<T> Merge<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, DOptional<T, T1, T2, T3> transform);

    public abstract IOptionalWithDiagnostics<T> Transform<T>(T result);
    public abstract IOptionalWithDiagnostics<T> Transform<T>(DResult<T> resultDelegate);
    public abstract IOptionalWithDiagnostics<T> Transform<T, T1>(T1 parameter, DResult<T, T1> resultDelegate);
    public abstract IOptionalWithDiagnostics<T> Transform<T, T1, T2>(T1 parameter1, T2 parameter2, DResult<T, T1, T2> resultDelegate);
    public abstract IOptionalWithDiagnostics<T> Transform<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, DResult<T, T1, T2, T3> resultDelegate);
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

    public static IValidityWithDiagnostics ConditionalWithoutDiagnostics(bool condition) => Conditional(condition, () => null);

    public static IValidityWithDiagnostics Conditional(bool condition, Func<Diagnostic?> invalidDiagnostics)
    {
        if (condition is false)
        {
            return Invalid(invalidDiagnostics());
        }

        return Valid;
    }

    private class SimpleValidityWithDiagnostics : IValidityWithDiagnostics
    {
        public static SimpleValidityWithDiagnostics Valid => WithoutDiagnositics(true);
        public static SimpleValidityWithDiagnostics InvalidWithoutDiagnostics => WithoutDiagnositics(false);
        public static SimpleValidityWithDiagnostics WithoutDiagnositics(bool isValid) => new(isValid, Array.Empty<Diagnostic>());

        public bool IsValid { get; }
        public bool IsInvalid => IsValid is false;
        public IValidityWithDiagnostics.ValidityState Validity => IsValid ? IValidityWithDiagnostics.ValidityState.Valid : IValidityWithDiagnostics.ValidityState.Invalid;

        public IEnumerable<Diagnostic> Diagnostics { get; }

        public SimpleValidityWithDiagnostics(bool isValid, IEnumerable<Diagnostic> diagnostics)
        {
            IsValid = isValid;
            Diagnostics = diagnostics;
        }

        public IValidityWithDiagnostics Validate(IValidityWithDiagnostics other) => new SimpleValidityWithDiagnostics(IsValid && other.IsValid, Diagnostics.Concat(other.Diagnostics));

        public IValidityWithDiagnostics Validate(IValidityWithDiagnostics.DValidity otherDelegate)
        {
            if (IsInvalid)
            {
                return this;
            }

            return Validate(otherDelegate());
        }

        public IValidityWithDiagnostics Validate<T>(T parameter, IValidityWithDiagnostics.DValidity<T> otherDelegate)
        {
            return Validate(wrappedDelegate);

            IValidityWithDiagnostics wrappedDelegate() => otherDelegate(parameter);
        }

        public IValidityWithDiagnostics Validate<T1, T2>(T1 parameter1, T2 parameter2, IValidityWithDiagnostics.DValidity<T1, T2> otherDelegate)
        {
            return Validate(wrappedDelegate);

            IValidityWithDiagnostics wrappedDelegate() => otherDelegate(parameter1, parameter2);
        }

        public IValidityWithDiagnostics Validate<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IValidityWithDiagnostics.DValidity<T1, T2, T3> otherDelegate)
        {
            return Validate(wrappedDelegate);

            IValidityWithDiagnostics wrappedDelegate() => otherDelegate(parameter1, parameter2, parameter3);
        }

        public IOptionalWithDiagnostics<T> Merge<T>(IOptionalWithDiagnostics<T> result) => Merge<T>(() => result);

        public IOptionalWithDiagnostics<T> Merge<T>(IValidityWithDiagnostics.DOptional<T> transform)
        {
            if (IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(Diagnostics);
            }

            var result = transform();

            var allDiagnostics = Diagnostics.Concat(result.Diagnostics);

            if (result.HasResult)
            {
                return OptionalWithDiagnostics.Result(result.Result, allDiagnostics);
            }

            return OptionalWithDiagnostics.Empty<T>(allDiagnostics);
        }

        public IOptionalWithDiagnostics<T> Merge<T, T1>(T1 parameter, IValidityWithDiagnostics.DOptional<T, T1> transform)
        {
            return Merge(wrappedTransform);

            IOptionalWithDiagnostics<T> wrappedTransform() => transform(parameter);
        }

        public IOptionalWithDiagnostics<T> Merge<T, T1, T2>(T1 parameter1, T2 parameter2, IValidityWithDiagnostics.DOptional<T, T1, T2> transform)
        {
            return Merge(wrappedTransform);

            IOptionalWithDiagnostics<T> wrappedTransform() => transform(parameter1, parameter2);
        }

        public IOptionalWithDiagnostics<T> Merge<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IValidityWithDiagnostics.DOptional<T, T1, T2, T3> transform)
        {
            return Merge(wrappedTransform);

            IOptionalWithDiagnostics<T> wrappedTransform() => transform(parameter1, parameter2, parameter3);
        }

        public IOptionalWithDiagnostics<T> Transform<T>(T result) => Transform(() => result);

        public IOptionalWithDiagnostics<T> Transform<T>(IValidityWithDiagnostics.DResult<T> resultDelegate)
        {
            if (IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(Diagnostics);
            }

            return OptionalWithDiagnostics.Result(resultDelegate(), Diagnostics);
        }

        public IOptionalWithDiagnostics<T> Transform<T, T1>(T1 parameter, IValidityWithDiagnostics.DResult<T, T1> resultDelegate)
        {
            return Transform(wrappedResultDelegate);

            T wrappedResultDelegate() => resultDelegate(parameter);
        }

        public IOptionalWithDiagnostics<T> Transform<T, T1, T2>(T1 parameter1, T2 parameter2, IValidityWithDiagnostics.DResult<T, T1, T2> resultDelegate)
        {
            return Transform(wrappedResultDelegate);

            T wrappedResultDelegate() => resultDelegate(parameter1, parameter2);
        }

        public IOptionalWithDiagnostics<T> Transform<T, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IValidityWithDiagnostics.DResult<T, T1, T2, T3> resultDelegate)
        {
            return Transform(wrappedResultDelegate);

            T wrappedResultDelegate() => resultDelegate(parameter1, parameter2, parameter3);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
