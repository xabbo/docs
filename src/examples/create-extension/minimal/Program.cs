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