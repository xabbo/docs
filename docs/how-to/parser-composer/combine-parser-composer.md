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

> [!Additional info]
> If you removed either of the IParser<T> or IComposer implementations from your class and attempted
> to compile the above example, the xabbo analyzer would emit an error because it cannot be read
> from or written to a packet:
>
> ```txt
> ❯ dotnet build
> MSBuild version 17.8.5+b5265ef37 for .NET
>   Determining projects to restore...
>   All projects are up-to-date for restore.
> /tmp/parser-composer/Program.cs(16,21): error XABBO013: 'Examples.ParserComposer.WallItem' is not a packet primitive or IParserComposer<T> implementation [/tmp/parser-composer/Examples.ParserComposer.csproj]
>
> Build FAILED.
>
> /tmp/parser-composer/Program.cs(16,21): error XABBO013: 'Examples.ParserComposer.WallItem' is not a packet primitive or IParserComposer<T> implementation [/tmp/parser-composer/Examples.ParserComposer.csproj]
>     0 Warning(s)
>     1 Error(s)
>
> Time Elapsed 00:00:00.75
> ```