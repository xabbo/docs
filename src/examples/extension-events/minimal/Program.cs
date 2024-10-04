using Xabbo.GEarth;

var ext = new GEarthExtension();

ext.Initialized += e => {
    Console.WriteLine($"Extension initialized.");
    Console.WriteLine($"Is game connected: {e.IsGameConnected}.");
};

ext.Connected += e => {
    Console.WriteLine($"Connected to the {e.Session.Hotel.Name} hotel via the {e.Session.Client.Type} client version {e.Session.Client.Version}.");
    Console.WriteLine($"Was connection already established? {e.PreEstablished}");
};

ext.Activated += () => {
    Console.WriteLine("Extension activated.");
};

ext.Intercepted += e => {
    Console.WriteLine($"{e.Packet.Header.Direction} packet {e.Packet.Header.Value} intercepted");
};

ext.Disconnected += () => {
    Console.WriteLine("Game disconnected.");
};

ext.Run();
