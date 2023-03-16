namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T? NullableValueResult<T>(this IOptionalWithDiagnostics<T?> optional) where T : struct
    {
        if (optional.LacksResult)
        {
            return null;
        }

        return optional.Result;
    }

    public static T? NullableValueResult<T>(this IOptionalWithDiagnostics<T> optional) where T : struct
    {
        if (optional.LacksResult)
        {
            return null;
        }

        return optional.Result;
    }

    public static T? NullableReferenceResult<T>(this IOptionalWithDiagnostics<T> optional) where T : class?
    {
        if (optional.LacksResult)
        {
            return default;
        }

        return optional.Result;
    }
}

public interface IOptionalWithDiagnostics : IEnumerable<Diagnostic>
{
    public abstract bool HasResult { get; }
    public abstract bool LacksResult { get; }

    public abstract IValidityWithDiagnostics Reduce();

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }

    public abstract IOptionalWithDiagnostics<TNew> AsEmptyOptional<TNew>();
}

public interface IOptionalWithDiagnostics<T> : IOptionalWithDiagnostics
{
    public delegate IValidityWithDiagnostics DValidator(T result);
    public delegate IOptionalWithDiagnostics<TNew> DTransform<TNew>(T result);
    public delegate TNew DResult<TNew>(T result);

    public abstract T Result { get; }

    public abstract IOptionalWithDiagnostics<T> AddDiagnostics(params IEnumerable<Diagnostic>[] diagnostics);

    public abstract IOptionalWithDiagnostics<T> Validate(DValidator validator);
    public abstract IValidityWithDiagnostics Reduce(DValidator validator);
    public abstract IOptionalWithDiagnostics<TNew> Merge<TNew>(DTransform<TNew> transform);
    public abstract IOptionalWithDiagnostics<TNew> Transform<TNew>(DResult<TNew> transform);
}

public static class OptionalWithDiagnostics
{
    public static IOptionalWithDiagnostics<T> EmptyWithoutDiagnostics<T>() => Empty<T>(Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Result<T>(T result, IEnumerable<Diagnostic> diagnostics) => new SimpleOptionalWithDiagnostics<T>(result, diagnostics);
    public static IOptionalWithDiagnostics<T> Result<T>(T result, params Diagnostic[] diagnostics) => Result(result, diagnostics as IEnumerable<Diagnostic>);
    public static IOptionalWithDiagnostics<T> Result<T>(T result) => Result(result, Array.Empty<Diagnostic>());
    public static IOptionalWithDiagnostics<T> Result<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Result(result);
        }

