# 5. Add event handlers

The @Xabbo.GEarth.GEarthExtension class provides various events you can subscribe to.

- @Xabbo.GEarth.GEarthExtension.Initialized
- @Xabbo.GEarth.GEarthExtension.Activated
- @Xabbo.GEarth.GEarthExtension.Connected
- @Xabbo.GEarth.GEarthExtension.Intercepted
- @Xabbo.GEarth.GEarthExtension.Disconnected

Add the following event handlers that print information to the console:

[!code-csharp[](~/snippets/tutorial/5/Program.cs?highlight=12-15)]

Now, when you run your extension you should see some output:

```txt
Extension initialized.
Game connected. Session { Hotel = US, Client = Client { Type = Flash, Identifier = FLASH20, Version = WIN63-202408051224-787955622 } }
```

Clicking the green play button in G-Earth raises the @Xabbo.GEarth.GEarthExtension.Activated event,
which outputs:

```txt
Hello, from G-Earth!
```