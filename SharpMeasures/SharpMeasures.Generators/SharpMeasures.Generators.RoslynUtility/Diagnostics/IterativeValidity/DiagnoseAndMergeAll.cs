namespace SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public static partial class IterativeValidity
{
    public static IValidityWithDiagnostics DiagnoseAndMergeAll(IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeAll(IValidityWithDiagnostics.MergeOperation.AND, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll(params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll(IValidityWithDiagnostics.MergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeAll(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll(IValidityWithDiagnostics.MergeOperation mergeOperation, params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(mergeOperation, delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(T argument, IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(ValidityWithDiagnostics.MergeOperations.AND, argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(T argument, params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(mergeOperation, argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(T1 argument1, T2 argument2, IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(ValidityWithDiagnostics.MergeOperations.AND, argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(T1 argument1, T2 argument2, params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeAll(mergeOperation, argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeAll<T>(IValidityWithDiagnostics.DMergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeAll<T1, T2>(IValidityWithDiagnostics.DMergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeAll(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T1, T2> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument1, argument2);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeAll(IValidityWithDiagnostics.DMergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        IValidityWithDiagnostics result = ValidityWithDiagnostics.Valid;

        foreach (DValidity delegatedDiagnosis in delegatedValidity)
        {
            result = result.Merge(mergeOperation, delegatedDiagnosis());
        }

        return result;
    }
}
