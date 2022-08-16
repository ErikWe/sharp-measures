namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IResultWithDiagnostics<T> : IOptionalWithDiagnostics<T>
{
    new public delegate IResultWithDiagnostics<TNew> DTransform<TNew>(T result);
    new public delegate IResultWithDiagnostics<TNew> DTransform<TNew, T1>(T1 parameter1, T result);
    new public delegate IResultWithDiagnostics<TNew> DTransform<TNew, T1, T2>(T1 parameter1, T2 parameter2, T result);
    new public delegate IResultWithDiagnostics<TNew> DTransform<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, T result);

    public abstract IResultWithDiagnostics<TNew> Merge<TNew>(DTransform<TNew> transform);
    public abstract IResultWithDiagnostics<TNew> Merge<TNew, T1>(T1 parameter, DTransform<TNew, T1> transform);
    public abstract IResultWithDiagnostics<TNew> Merge<TNew, T1, T2>(T1 parameter1, T2 parameter2, DTransform<TNew, T1, T2> transform);
    public abstract IResultWithDiagnostics<TNew> Merge<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, DTransform<TNew, T1, T2, T3> transform);

    new public abstract IResultWithDiagnostics<TNew> Transform<TNew>(DResult<TNew> transform);
    new public abstract IResultWithDiagnostics<TNew> Transform<TNew, T1>(T1 parameter, DResult<TNew, T1> transform);
    new public abstract IResultWithDiagnostics<TNew> Transform<TNew, T1, T2>(T1 parameter1, T2 parameter2, DResult<TNew, T1, T2> transform);
    new public abstract IResultWithDiagnostics<TNew> Transform<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, DResult<TNew, T1, T2, T3> transform);
}

public static class ResultWithDiagnostics
{
    public static IResultWithDiagnostics<T> Construct<T>(T result, IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleResultWithDiagnostics<T>(result, diagnostics);
    }

    public static IResultWithDiagnostics<T> Construct<T>(T result, params Diagnostic[] diagnostics)
    {
        return Construct(result, diagnostics as IEnumerable<Diagnostic>);
    }

