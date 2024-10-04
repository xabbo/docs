# How to: Send packets

Make sure you have added the `Xabbo.Messages` package to get access to named message identifiers e.g. `Out.Chat`. To add the package to your project:

```
dotnet add package Xabbo.Messages
```

Then add a using statement for the client you prefer to work with:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?range=3,5-6)]

This will give you access to either the @Xabbo.Messages.Flash?text=Flash or @Xabbo.Messages.Shockwave?text=Shockwave identifiers.

## Send packets by identifier

# [Minimal](#tab/minimal)

Without the `Xabbo.Messages` package.

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=send-by-identifier-implicit)]

[!code-csharp[](~/src/examples/packets/minimal-shockwave/Program.cs?name=send-by-identifier-implicit)]

With the `Xabbo.Messages` package.

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=send-by-identifier)]

[!code-csharp[](~/src/examples/packets/minimal-shockwave/Program.cs?name=send-by-identifier)]

# [Derived](#tab/derived)

Without the `Xabbo.Messages` package.

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    void HowToSendPacketsByIdentifier()
    {
        Send((ClientType.Flash, Direction.Out, "MoveAvatar"), 3, 4);
        // - or -
        Send((ClientType.Shockwave, Direction.Out, "MOVE"), (short)3, (short)4);
    }
}
```

With the `Xabbo.Messages` package.

```csharp
using Xabbo.Messages.Flash;
// ...
[Extension]
partial class MyExtension : GEarthExtension
{
    void HowToSendPacketsByIdentifier()
    {
        Send(Out.MoveAvatar, 3, 4);
    }
}
```

```csharp
using Xabbo.Messages.Shockwave;
// ...
[Extension]
partial class MyExtension : GEarthExtension
{
    void HowToSendPacketsByIdentifier()
    {
        Send(Out.MOVE, (short)3, (short)4);
    }
}
```

---

## Send packets to the client

To send packets to the client, simply specify an incoming identifier or header.

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=send-to-client)]

## Send packets by header

The use-case for this is limited, but it is possible to send packets with an explicit header value.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=send-header)]

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    void HowToSendPacketsByHeader()
    {
        Send((Direction.Out, 123), "Packet contents");
    }
}
```

---
