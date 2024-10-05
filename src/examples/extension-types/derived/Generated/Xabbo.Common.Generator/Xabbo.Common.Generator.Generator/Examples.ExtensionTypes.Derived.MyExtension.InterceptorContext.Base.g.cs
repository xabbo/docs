#nullable enable

namespace Examples.ExtensionTypes.Derived
{
    partial class MyExtension
    {
        
        /// <inheritdoc cref="global::Xabbo.ConnectionExtensions.Send(
        ///     global::Xabbo.Interceptor.IInterceptor,
        ///     global::Xabbo.Messages.IMessage
        /// )" />
        protected void Send(global::Xabbo.Messages.IMessage message)
            => global::Xabbo.ConnectionExtensions.Send(((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor, message);

        /// <inheritdoc cref="global::Xabbo.InterceptorExtensions.ReceiveAsync(
        ///     global::Xabbo.Interceptor.IInterceptor,
        ///     global::System.ReadOnlySpan{global::Xabbo.Messages.Header},
        ///     int, bool,
        ///     global::System.Func{global::Xabbo.Messages.IPacket, bool}?,
        ///     global::System.Threading.CancellationToken
        /// )" />
        protected global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Header> headers,
            int? timeout = null, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            headers, timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref="global::Xabbo.InterceptorExtensions.ReceiveAsync(
        ///     global::Xabbo.Interceptor.IInterceptor,
        ///     global::System.ReadOnlySpan{global::Xabbo.Messages.Identifier},
        ///     int, bool,
        ///     global::System.Func{global::Xabbo.Messages.IPacket, bool}?,
        ///     global::System.Threading.CancellationToken
        /// )" />
        protected global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier> identifiers,
            int? timeout = null, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            identifiers, timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref="global::Xabbo.InterceptorExtensions.ReceiveAsync{TMsg}(
        ///     global::Xabbo.Interceptor.IInterceptor,
        ///     int, bool,
        ///     global::System.Func{TMsg, bool}?,
        ///     global::System.Threading.CancellationToken
        /// )" />
        protected global::System.Threading.Tasks.Task<TMsg> ReceiveAsync<TMsg>(
            int? timeout = null, bool block = false,
            global::System.Func<TMsg, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) where TMsg : global::Xabbo.Messages.IMessage<TMsg> => global::Xabbo.InterceptorExtensions.ReceiveAsync<TMsg>(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref="global::Xabbo.InterceptorExtensions.RequestAsync{TReq, TRes, TData}(
        ///     global::Xabbo.Interceptor.IInterceptor,
        ///     global::Xabbo.Messages.IRequestMessage{TReq, TRes, TData},
        ///     int,
        ///     global::System.Threading.CancellationToken
        /// )" />
        protected async global::System.Threading.Tasks.Task<TData> RequestAsync<TReq, TRes, TData>(
            global::Xabbo.Messages.IRequestMessage<TReq, TRes, TData> request,
            int? timeout = null, global::System.Threading.CancellationToken cancellationToken = default
        )
            where TReq : global::Xabbo.Messages.IRequestMessage<TReq, TRes, TData>
            where TRes : global::Xabbo.Messages.IMessage<TRes>
        {
            global::Xabbo.Interceptor.IInterceptor interceptor = ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor;
            global::System.Threading.Tasks.Task<TRes> response = global::Xabbo.InterceptorExtensions.ReceiveAsync<TRes>(
                interceptor,
                timeout: timeout,
                block: true,
                shouldCapture: request.MatchResponse,
                cancellationToken: cancellationToken
            );
            global::Xabbo.ConnectionExtensions.Send(interceptor, request);

            return request.GetData(await response);
        }
    }
}
