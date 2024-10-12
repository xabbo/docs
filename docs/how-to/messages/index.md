# Messages

Messages are an abstraction over packets, and implement the @Xabbo.Messages.IMessage`1 interface.
Like packets, messages can be sent, received, intercepted and modified. However, because messages
are associated with identifiers via their implementation, you do not need to specify an identifier
when sending or intercepting them.

They also provide a way to send and intercept packets in a cross-client manner, as they have access
to the interceptor's context and can format themselves correctly for the current client session.

The `Xabbo.Core` library provides many messages that you can use in your extensions. See the
documentation pages for @Xabbo.Core.Messages.Incoming and @Xabbo.Core.Messages.Outgoing to see
various messages that are available.