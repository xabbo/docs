# How to: Use Xabbo.Core parsers

The @Xabbo.Core library provides many parsers and composers that you can use in your extensions.
Most of them implement @Xabbo.Messages.IParserComposer`1 and can therefore both be read from and
written to packets. They are designed to work across different clients where possible.

## Examples

### Using core parsers

Using the @Xabbo.Core.Avatar parser to read the list of avatars added to the room:

[!code-csharp[](~/src/examples/core-parsers/minimal/Program.cs?name=parsers)]

### Using core composers

Using the @Xabbo.Core.Avatar composer to inject a client-side avatar into the room:

[!code-csharp[](~/src/examples/core-parsers/minimal/Program.cs?name=composers)]

### Using core parser/composers

Using the @Xabbo.Core.Avatar parser/composer to modify all avatars in the room to have the same
figure:

[!code-csharp[](~/src/examples/core-parsers/minimal/Program.cs?name=parser-composers)]