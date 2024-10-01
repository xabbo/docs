# How to: Create and run an extension

To create an extension, add the `Xabbo.GEarth` package to your project:

```sh
dotnet add package Xabbo.GEarth
```

# [Minimal](#tab/minimal)

```csharp
using Xabbo.GEarth;

// Create the extension.
var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "how to create an extension",
    Author = "xabbo",
    Version = "1.0"
});

// Register event and intercept handlers here.

// Run the extension.
ext.Run();
```

# [Derived](#tab/derived)

in `MyExtension.cs`:

```csharp
using Xabbo;
using Xabbo.GEarth;

// Declare the extension metadata.
[Extension(
    Name = "How to",
    Description = "how to create an extension",
    Author = "xabbo",
    Version = "1.0"
)]
// Mark your class as partial - the source generator adds code to your extension class.
partial class MyExtension : GEarthExtension
{
    // Define intercept handlers here.
}
```

in `Program.cs`:

```csharp
// Create and run the extension.
new MyExtension().Run();
```

---
