# Extension types

There are two main ways to write an extension - minimal and derived.

## Minimal

With a minimal extension you instantiate a @Xabbo.GEarth.GEarthExtension and interact with it directly:

`Program.cs`:
```csharp
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

// Instantiate a new GEarthExtension.
var ext = new GEarthExtension(new GEarthOptions {
    Name = "Minimal",
    Description = "a minimal example"
});

// Intercept a walk message and shout out the coordinates.
ext.Intercept(Out.MoveAvatar, e => {
    var (x, y) = e.Packet.Read<int, int>();
    ext.Send(new ShoutMsg($"I am walking to {x}, {y}"));
});

// Run the extension.
ext.Run();
```

This works well for simple extensions using C# [top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/top-level-statements).

## Derived

This is where you inherit @Xabbo.GEarth.GEarthExtension and write your extension logic inside the class:

`MyExtension.cs`:
```csharp
using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

[Extension(
   Name = "Derived",
   Description = "a derived example"
)]
partial class MyExtension : GEarthExtension
{
    // Intercept a walk message and shout out the coordinates.
    [InterceptOut(nameof(Out.MoveAvatar))]
    void OnWalk(Intercept e)
    {
        var (x, y) = e.Packet.Read<int, int>();
        Send(new ShoutMsg($"I am walking to {x}, {y}"));
    }
}
```

`Program.cs`:
```csharp
new MyExtension().Run();
```

The source generator will locate the @"Xabbo.ExtensionAttribute?text=[Extension]" and @"Xabbo.InterceptAttribute?text=[Intercept]" attributes and implement the necessary interfaces to initialize the extension information and wire up the intercept handlers.

For example, the generator will emit sources equivalent to the code below for the above extension to initialize its information and intercept the `MoveAvatar` packet:

`MyExtension.Extension.g.cs`:
```csharp
partial class MyExtension : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new ExtensionInfo(
        Name: "Derived",
        Description: "a derived example"
    );
}
```

`MyExtension.Interceptor.g.cs`:
```csharp
partial class MyExtension : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    new Identifier(ClientType.None, Direction.Out, "MoveAvatar")
                ],
                OnWalk
            ) { Target = ClientType.All }
        ]));
    }
}
```