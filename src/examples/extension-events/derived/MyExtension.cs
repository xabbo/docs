using Xabbo;
using Xabbo.GEarth;

namespace Examples.ExtensionEvents.Derived;

[Extension]
partial class MyExtension : GEarthExtension
{
    protected override void OnInitialized(InitializedEventArgs e)
    {
        base.OnInitialized(e);

        Console.WriteLine($"Extension initialized.");
        Console.WriteLine($"Is game connected: {e.IsGameConnected}.");
    }

    protected override void OnConnected(ConnectedEventArgs e)
    {
        base.OnConnected(e);

        Console.WriteLine($"Connected to the {e.Session.Hotel.Name} hotel via the {e.Session.Client.Type} client version {e.Session.Client.Version}.");
        Console.WriteLine($"Was connection already established? {e.PreEstablished}");
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        Console.WriteLine("Extension activated.");
    }

    protected override void OnIntercepted(Intercept e)
    {
        base.OnIntercepted(e);

        Console.WriteLine($"{e.Packet.Header.Direction} packet {e.Packet.Header.Value} intercepted");
    }

    protected override void OnDisconnected()
    {
        base.OnDisconnected();

        Console.WriteLine("Game disconnected.");
    }
}