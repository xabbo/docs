# 4. Add extension information

To add information about your extension such as its name and author, you can pass `GEarthOptions` to
the `GEarthExtension` constructor:

```csharp
using Xabbo.GEarth;

// Create the extension.
var ext = new GEarthExtension(new GEarthOptions {
  Name = "Tutorial",
  Description = "a tutorial extension",
  Author = "xabbo",
  Version = "1.0"
});

// Run the extension.
ext.Run();
```

When you run the program again, you should see the updated information in G-Earth:

![G-Earth](~/images/tutorial/4-1.png)
