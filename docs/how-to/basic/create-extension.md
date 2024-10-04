# How to: Create and run an extension

To create an extension, add the `Xabbo.GEarth` package to your project:

```sh
dotnet add package Xabbo.GEarth
```

# [Minimal](#tab/minimal)

in `Program.cs`

[!code-xml[](~/src/examples/create-extension/minimal/Program.cs)]

# [Derived](#tab/derived)

in `MyExtension.cs`:

[!code-xml[](~/src/examples/create-extension/inherited/MyExtension.cs)]

in `Program.cs`:

[!code-xml[](~/src/examples/create-extension/inherited/Program.cs)]

---
