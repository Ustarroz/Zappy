using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkAsync : UnityTcpClientAsync
{
    public string ip;
    public int port;
    public GameObject homeUI;
    public GameObject inventoryUI;

    public InterpretServerResponse interpretor;

    public void GetPort(string str)
    {
        port = int.Parse(str);
    }

    public void GetIp(string str)
    {
        ip = str;
    }

    public void ConnecteToServer()
    {
        print(ip + ":"+ port);
        Init();
        Connect(ip, port);
    }

    public override void OnConnect(params object[] p)
    {
        homeUI.SetActive(false);
        inventoryUI.SetActive(true);
        Send("GRAPHIC\n");
    }

    public override void OnDisconnect(params object[] p)
    {
        throw new NotImplementedException();
    }

    public override void OnError(params object[] p)
    {
        foreach(object o in p)
        {
            Debug.Log(o.ToString());
        }
    }

    public override void OnReceive(params object[] p)
    {
        foreach (object obj in p)
        {
            print(obj.ToString());
            interpretor.InterpretResponse(obj.ToString());
        }
    }

    public override void OnSend(params object[] p)
    {
    }
}

