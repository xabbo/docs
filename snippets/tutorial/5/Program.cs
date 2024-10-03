using Xabbo.GEarth;

// Create the extension.
var ext = new GEarthExtension(new GEarthOptions {
    Name = "Tutorial",
    Description = "a tutorial extension",
    Author = "xabbo",
    Version = "1.0"
});

// Subscribe to extension events.
ext.Initialized += (e) => Console.WriteLine("Extension initialized.");
ext.Connected += (e) => Console.WriteLine($"Game connected. {e.Session}");
ext.Disconnected += () => Console.WriteLine("Game disconnected.");
ext.Activated += () => Console.WriteLine("Hello from G-Earth!");

// Run the extension.
ext.Run();