namespace Examples.ExtensionTypes.Derived
{
    partial class MyExtension : global::Xabbo.Messages.IMessageHandler
    {
        global::System.IDisposable global::Xabbo.Messages.IMessageHandler.Attach(global::Xabbo.Interceptor.IInterceptor interceptor)
        {
            return interceptor.Dispatcher.Register(new global::Xabbo.Messages.InterceptGroup([
                new global::Xabbo.Messages.InterceptHandler(
                    (global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier>)[
                        new global::Xabbo.Messages.Identifier(global::Xabbo.ClientType.None, global::Xabbo.Direction.Out, "MoveAvatar")
                    ],
                    OnWalk
                ) { Target = global::Xabbo.ClientType.All }
            ]));
        }
    }
}
