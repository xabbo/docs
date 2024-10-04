# How to: Block packets

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=block-packets)]

# [Derived](#tab/derived)

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    [InterceptOut("MoveAvatar")]
    void HandleMoveAvatar(Intercept e)
    {
        // Block the packet.
        e.Block();
    }
}
```
---
