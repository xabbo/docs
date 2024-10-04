using Xabbo;
using Xabbo.GEarth;

namespace Examples.CreateExtension.Derived;

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