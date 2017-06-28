using System;
using System.Net.Sockets;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace TcpAsync
{
    public class TcpClientAsync : ITcpClientAsync
    {
        private TcpClient client = null;
        private string returndata = String.Empty;
        public bool connectStatus = false;
        public bool receiveStatus = false;
        public bool sendStatus = false;
        public const int BufferSize = 256;
        private byte[] buffer = new byte[BufferSize];
        private string sendMsg = String.Empty;
        private int port;
        private string ip;

        // ActionCallback allow you to asign an Action with paramaters which is automatically call after connect, receive or send
        public class ActionCallback<Tparams>
        {
            Action<Tparams> action;
            Tparams parameters;

            public ActionCallback(Action<Tparams> act, Tparams pa)
            {
                action = act;
                parameters = pa;
            }

            public void exec()
            {
                action(parameters);
            }
        }

        // Constructor
        public TcpClientAsync()
        {
            client = new TcpClient();
        }

        // Constructor with automatic connect
        public TcpClientAsync(string _ip, int _port)
        {
            client = new TcpClient();
            Connect(_ip, _port);
        }

        // Call Action without paramaters
        private void CallNullParam(Action act)
        {
            act();
        }

        #region Connect Functions
        public void Connect(string _ip, int _port)
        {
            Connect<object>(_ip, _port, null, null);
        }

        public void Connect(string _ip, int _port, Action act)
        {
            Connect<Action>(_ip, _port, CallNullParam, act);
        }

        public void Connect<Tparams>(string _ip, int _port, Action<Tparams> act, Tparams parameters)
        {
            try
            {
                ip = _ip;
                port = _port;
                connectStatus = false;
                if (client.Connected)
                    Disconnect();
                ActionCallback<Tparams> action = null;
                if (act != null)
                    action = new ActionCallback<Tparams>(act, parameters);
                client.Client.BeginConnect(ip, port, new AsyncCallback(ConnectCallback<Tparams>), action);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ConnectCallback<Tparams>(IAsyncResult ar)
        {
            ActionCallback<Tparams> action = (ActionCallback<Tparams>)ar.AsyncState;
            client.EndConnect(ar);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            if (action != null)
                action.exec();
            connectStatus = true;
        }
        #endregion

        #region Receive Functions
        

        public void Receive()
        {
            Receive<object>(null, null);
        }

        public void Receive(Action act)
        {
            Receive<Action>(CallNullParam, act);
        }

        public void Receive<Tparams>(Action<Tparams> act, Tparams parameters)
        {
            try
            {
                if (client.Connected)
                {
                    returndata = String.Empty;
                    receiveStatus = false;
                    buffer = new byte[BufferSize];
                    ActionCallback<Tparams> action = null;
                    if (act != null)
                        action  = new ActionCallback<Tparams>(act, parameters);
                    client.Client.BeginReceive(buffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback<Tparams>), action);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback<Tparams>(IAsyncResult ar)
        {
            int bytesRead = client.GetStream().EndRead(ar);
            ActionCallback<Tparams> action = (ActionCallback<Tparams>)ar.AsyncState;
            if (bytesRead > 0)
            {
                returndata += Encoding.ASCII.GetString(buffer, 0, bytesRead);
                buffer = new byte[BufferSize];
                if (client.Client.Poll(0, SelectMode.SelectRead) || returndata[returndata.Length - 1] != '\n')
                    client.Client.BeginReceive(buffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback<Tparams>), action);
                else
                {
                    if (action != null)
                        action.exec();
                    receiveStatus = true;
                }
            }
            else
                connectStatus = false;
                
        }
        #endregion

        #region Send Functions
        public void Send(string data)
        {
            Send<object>(data, null, null);
        }

        public void Send(string data, Action act)
        {
            Send<Action>(data, CallNullParam, act);
        }

        public void Send<Tparams>(string data, Action<Tparams> act, Tparams parameters)
        {
            if (client.Connected)
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                sendStatus = false;
                ActionCallback<Tparams> action = null;
                if (act != null)
                    action = new ActionCallback<Tparams>(act, parameters);
                client.Client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback<Tparams>), action);
                sendMsg = data;
            }
        }

        private void SendCallback<Tparams>(IAsyncResult ar)
        {
            try
            {
                ActionCallback<Tparams> action = (ActionCallback<Tparams>)ar.AsyncState;
                if (action != null)
                    action.exec();
                sendStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

        public void Disconnect()
        {
            if (client.Connected)
            {
                client.Client.Disconnect(true);
                client.Close();
                connectStatus = false;
                receiveStatus = false;
                sendStatus = false;
                client = new TcpClient();
            }
        }

        #region Getter
        // Get Receive Data
        public string getData()
        {
            return returndata;
        }

        // Get Send Data
        public string getSend()
        {
            return sendMsg;
        }

        //Get socket Ip
        public string IP()
        {
            return ip;
        }

        // Get socket port
        public int Port()
        {
            return port;
        }

        public Socket getSocket()
        {
            return client.Client;
        }
        #endregion
    }
}
