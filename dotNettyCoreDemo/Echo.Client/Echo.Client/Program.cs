using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Examples.Common;

namespace Echo.Client
{
    class Program
    {
        static async Task RunClientAsync()
        {
            ExampleHelper.SetConsoleLogger();

            var group = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(
                        new ActionChannelInitializer<ISocketChannel>(
                            channel =>
                            {
                                IChannelPipeline pipeline = channel.Pipeline;
                                pipeline.AddLast(new LoggingHandler());
                                pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                                pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                                pipeline.AddLast("echo", new EchoClientHandler());
                            }));

                IChannel clientChannel = await bootstrap.ConnectAsync(new IPEndPoint(ClientSettings.Host, ClientSettings.Port));

                // 建立死循环，类同于While(true)
                for (; ; ) // (4)
                {
                    Console.WriteLine("input you data:");
                    // 根据设置建立缓存区大小
                    IByteBuffer initialMessage = Unpooled.Buffer(ClientSettings.Size); // （1）
                    string r = Console.ReadLine();
                    // 将数据流写入缓冲区
                    initialMessage.WriteBytes(Encoding.UTF8.GetBytes(r ?? throw new InvalidOperationException())); // (2)
                    // 将缓冲区数据流写入到管道中
                    await clientChannel.WriteAndFlushAsync(initialMessage); // (3)
                    if (r.Contains("bye"))
                        break;
                }

                Console.WriteLine("byebye");


                await clientChannel.CloseAsync();
            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }

        static void Main() => RunClientAsync().Wait();
    }
}
/*
 初始化一个缓冲区的大小。
默认缓冲区接受的数据类型为bytes[]，当然这样也更加便于序列化成流。
将缓冲区的流直接数据写入到Channel管道中。该管道一般为链接通讯的另一端（C端）。
建立死循环，这样做的目的是为了测试每次都必须从客户端输入的数据，通过服务端回路一次后，再进行下一次的输入操作。
     */
