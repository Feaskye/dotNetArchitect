using System;
using System.Collections.Generic;
using System.Text;
using System;
using DotNetty.Transport.Channels;

namespace Discard.Server
{
    public class DiscardServerHandler : SimpleChannelInboundHandler<object>
    {
        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
            ctx.CloseAsync();
        }
    }
}