        return Result(result, new[] { diagnostics });
    }

    public static IOptionalWithDiagnostics<T> Empty<T>(IEnumerable<Diagnostic> diagnostics) => new SimpleOptionalWithDiagnostics<T>(diagnostics);
    public static IOptionalWithDiagnostics<T> Empty<T>(params Diagnostic[] diagnostics) => Empty<T>(diagnostics as IEnumerable<Diagnostic>);
    public static IOptionalWithDiagnostics<T> Empty<T>(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return EmptyWithoutDiagnostics<T>();
        }

        return Empty<T>(new[] { diagnostics });
    }

    public static IOptionalWithDiagnostics<T> ConditionalWithoutDiagnostics<T>(bool condition, T result) => ConditionalWithoutDiagnostics(condition, () => result);
    public static IOptionalWithDiagnostics<T> ConditionalWithoutDiagnostics<T>(bool condition, Func<T> resultDelegate)
    {
        if (condition)
        {
            return Result(resultDelegate());
        }

        return EmptyWithoutDiagnostics<T>();
    }

    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, T result, Func<IEnumerable<Diagnostic>> invalidDiagnosticsDelegate) => Conditional(condition, () => result, invalidDiagnosticsDelegate);
    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, Func<T> resultDelegate, Func<IEnumerable<Diagnostic>> invalidDiagnosticsDelegate)
    {
        if (condition)
        {
            return Result(resultDelegate());
        }

        return Empty<T>(invalidDiagnosticsDelegate());
    }

    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, T result, Func<Diagnostic?> invalidDiagnostics) => Conditional(condition, () => result, invalidDiagnostics);
    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, Func<T> resultDelegate, Func<Diagnostic?> invalidDiagnostics)
    {
        if (condition)
        {
            return Result(resultDelegate());
        }

        return Empty<T>(invalidDiagnostics());
    }

    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, T result, IEnumerable<Diagnostic> invalidDiagnostics) => Conditional(condition, result, () => invalidDiagnostics);
    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, Func<T> resultDelegate, IEnumerable<Diagnostic> invalidDiagnostics) => Conditional(condition, resultDelegate, () => invalidDiagnostics);

    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, T result, Diagnostic? invalidDiagnostics) => Conditional(condition, result, () => invalidDiagnostics);
    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, Func<T> resultDelegate, Diagnostic? invalidDiagnostics) => Conditional(condition, resultDelegate, () => invalidDiagnostics);

    public static IOptionalWithDiagnostics<T> ConditionalWithDefiniteDiagnostics<T>(bool condition, T result, params Diagnostic[] diagnostics) => ConditionalWithDefiniteDiagnostics(condition, () => result, diagnostics);
    public static IOptionalWithDiagnostics<T> ConditionalWithDefiniteDiagnostics<T>(bool condition, Func<T> resultDelegate, params Diagnostic[] diagnostics) => ConditionalWithDefiniteDiagnostics(condition, resultDelegate, diagnostics as IEnumerable<Diagnostic>);

    public static IOptionalWithDiagnostics<T> ConditionalWithDefiniteDiagnostics<T>(bool condition, T result, IEnumerable<Diagnostic> diagnostics) => ConditionalWithDefiniteDiagnostics(condition, () => result, diagnostics);
    public static IOptionalWithDiagnostics<T> ConditionalWithDefiniteDiagnostics<T>(bool condition, Func<T> resultDelegate, IEnumerable<Diagnostic> diagnostics)
    {
        if (condition)
        {
            return Result(resultDelegate(), diagnostics);
        }

        return Empty<T>(diagnostics);
    }

    private sealed class SimpleOptionalWithDiagnostics<T> : IOptionalWithDiagnostics<T>
    {
        private Optional<T> Optional { get; }

        public IEnumerable<Diagnostic> Diagnostics { get; }
        public T Result
        {
            get
            {
                if (LacksResult)
                {
                    throw new OptionalLacksResultException();
                }

                return Optional.Value;
            }
        }

        public bool HasResult => Optional.HasValue;
        public bool LacksResult => HasResult is false;

        public SimpleOptionalWithDiagnostics(IEnumerable<Diagnostic> diagnostics) : this(new Optional<T>(), diagnostics) { }

        public SimpleOptionalWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics) : this((Optional<T>)result, diagnostics) { }

        private SimpleOptionalWithDiagnostics(Optional<T> optional, IEnumerable<Diagnostic> diagnostics)
        {
            Optional = optional;
            Diagnostics = diagnostics;
        }

        public IOptionalWithDiagnostics<TNew> AsEmptyOptional<TNew>() => new SimpleOptionalWithDiagnostics<TNew>(Diagnostics);

        public IOptionalWithDiagnostics<T> AddDiagnostics(params IEnumerable<Diagnostic>[] diagnostics) => new SimpleOptionalWithDiagnostics<T>(Optional, Diagnostics.Concat(diagnostics.SelectMany(static (diagnostics) => diagnostics)));

        public IOptionalWithDiagnostics<T> Validate(IOptionalWithDiagnostics<T>.DValidator validator)
        {
            if (LacksResult)
            {
                return this;
            }

            var validity = validator(Result);
            var allDiagnostics = Diagnostics.Concat(validity.Diagnostics);

            if (validity.IsInvalid)
            {
                return new SimpleOptionalWithDiagnostics<T>(allDiagnostics);
            }

            return new SimpleOptionalWithDiagnostics<T>(Result, allDiagnostics);
        }

        public IValidityWithDiagnostics Reduce(IOptionalWithDiagnostics<T>.DValidator validator) => Validate(validator).Reduce();
        public IValidityWithDiagnostics Reduce()
        {
            if (HasResult)
            {
                return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics);
            }

            return ValidityWithDiagnostics.Invalid(Diagnostics);
        }

        public IOptionalWithDiagnostics<TNew> Merge<TNew>(IOptionalWithDiagnostics<T>.DTransform<TNew> transform)
        {
            if (LacksResult)
            {
                return new SimpleOptionalWithDiagnostics<TNew>(Diagnostics);
            }

            var result = transform(Result);
            var allDiagnostics = Diagnostics.Concat(result.Diagnostics);

            if (result.HasResult)
            {
                return new SimpleOptionalWithDiagnostics<TNew>(result.Result, allDiagnostics);
            }

            return new SimpleOptionalWithDiagnostics<TNew>(allDiagnostics);
        }

        public IOptionalWithDiagnostics<TNew> Transform<TNew>(IOptionalWithDiagnostics<T>.DResult<TNew> transform)
        {
            if (LacksResult)
            {
                return new SimpleOptionalWithDiagnostics<TNew>(Diagnostics);
            }

            return new SimpleOptionalWithDiagnostics<TNew>(transform(Result), Diagnostics);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

public sealed class OptionalLacksResultException : Exception
{
    public OptionalLacksResultException() : base() { }
    public OptionalLacksResultException(string message) : base(message) { }
    public OptionalLacksResultException(string message, Exception innerException) : base(message, innerException) { }
}
