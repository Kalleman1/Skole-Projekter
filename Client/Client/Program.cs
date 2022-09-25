// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

ExecuteClient();

static void ExecuteClient()
{
    try
    {
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

        Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            sender.Connect(localEndPoint);

            Console.WriteLine("Socket connected to -> {0}", sender.RemoteEndPoint.ToString());

            byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
            int byteSent = sender.Send(messageSent);
            byte[] messageRecieved = new byte[1024];
            int byteRecv = sender.Receive(messageRecieved);
            Console.WriteLine("Message from server -> {0}", Encoding.ASCII.GetString(messageRecieved, 0, byteRecv));

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (ArgumentException ane)
        {
            Console.WriteLine("ArgumentNullException : {0}", ane.ToString);
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException : {0}", se.ToString);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception : {0}", e.ToString());
        }


    }
    catch (Exception e)
    {

        Console.WriteLine(e.ToString());
    }
}