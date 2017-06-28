using UnityEngine;
using TcpAsync;
using System;

public abstract class UnityTcpClientAsync : MonoBehaviour
{
    protected TcpClientAsync tcpClient = null;
    private bool run = false;
    private float timer = 0;
    private bool start = false;
    private float timeout;

    #region Unity
    void Awake()
    {
        
    }

    protected void Init(float t = 3)
    {
        tcpClient = new TcpClientAsync();
        timeout = t;
    }

    void Update()
    {
        if (run)
        {
            if (tcpClient.receiveStatus)
            {
                tcpClient.receiveStatus = false;
                OnReceive(tcpClient.getData());
                tcpClient.Receive();
            }
            if (tcpClient.sendStatus)
            {
                tcpClient.sendStatus = false;
                OnSend(tcpClient.getSend());
            }
            if (!tcpClient.connectStatus)
                OnDisconnect();
        }
        else if (!run && tcpClient != null && tcpClient.connectStatus)
        {
            run = true;
            tcpClient.Receive();
            OnConnect();
        }
        else if (!run && tcpClient != null && !tcpClient.connectStatus && start)
        {
            timer += Time.deltaTime;
            if (timer > timeout)
            {
                timer = 0;
                start = false;
                OnError();
            }
        }
    }

    #endregion

    #region TcpAsync
    public void Connect(string ip, int port)
    {
        if (!start)
        {
            tcpClient.Connect(ip, port);
            start = true;
        }
    }

    public void Send(string data)
    {
        tcpClient.Send(data);
    }

    public void Disconnect()
    {
		start = false;
		run = false;
		timer = 0;
		tcpClient.Disconnect ();
    }

    // !! Theses three functions must be implement !!

    // This function is automatically call after a Connect
    abstract public void OnConnect(params object[] p);

    // This function is automatically call after a Receive
    abstract public void OnReceive(params object[] p);

    // This function is automatically call after a Send
    abstract public void OnSend(params object[] p);

    // This function is automatically call after an Error
    abstract public void OnError(params object[] p);

    // This function is automatically call after an Disconnect
    abstract public void OnDisconnect(params object[] p);

    #endregion
}
