let scalarQuantity : {
    type: "Scalar",
    name: string,
    baseUnits: string,
    unit: string,
    unitBias: string | boolean,
    vector: string | false,
    vectorDimensionalities: number[] | undefined,
    inverse: string | string[] | false,
    square: string | string[] | false,
    cube: string | string[] | false,
    squareRoot: string | string[] | false,
    cubeRoot: string | string[] | false,
    units: string | ({ 
        special: false | undefined,
        singular: string,
        plural: string,
        SI : boolean | undefined,
        symbol: string | undefined,
        base: true | false | undefined
    } | {
        special: true,
        separator: boolean | undefined
    })[],
    convertible: string | string[] | false,
    symbol: string | undefined,
    bases: string | ({ 
        special: false | undefined,
        singular: string,
        plural: string,
        SI : boolean | undefined,
        symbol: string | undefined,
        base: true | false | undefined
    } | {
        special: true,
        separator: boolean | undefined
    })[]
}

export type ScalarQuantity = typeof scalarQuantity