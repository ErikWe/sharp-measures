let scalarQuantity : {
    type: "Scalar",
    name: string,
    vector: string | false,
    inverse: string[] | undefined,
    square: string[] | undefined,
    cube: string[] | undefined,
    squareRoot: string[] | undefined,
    cubeRoot: string[] | undefined,
    unit: string,
    unitBias: boolean | undefined,
    defaultUnit: string | undefined,
    includeUnits: string[] | undefined,
    excludeUnits: string[] | undefined,
    includeBases: string[] | undefined,
    excludeBases: string[] | undefined,
    includeConstants: string[] | undefined,
    excludeConstants: string[] | undefined,
    convertible: string[] | undefined
}

export type ScalarQuantity = typeof scalarQuantity