let scalarQuantity : {
    type: "Scalar",
    name: string,
    baseUnits: string,
    unit: string,
    unitBias: string | boolean,
    vector: string | false,
    vectorDimensionalities: number[] | undefined,
    inverse: QuantityPower,
    square: QuantityPower,
    cube: QuantityPower,
    squareRoot: QuantityPower,
    cubeRoot: QuantityPower,
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
    convertible: QuantityPower,
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

type QuantityPower = string | string[] | false

export type ScalarQuantity = typeof scalarQuantity