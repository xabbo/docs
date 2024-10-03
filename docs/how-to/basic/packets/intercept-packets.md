# How to: Intercept packets

## Intercept a single packet

# [Minimal](#tab/minimal)

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    Console.WriteLine("Intercepted MoveAvatar");
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
        Console.WriteLine("Intercepted MoveAvatar");
    }
}
```

To get auto-completion we could also use the `nameof` keyword:
```csharp
[InterceptOut(nameof(Out.MoveAvatar))]
```

---

## Intercept multiple packets

# [Minimal](#tab/minimal)

To intercept multiple packets, place them inside a collection expression:

```csharp
ext.Intercept([Out.Chat, Out.Shout, Out.Whisper], e => {
    Console.WriteLine("Intercepted Chat, Shout or Whisper");
    // To check which one was intercepted:
    if (e.Is(Out.Whisper))
    {
        Console.WriteLine("Intercepted Whisper");
    }
});
```

# [Derived](#tab/derived)

To intercept multiple packets, pass multiple identifier names to the attribute constructor:

```csharp
[Extension]
partial class MyExtension : GEarthExtension
{
    [InterceptOut("Chat", "Shout", "Whisper")]
    void HandleMoveAvatar(Intercept e)
    {
        Console.WriteLine("Intercepted Chat, Shout or Whisper");
        // To check which one was intercepted:
        if (e.Is(Out.Whisper))
        {
            Console.WriteLine("Intercepted Whisper");
        }
    }
}
```

---
