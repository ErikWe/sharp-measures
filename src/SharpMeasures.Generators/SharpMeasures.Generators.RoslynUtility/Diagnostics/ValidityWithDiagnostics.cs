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
    public delegate IOptionalWithDiagnostics<T> DOptional<T>();
    public delegate T DResult<T>();

    public abstract ValidityState Validity { get; }
    public abstract bool IsValid { get; }
    public abstract bool IsInvalid { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }

    public abstract IOptionalWithDiagnostics<T> AsEmptyOptional<T>();

    public abstract IValidityWithDiagnostics Validate(IValidityWithDiagnostics other);
    public abstract IValidityWithDiagnostics Validate(DValidity otherDelegate);

    public abstract IOptionalWithDiagnostics<T> Merge<T>(IOptionalWithDiagnostics<T> transform);
    public abstract IOptionalWithDiagnostics<T> Merge<T>(DOptional<T> transform);

    public abstract IOptionalWithDiagnostics<T> Transform<T>(T result);
    public abstract IOptionalWithDiagnostics<T> Transform<T>(DResult<T> resultDelegate);
}

public static class ValidityWithDiagnostics
{
    public static IValidityWithDiagnostics Valid => SimpleValidityWithDiagnostics.Valid;
    public static IValidityWithDiagnostics InvalidWithoutDiagnostics => SimpleValidityWithDiagnostics.InvalidWithoutDiagnostics;

    public static IValidityWithDiagnostics ValidWithDiagnostics(IEnumerable<Diagnostic> diagnostics) => new SimpleValidityWithDiagnostics(true, diagnostics);
    public static IValidityWithDiagnostics ValidWithDiagnostics(params Diagnostic[] diagnostics) => ValidWithDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    public static IValidityWithDiagnostics ValidWithDiagnostics(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Valid;
        }

        return ValidWithDiagnostics(new[] { diagnostics });
    }

    public static IValidityWithDiagnostics ValidWithConditionalDiagnostics(bool condition, Func<IEnumerable<Diagnostic>> diagnostics)
    {
        if (condition)
        {
            return ValidWithDiagnostics(diagnostics());
        }

        return Valid;
    }

    public static IValidityWithDiagnostics ValidWithConditionalDiagnostics(bool condition, Func<Diagnostic?> diagnostics)
    {
        if (condition)
        {
            return ValidWithDiagnostics(diagnostics());
        }

        return Valid;
    }

    public static IValidityWithDiagnostics Invalid(IEnumerable<Diagnostic> diagnostics) => new SimpleValidityWithDiagnostics(false, diagnostics);
    public static IValidityWithDiagnostics Invalid(params Diagnostic[] diagnostics) => Invalid(diagnostics as IEnumerable<Diagnostic>);
    public static IValidityWithDiagnostics Invalid(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return InvalidWithoutDiagnostics;
        }

        return Invalid(new[] { diagnostics });
    }

    public static IValidityWithDiagnostics InvalidWithConditionalDiagnostics(bool condition, Func<IEnumerable<Diagnostic>> diagnostics)
    {
        if (condition)
        {
            return Invalid(diagnostics());
        }

        return InvalidWithoutDiagnostics;
    }

    public static IValidityWithDiagnostics InvalidWithConditionalDiagnostics(bool condition, Func<Diagnostic?> diagnostics)
    {
        if (condition)
        {
            return Invalid(diagnostics());
        }

        return InvalidWithoutDiagnostics;
    }

    public static IValidityWithDiagnostics ConditionalWithoutDiagnostics(bool condition)
    {
        if (condition)
        {
            return Valid;
        }

        return InvalidWithoutDiagnostics;
    }

    public static IValidityWithDiagnostics Conditional(bool condition, Func<IEnumerable<Diagnostic>> invalidDiagnostics)
    {
        if (condition)
        {
            return Valid;
        }

        return Invalid(invalidDiagnostics());
    }

    public static IValidityWithDiagnostics Conditional(bool condition, Func<IEnumerable<Diagnostic>> validDiagnostics, Func<IEnumerable<Diagnostic>> invalidDiagnostics)
    {
        if (condition)
        {
            return ValidWithDiagnostics(validDiagnostics());
        }

        return Invalid(invalidDiagnostics());
    }

    public static IValidityWithDiagnostics Conditional(bool condition, Func<Diagnostic?> invalidDiagnostics)
    {
        if (condition)
        {
            return Valid;
        }

        return Invalid(invalidDiagnostics());
    }

    public static IValidityWithDiagnostics Conditional(bool condition, Func<Diagnostic?> validDiagnostics, Func<Diagnostic?> invalidDiagnostics)
    {
        if (condition)
        {
            return ValidWithDiagnostics(validDiagnostics());
        }

        return Invalid(invalidDiagnostics());
    }

    public static IValidityWithDiagnostics Conditional(bool condition, IEnumerable<Diagnostic> invalidDiagnostics) => Conditional(condition, () => invalidDiagnostics);
    public static IValidityWithDiagnostics Conditional(bool condition, IEnumerable<Diagnostic> validDiagnostics, IEnumerable<Diagnostic> invalidDiagnostics) => Conditional(condition, () => validDiagnostics, () => invalidDiagnostics);

    public static IValidityWithDiagnostics Conditional(bool condition, Diagnostic? invalidDiagnostics) => Conditional(condition, () => invalidDiagnostics);
    public static IValidityWithDiagnostics Conditional(bool condition, Diagnostic? validDiagnostics, Diagnostic? invalidDiagnostics) => Conditional(condition, () => validDiagnostics, () => invalidDiagnostics);

    private sealed class SimpleValidityWithDiagnostics : IValidityWithDiagnostics
    {
        public static SimpleValidityWithDiagnostics Valid => WithoutDiagnostics(true);
        public static SimpleValidityWithDiagnostics InvalidWithoutDiagnostics => WithoutDiagnostics(false);
        public static SimpleValidityWithDiagnostics WithoutDiagnostics(bool isValid) => new(isValid, Array.Empty<Diagnostic>());

        public bool IsValid { get; }
        public bool IsInvalid => IsValid is false;
        public IValidityWithDiagnostics.ValidityState Validity => IsValid ? IValidityWithDiagnostics.ValidityState.Valid : IValidityWithDiagnostics.ValidityState.Invalid;

        public IEnumerable<Diagnostic> Diagnostics { get; }

        public SimpleValidityWithDiagnostics(bool isValid, IEnumerable<Diagnostic> diagnostics)
        {
            IsValid = isValid;
            Diagnostics = diagnostics;
        }

        public IOptionalWithDiagnostics<T> AsEmptyOptional<T>() => OptionalWithDiagnostics.Empty<T>(Diagnostics);

        public IValidityWithDiagnostics Validate(IValidityWithDiagnostics other) => new SimpleValidityWithDiagnostics(IsValid && other.IsValid, Diagnostics.Concat(other.Diagnostics));
        public IValidityWithDiagnostics Validate(IValidityWithDiagnostics.DValidity otherDelegate)
        {
            if (IsInvalid)
            {
                return this;
            }

            return Validate(otherDelegate());
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

        public IOptionalWithDiagnostics<T> Transform<T>(T result) => Transform(() => result);
        public IOptionalWithDiagnostics<T> Transform<T>(IValidityWithDiagnostics.DResult<T> resultDelegate)
        {
            if (IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(Diagnostics);
            }

            return OptionalWithDiagnostics.Result(resultDelegate(), Diagnostics);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
