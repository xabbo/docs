# Create a composer

To create a composer, you must implement the @Xabbo.Messages.IComposer interface.

We will be using the `WallItem` model we created from [create a model](create-a-model.md).

## Implement the IComposer interface

Add the `IComposer` interface and its `Compose` method implementation to your model.
In `Compose` you have access to the `PacketWriter` that allows you to write primitive types to the
packet. This is where you construct the packet by writing each of the values from the object.

[!code-csharp[](~/src/examples/composer/WallItem.cs?range=6-7,17-31)]

## In action

Now that we have created our own composer, we are able to write it to a packet.

Let's send a new wall item to the client when we activate the extension. Add the following event
handler to your extension:

[!code-csharp[](~/src/examples/composer/Program.cs?name=snippet)]

When you run the extension and activate it, you should see a Jolly Roger appear somewhere in your
room:

![Injecting a wall item into a room](~/videos/compose-wall-item.mp4)

> [!WARNING]
> You must make sure that the packet structure and its data is correctly formatted. Sending a
> malformed packet to the client can cause it to crash with a black screen, and you will have to
> close the game and log back in.

> [!Additional info]
> If you removed the `IComposer` interface from your class and attempted to compile the
> above example, the xabbo analyzer would emit an error because it cannot be written to a packet:
>
> ```txt
> ❯ dotnet build
> MSBuild version 17.8.5+b5265ef37 for .NET
>   Determining projects to restore...
>   All projects are up-to-date for restore.
> /tmp/composer/Program.cs(26,26): error XABBO012: 'Examples.Composer.WallItem' is not a packet primitive or IComposer implementation [/tmp/composer/Examples.Composer.csproj]
>
> Build FAILED.
>
> /tmp/composer/Program.cs(26,26): error XABBO012: 'Examples.Composer.WallItem' is not a packet primitive or IComposer implementation [/tmp/composer/Examples.Composer.csproj]
>     0 Warning(s)
>     1 Error(s)
>
> Time Elapsed 00:00:01.29
> ```