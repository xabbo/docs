# 3. Connect to G-Earth

Replace your `Program.cs` with the following code:

```csharp
using Xabbo.GEarth;

// Create the extension.
var ext = new GEarthExtension();

// Run the extension.
ext.Run();
```

This creates an instance of `GEarthExtension` and runs it.
This is all you need for your extension to connect to G-Earth and show up in the extension list.

Make sure you have G-Earth running and connected. You can then run your program:

# [.NET CLI](#tab/cli)

```sh
dotnet run
```

# [Visual Studio](#tab/vs)

TODO

---

Once your program is running, you should see it in G-Earth's extension list:

![G-Earth](~/images/tutorial/3-1.png)
