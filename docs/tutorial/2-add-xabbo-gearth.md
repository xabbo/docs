# 2. Add Xabbo.GEarth

To create the most basic extension, all you need to do is add the `Xabbo.GEarth` package to your project. Since `Xabbo.GEarth` references the `Xabbo.Common` library, it will also be restored as a transitive dependency.

# [.NET CLI](#tab/cli)

To add the `Xabbo.GEarth` package via the .NET CLI:

```sh
dotnet add package Xabbo.GEarth
```

# [Visual Studio](#tab/vs)

TODO

---

This should add the reference to your project file:

[!code-xml[](../../snippets/tutorial/2/Tutorial.csproj?highlight=10-12)]