namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IOptionalWithDiagnostics : IEnumerable<Diagnostic>
{
    public abstract bool HasResult { get; }
    public abstract bool LacksResult { get; }

    public abstract IValidityWithDiagnostics Reduce();

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }
}

public interface IOptionalWithDiagnostics<T> : IOptionalWithDiagnostics
{
    public delegate IValidityWithDiagnostics DValidator(T result);
    public delegate IOptionalWithDiagnostics<TNew> DTransform<TNew>(T result);
    public delegate TNew DResult<TNew>(T result);

    public abstract T Result { get; }
    public abstract T? NullableResult { get; }

    public abstract IOptionalWithDiagnostics<T> Validate(DValidator validator);
    public abstract IValidityWithDiagnostics Reduce(DValidator validator);
    public abstract IOptionalWithDiagnostics<TNew> Merge<TNew>(DTransform<TNew> transform);
    public abstract IOptionalWithDiagnostics<TNew> Transform<TNew>(DResult<TNew> transform);
}

public static class OptionalWithDiagnostics
{
    public static IOptionalWithDiagnostics<T> EmptyWithoutDiagnostics<T>() => Empty<T>(Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Result<T>(T result, IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleOptionalWithDiagnostics<T>(result, diagnostics);
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result, params Diagnostic[] diagnostics)
    {
        return Result(result, diagnostics as IEnumerable<Diagnostic>);
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Result(result);
        }

        return Result(result, new[] { diagnostics });
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result) => Result(result, Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Empty<T>(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleOptionalWithDiagnostics<T>(diagnostics);
    }

    public static IOptionalWithDiagnostics<T> Empty<T>(params Diagnostic[] diagnostics) => Empty<T>(diagnostics as IEnumerable<Diagnostic>);

    public static IOptionalWithDiagnostics<T> Empty<T>(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return EmptyWithoutDiagnostics<T>();
        }

        return Empty<T>(new[] { diagnostics });
    }

    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, T result, Func<Diagnostic?> invalidDiagnostics) => Conditional(condition, () => result, invalidDiagnostics);
    public static IOptionalWithDiagnostics<T> Conditional<T>(bool condition, Func<T> resultDelegate, Func<Diagnostic?> invalidDiagnostics)
    {
        if (condition is false)
        {
            return Empty<T>(invalidDiagnostics());
        }

        return Result(resultDelegate());
    }

    private class SimpleOptionalWithDiagnostics<T> : IOptionalWithDiagnostics<T>
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

        public T? NullableResult
        {
            get
            {
                if (LacksResult)
                {
                    return default;
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

        public IValidityWithDiagnostics Reduce()
        {
            if (HasResult)
            {
                return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics);
            }

            return ValidityWithDiagnostics.Invalid(Diagnostics);
        }

        public IValidityWithDiagnostics Reduce(IOptionalWithDiagnostics<T>.DValidator validator)
        {
            return Validate(validator).Reduce();
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