    public static IResultWithDiagnostics<T> Construct<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Construct(result);
        }

        return Construct(result, new[] { diagnostics });
    }

    private class SimpleResultWithDiagnostics<T> : IResultWithDiagnostics<T>
    {
        public T Result { get; }
        public IEnumerable<Diagnostic> Diagnostics { get; }

        T? IOptionalWithDiagnostics<T>.NullableResult => Result;

        public SimpleResultWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics)
        {
            Result = result;
            Diagnostics = diagnostics;
        }

        public IOptionalWithDiagnostics<T> Validate(IOptionalWithDiagnostics<T>.DValidator validator)
        {
            var validity = validator(Result);
            var allDiagnostics = Diagnostics.Concat(validity.Diagnostics);

            if (validity.IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(allDiagnostics);
            }

            return new SimpleResultWithDiagnostics<T>(Result, allDiagnostics);
        }

        public IOptionalWithDiagnostics<T> Validate<T1>(T1 parameter, IOptionalWithDiagnostics<T>.DValidator<T1> validator)
        {
            return Validate(wrappedValidator);

            IValidityWithDiagnostics wrappedValidator(T result) => validator(parameter, result);
        }

        public IOptionalWithDiagnostics<T> Validate<T1, T2>(T1 parameter1, T2 parameter2, IOptionalWithDiagnostics<T>.DValidator<T1, T2> validator)
        {
            return Validate(wrappedValidator);

            IValidityWithDiagnostics wrappedValidator(T result) => validator(parameter1, parameter2, result);
        }

        public IOptionalWithDiagnostics<T> Validate<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IOptionalWithDiagnostics<T>.DValidator<T1, T2, T3> validator)
        {
            return Validate(wrappedValidator);

            IValidityWithDiagnostics wrappedValidator(T result) => validator(parameter1, parameter2, parameter3, result);
        }

        public IValidityWithDiagnostics Reduce()
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics);
        }

        public IValidityWithDiagnostics Reduce(IOptionalWithDiagnostics<T>.DValidator validator)
        {
            return Validate(validator).Reduce();
        }

        public IValidityWithDiagnostics Reduce<T1>(T1 parameter1, IOptionalWithDiagnostics<T>.DValidator<T1> validator)
        {
            return Validate(parameter1, validator).Reduce();
        }

        public IValidityWithDiagnostics Reduce<T1, T2>(T1 parameter1, T2 parameter2, IOptionalWithDiagnostics<T>.DValidator<T1, T2> validator)
        {
            return Validate(parameter1, parameter2, validator).Reduce();
        }

        public IValidityWithDiagnostics Reduce<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IOptionalWithDiagnostics<T>.DValidator<T1, T2, T3> validator)
        {
            return Validate(parameter1, parameter2, parameter3, validator).Reduce();
        }

        public IResultWithDiagnostics<TNew> Merge<TNew>(IResultWithDiagnostics<T>.DTransform<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result.Result, Diagnostics.Concat(result.Diagnostics));
        }

        public IResultWithDiagnostics<TNew> Merge<TNew, T1>(T1 parameter, IResultWithDiagnostics<T>.DTransform<TNew, T1> transform)
        {
            return Merge(wrappedTransform);

            IResultWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter, result);
        }

        public IResultWithDiagnostics<TNew> Merge<TNew, T1, T2>(T1 parameter1, T2 parameter2, IResultWithDiagnostics<T>.DTransform<TNew, T1, T2> transform)
        {
            return Merge(wrappedTransform);

            IResultWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter1, parameter2, result);
        }

        public IResultWithDiagnostics<TNew> Merge<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IResultWithDiagnostics<T>.DTransform<TNew, T1, T2, T3> transform)
        {
            return Merge(wrappedTransform);

            IResultWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter1, parameter2, parameter3, result);
        }

        public IResultWithDiagnostics<TNew> Transform<TNew>(IOptionalWithDiagnostics<T>.DResult<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result, Diagnostics);
        }

        public IResultWithDiagnostics<TNew> Transform<TNew, T1>(T1 parameter, IOptionalWithDiagnostics<T>.DResult<TNew, T1> transform)
        {
            return Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter, result);
        }

        public IResultWithDiagnostics<TNew> Transform<TNew, T1, T2>(T1 parameter1, T2 parameter2, IOptionalWithDiagnostics<T>.DResult<TNew, T1, T2> transform)
        {
            return Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter1, parameter2, result);
        }

        public IResultWithDiagnostics<TNew> Transform<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IOptionalWithDiagnostics<T>.DResult<TNew, T1, T2, T3> transform)
        {
            return Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter1, parameter2, parameter3, result);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IOptionalWithDiagnostics.HasResult => true;
        bool IOptionalWithDiagnostics.LacksResult => false;

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Merge<TNew>(IOptionalWithDiagnostics<T>.DTransform<TNew> transform)
        {
            var result = transform(Result);
            var allDiagnostics = Diagnostics.Concat(result.Diagnostics);

            if (result.HasResult)
            {
                return new SimpleResultWithDiagnostics<TNew>(result.Result, allDiagnostics);
            }

            return OptionalWithDiagnostics.Empty<TNew>(allDiagnostics);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Merge<TNew, T1>(T1 parameter, IOptionalWithDiagnostics<T>.DTransform<TNew, T1> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Merge(wrappedTransform);

            IOptionalWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter, result);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Merge<TNew, T1, T2>(T1 parameter1, T2 parameter2, IOptionalWithDiagnostics<T>.DTransform<TNew, T1, T2> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Merge(wrappedTransform);

            IOptionalWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter1, parameter2, result);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Merge<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IOptionalWithDiagnostics<T>.DTransform<TNew, T1, T2, T3> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Merge(wrappedTransform);

            IOptionalWithDiagnostics<TNew> wrappedTransform(T result) => transform(parameter1, parameter2, parameter3, result);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Transform<TNew>(IOptionalWithDiagnostics<T>.DResult<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result, Diagnostics);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Transform<TNew, T1>(T1 parameter, IOptionalWithDiagnostics<T>.DResult<TNew, T1> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter, result);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Transform<TNew, T1, T2>(T1 parameter1, T2 parameter2, IOptionalWithDiagnostics<T>.DResult<TNew, T1, T2> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter1, parameter2, result);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Transform<TNew, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IOptionalWithDiagnostics<T>.DResult<TNew, T1, T2, T3> transform)
        {
            return (this as IOptionalWithDiagnostics<T>).Transform(wrappedTransform);

            TNew wrappedTransform(T result) => transform(parameter1, parameter2, parameter3, result);
        }
    }
}
