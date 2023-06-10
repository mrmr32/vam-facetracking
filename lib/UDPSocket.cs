// https://gist.github.com/louis-e/888d5031190408775ad130dde353e0fd

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FacialTrackerVamPlugin
{
    public class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        public Action<string> ReceiveCallbackAction = null;
        private IAsyncResult ar;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(string address, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();
            //_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            //_socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
           // Receive();
        }

        //public void Client(string address, int port)
        // {
       // _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //_socket.Connect(IPAddress.Parse(address), port);
       //     Receive();

        //     _socket.Connect(IPAddress.Parse(address), port);
        //     Receive();
        //}

        //public void Send(string text)
        //{
        //    byte[] data = Encoding.ASCII.GetBytes(text);
        //    _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
        //    {
        //        State so = (State)ar.AsyncState;
        //        int bytes = _socket.EndSend(ar);
        //        //Console.WriteLine("SEND: {0}, {1}", bytes, text);
        //        SuperController.LogMessage($"SEND: {bytes}, {text}");
        //     }, state);
        //}

        private void Receive()
        {

            ar = _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                try
                {
                    State so = (State)ar.AsyncState;

                    int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                    _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                    //Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
                    //SuperController.LogMessage($"RECV: {epFrom.ToString()}: {bytes}, {Encoding.ASCII.GetString(so.buffer, 0, bytes)}");

                    if (ReceiveCallbackAction != null)
                    {
                        ReceiveCallbackAction(Encoding.ASCII.GetString(so.buffer, 0, bytes));
                    }
                }
                catch(Exception e)
                {
                    if(e.GetType().ToString() != "System.ObjectDisposedException")
                    {
                        SuperController.LogError($"Socket receive error: {e}");
                        Disconnect();
                        throw new Exception($"Socket receive error: {e}");
                    }
                    
                }
                

            }, state);

        }

        // this simply isn't working
        public void Disconnect()
        {

            _socket.Close();

            //_socket = (Socket)ar.AsyncState;
            //_socket.EndReceiveFrom(ar, ref epFrom);
            //SuperController.LogMessage($"called EndReceiveFrom");
            // _socket.EndReceive(ar);
            

        }
    }
}