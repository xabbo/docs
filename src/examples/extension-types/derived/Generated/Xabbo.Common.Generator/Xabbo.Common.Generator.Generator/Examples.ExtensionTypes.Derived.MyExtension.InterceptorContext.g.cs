namespace Examples.ExtensionTypes.Derived
{
    partial class MyExtension
    {
        // Generating 1 send header method(s)
        
        protected void Send<T1, T2>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2){
            global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
            using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
            packet.Write<T1>(arg1);
            packet.Write<T2>(arg2);
            context.Interceptor.Send(packet);
        }
        // Generating 1 send identifier method(s)
        
        protected void Send<T1, T2>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2){
            global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
            global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
            using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
            packet.Write<T1>(arg1);
            packet.Write<T2>(arg2);
            context.Interceptor.Send(packet);
        }
    }
}
