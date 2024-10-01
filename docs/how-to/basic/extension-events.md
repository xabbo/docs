# How to: Handle extension events

## Initialized

Occurs when your extension is initialized by G-Earth.

Accepts an @Xabbo.InitializedEventArgs.

Packet information is **not** yet available, so attempting to send a packet (by identifier) here will fail.

# [Minimal](#tab/minimal)

```csharp
ext.Initialized += e => {
    Console.WriteLine($"Extension initialized.");
    Console.WriteLine($"Is game connected: {e.IsGameConnected}.");
};
```

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnInitialized(InitializedEventArgs e)
    {
        base.OnInitialized(e);

        Console.WriteLine($"Extension initialized.");
        Console.WriteLine($"Is game connected: {e.IsGameConnected}.");
    }
}
```

---

## Connected

Occurs when the game is connected.

Accepts a @Xabbo.ConnectedEventArgs which provides information about the game host and port,
whether the connection was already established before the extension was initialized,
and the @Xabbo.Session containing the current @Xabbo.Hotel and @Xabbo.Client.

# [Minimal](#tab/minimal)

```csharp
ext.Connected += e => {
    Console.WriteLine($"Game connected.");
    Console.WriteLine($"Is game connected: {e.IsGameConnected}.");
};
```

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnConnected(ConnectedEventArgs e)
    {
        base.OnConnected(e);

        Console.WriteLine($"Connected to the {e.Session.Hotel.Name} hotel via the {e.Session.Client.Type} client version {e.Session.Client.Version}.");
        Console.WriteLine($"Was connection already established? {e.PreEstablished}");
    }
}
```

---

## Activated

Occurs when the user clicks the green play button next to your extension in G-Earth.

# [Minimal](#tab/minimal)

```csharp
ext.Activated += () => {
    Console.WriteLine("Extension activated.");
};
```

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnActivated()
    {
        base.OnActivated();

        Console.WriteLine("Extension activated.");
    }
}
```

---

## Intercepted

Occurs when a packet is intercepted.

Accepts an @Xabbo.Intercept.

# [Minimal](#tab/minimal)

```csharp
ext.Intercepted += e => {
    Console.WriteLine($"{e.Packet.Header.Direction} packet {e.Packet.Header.Value} intercepted");
};
```

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnIntercepted(Intercept e)
    {
        base.OnIntercepted(e);

        Console.WriteLine($"{e.Packet.Header.Direction} packet {e.Packet.Header.Value} intercepted");
    }

}
```

---

## Disconnected

Occurs when the game disconnects.

# [Minimal](#tab/minimal)

```csharp
ext.Disconnected += () => {
    Console.WriteLine("Game disconnected.");
};
```

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnDisconnected()
    {
        base.OnDisconnected();

        Console.WriteLine("Game disconnected.");
    }
}
```