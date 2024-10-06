# Combine a parser and composer

To combine a parser and composer, implement @Xabbo.Messages.IParserComposer`1
which inherits from @Xabbo.Messages.IParser`1 and @Xabbo.Messages.IComposer.

After following the previous guides on [how to create a parser](create-a-parser.md) and
[a composer](create-a-composer.md), let's combine our `WallItem` parser and composer.

Add the `IParserComposer<WallItem>` interface and place both of the `Parse` and `Compose` methods
in your class:

[!code-csharp[](~/src/examples/parser-composer/WallItem.cs?range=6-8,19-22,39-43,52-53)]

This allows us to read *and* write the object from/to a packet, and opens up the possibility of
using `Replace` and `Modify`.

Let's use `Modify` to change all wall items that we place into the "Jolly Roger" poster client-side.

[!code-csharp[](~/src/examples/parser-composer/Program.cs?name=snippet)]

When we run the extension, any wall item we place will be modified:

![](~/videos/modify-wall-item.mp4)