# 2. Add xabbo libraries

To create the most basic extension, all you need to do is add the `Xabbo.Common` and `Xabbo.GEarth`
packages to your project.

# [.NET CLI](#tab/cli)

To add the `Xabbo.Common` and `Xabbo.GEarth` packages via the .NET CLI:

```sh
dotnet add package Xabbo.Common
dotnet add package Xabbo.GEarth
```

# [Visual Studio](#tab/vs)

TODO

---

This should add the references to your project file:

[!code-xml[](~/snippets/tutorial/2/Tutorial.csproj?highlight=10-13)]

> [!NOTE]
> Make sure you have at least `Xabbo.Common` version `1.0.1` for this tutorial.
