# How to: Read packets

## Read a single value

To read from a packet, call `Read<T>`, passing in the type that you want to read from the packet:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=read-single-value)]

See the [structure of a packet](~/docs/in-depth/packet-structure.md) for in-depth information on
the different types you can read from a packet.

## Read multiple values

You can pass multiple types to `Read<T, ...>` to read a tuple containing multiple values:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=read-multiple-values)]

## Read an array

You can read an array of values by specifying an array type:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=read-array)]

A [Length](~/docs/in-depth/packet-structure.md#length) is read to determine the number of elements
in the array, after which elements of the specified type are read into the array.

The above `Read<Id[]>` is equivalent to the following:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=read-array-manual)]

## Read parsers

Any type that implements @Xabbo.Messages.IParser`1 can be read from a packet:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=read-parser)]