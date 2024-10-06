# How to: Intercept packets

## Intercept a single packet

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-single-identifier)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=intercept-single-identifier)]

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

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=intercept-multiple-identifiers)]

---
