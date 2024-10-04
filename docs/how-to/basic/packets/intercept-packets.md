# How to: Intercept packets

## Intercept a single packet

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-single-identifier)]

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

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-multiple-identifiers)]

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
