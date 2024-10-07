# Extension types

There are two main ways to write an extension - minimal and derived.

## Minimal

With a minimal extension you instantiate a @Xabbo.GEarth.GEarthExtension and interact with it
directly:

`Program.cs`:
[!code-csharp[Program.cs](~/src/examples/extension-types/minimal/Program.cs)]

This works well for simple extensions using C#
[top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/top-level-statements).

## Derived

This is where you inherit @Xabbo.GEarth.GEarthExtension and write your extension logic inside the
class:

`MyExtension.cs`:
[!code-csharp[MyExtension.cs](~/src/examples/extension-types/derived/MyExtension.cs)]

`Program.cs`:
[!code-csharp[MyExtension.cs](~/src/examples/extension-types/derived/Program.cs)]

The source generator will locate the @"Xabbo.ExtensionAttribute?text=[Extension]" and
@"Xabbo.InterceptAttribute?text=[Intercept]" attributes and implement the necessary interfaces to
initialize the extension information and wire up the intercept handlers.

For example, the generator will emit the source code below to the code below for the above extension
to initialize its information and intercept the `MoveAvatar` packet:

`Examples.ExtensionTypes.Derived.MyExtension.Extension.g.cs`:
[!code-csharp[](~/src/examples/extension-types/derived/Generated/Xabbo.Common.Generator/Xabbo.Common.Generator.Generator/Examples.ExtensionTypes.Derived.MyExtension.Extension.g.cs)]

`Examples.ExtensionTypes.Derived.MyExtension.Interceptor.g.cs`:
[!code-csharp[](~/src/examples/extension-types/derived/Generated/Xabbo.Common.Generator/Xabbo.Common.Generator.Generator/Examples.ExtensionTypes.Derived.MyExtension.Interceptor.g.cs)]
