# How to: Block packets

# [Minimal](#tab/minimal)

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Block the packet.
    e.Block();
});
```

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
