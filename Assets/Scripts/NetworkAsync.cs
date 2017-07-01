using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

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
        Init();
        Connect(ip, port);
    }

    public override void OnConnect(params object[] p)
    {
        print(ip + ":" + port);
        homeUI.SetActive(false);
        inventoryUI.SetActive(true);
        print(inventoryUI.activeSelf);
        Send("GRAPHIC\n");
    }

    public override void OnDisconnect(params object[] p)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void OnError(params object[] p)
    {
        foreach (object o in p)
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
        foreach (var o in p)
        {
            Debug.Log("Send : " + o.ToString());
        }
    }
}

