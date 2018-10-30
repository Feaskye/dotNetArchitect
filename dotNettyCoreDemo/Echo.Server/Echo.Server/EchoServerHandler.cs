using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Server
{
    /// <summary>
    /// 服务端处理事件函数
    /// </summary>
    public class EchoServerHandler : ChannelHandlerAdapter // ChannelHandlerAdapter 业务继承基类适配器 // (1)
    {
        /// <summary>
        /// 管道开始读
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public override void ChannelRead(IChannelHandlerContext context, object message) // (2)
        {
            if (message is IByteBuffer buffer)    // (3)
            {
                Console.WriteLine("Received from client: " + buffer.ToString(Encoding.UTF8));
            }

            context.WriteAsync(message); // (4)
        }

        /// <summary>
        /// 管道读取完成
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush(); // (5)

        /// <summary>
        /// 出现异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}
/*
 DiscardServerHandler 继承自 ChannelInboundHandlerAdapter，这个类实现了IChannelHandler接口，IChannelHandler提供了许多事件处理的接口方法，
 然后你可以覆盖这些方法。现在仅仅只需要继承 ChannelInboundHandlerAdapter 类而不是你自己去实现接口方法。

这里我们覆盖了 chanelRead() 事件处理方法。每当从客户端收到新的数据时，这个方法会在收到消息时被调用，这个例子中，收到的消息的类型是 ByteBuf。

为了响应或显示客户端发来的信息，为此，我们将在控制台中打印出客户端传来的数据。

然后，我们将客户端传来的消息通过context.WriteAsync写回到客户端。

当然，步骤4只是将流缓存到上下文中，并没执行真正的写入操作，通过执行Flush将流数据写入管道，并通过context传回给传来的客户端。

     */
