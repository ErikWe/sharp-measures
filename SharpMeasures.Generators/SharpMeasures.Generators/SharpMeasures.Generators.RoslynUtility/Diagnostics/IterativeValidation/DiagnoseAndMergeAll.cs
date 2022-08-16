namespace SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public static partial class IterativeValidation
{
    public static IValidityWithDiagnostics DiagnoseAndMergeAll(IEnumerable<IValidityWithDiagnostics.DValidity> delegatedValidity)
    {
        IValidityWithDiagnostics result = ValidityWithDiagnostics.Valid;

        foreach (IValidityWithDiagnostics.DValidity delegatedDiagnosis in delegatedValidity)
        {
            result = result.Validate(delegatedDiagnosis());
        }

        return result;
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll(params IValidityWithDiagnostics.DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(T parameter, IEnumerable<IValidityWithDiagnostics.DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (var delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(T parameter, params IValidityWithDiagnostics.DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(parameter, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(T1 parameter1, T2 parameter2, IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (var delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter1, parameter2);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(T1 parameter1, T2 parameter2, params IValidityWithDiagnostics.DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(parameter1, parameter2, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2, T3>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (var delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter1, parameter2, parameter3);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, params IValidityWithDiagnostics.DValidity<T1, T2, T3>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(parameter1, parameter2, parameter3, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2, T3>>);
    }
}
