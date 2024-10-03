# 1. Create a new project

First, create a new console project.

# [.NET CLI](#tab/cli)

Run the following command:

```sh
dotnet new console -n Tutorial
```

# [Visual Studio](#tab/vs)

TODO

---

You should now have a directory with a C# project `.csproj` and a `Program.cs`:

```txt
 Tutorial
├── 󰌛 Program.cs
└── 󰌛 Tutorial.csproj
```

The `Program.cs` should contain a basic "Hello, World!" example:

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

