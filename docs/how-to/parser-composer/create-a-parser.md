# Create a parser

To create a parser, you must implement the @Xabbo.Messages.IParser`1 interface.

We will be using the `WallItem` model we created from [create a model](create-a-model.md).

## Implement the IParser interface

Add the `IParser<T>` interface and its `Parse` method implementation to your model.
In `Parse` you have access to the `PacketReader` that allows you to read primitive types from the
packet. This is where you read each value, apply them to an object and return the result.

[!code-csharp[](~/src/examples/parser/WallItem.cs?range=10-11,21-41)]

## In action

Now that we have created our own parser, we are able to read it from a packet.
Let's intercept the `ItemAdd` packet and print to the console whenever someone places a wall item:

[!code-csharp[](~/src/examples/parser/Program.cs?name=snippet)]

When you run the extension and place some wall items, you should see the information printed to the
console:

```txt
❯ dotnet run
(name) placed a wall item with ID 2147418119 of kind 4395 at :w=6,1 l=29,54 r
(name) placed a wall item with ID 2147418120 of kind 4395 at :w=8,1 l=3,60 r
(name) placed a wall item with ID 2147418121 of kind 4395 at :w=7,1 l=26,53 r
(name) placed a wall item with ID 2147418122 of kind 4395 at :w=9,1 l=2,33 r
```

> [!Additional info]
> If you removed the `IParser<T>` interface from your class and attempted to compile the
> above example, the xabbo analyzer would emit an error because it cannot be read from a packet:
>
> ```txt
> ❯ dotnet build
> MSBuild version 17.8.5+b5265ef37 for .NET
>   Determining projects to restore...
>   All projects are up-to-date for restore.
> /tmp/parser/Program.cs(15,30): error XABBO011: 'Examples.Parser.WallItem' is not a packet primitive or IParser<T> implementation [/tmp/parser/Examples.Parser.csproj]
>
> Build FAILED.
>
> /tmp/parser/Program.cs(15,30): error XABBO011: 'Examples.Parser.WallItem' is not a packet primitive or IParser<T> implementation [/tmp/parser/Examples.Parser.csproj]
>     0 Warning(s)
>     1 Error(s)
>
> Time Elapsed 00:00:00.99
> ```