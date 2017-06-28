using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpAsync
{
    public interface ITcpClientAsync
    {
        void Connect(string _ip, int _port);

        void Connect(string _ip, int _port, Action act);

        void Connect<Tparams>(string _ip, int _port, Action<Tparams> act, Tparams parameters);

        void ConnectCallback<Tparams>(IAsyncResult ar);

        void Disconnect();

        void Receive();

        void Receive(Action act);

        void Receive<Tparams>(Action<Tparams> act, Tparams parameters);

        void Send(string data);

        void Send(string data, Action act);

        void Send<Tparams>(string data, Action<Tparams> act, Tparams parameters);

        string getData();
    }
}
