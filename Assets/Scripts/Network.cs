using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class Network : MonoBehaviour {

    public String ip = "127.0.0.1";
    public int port = 4242;

    IPAddress ipAddress;
    IPEndPoint remoteEP;
    Socket sender;

	public enum commands
	{
		MAP_SIZE = 0,
		CELL_CONTENT,
		MAP_CONTENT,
		TEAM_NAMES,

		NEW_CONNECTION,
		PLAYER_POS,
		PLAYER_LVL,
		PLAYER_INVENTORY,
		PLAYER_BROADCAST,
		PLAYER_INCANTATION_START,
		PLAYER_INCANTATION_STOP,
		EGG,
		DROP,
		TAKE,
		DEATH,
		EGG_SPAWN,
		NEW_PLAYER,
		EGG_DEATH,
		TIME,
		TIME_CHANGE,
		END_GAME,
		SERVER_MSG,
		WRONG_COMMAND,
		WRONG_PARAM
	};

    void Start()
    {
        StartClient();
        RequestServer("");
        RequestServer("GRAPHIC\n");
    }

    /*public string CheckCommand(int command, string args)
	{
		switch (command) {
		case (int)commands.MAP_SIZE:
			RequestServer ();
			break;
		case commands.MAP_CONTENT:
			RequestServer ();
			break;
		case commands.MAP_FULL_CONTENT:
			RequestServer ();
			break;
		case commands.MAP_FULL_CONTENT:
			RequestServer ();
			break;
		case commands.TEAM_NAMES:
			RequestServer ();
			break;
		case commands.NEW_CONNECTION:
			RequestServer ();
			break;
		case commands.PLAYER_POS:
			RequestServer ();
			break;
		case commands.PLAYER_LVL:
			RequestServer ();
			break;
		case commands.PLAYER_INVENTORY:
			RequestServer ();
			break;
		case commands.PLAYER_BROADCAST:
			RequestServer ();
			break;
		case commands.PLAYER_INCANTATION_START:
			RequestServer ();
			break;
		case commands.PLAYER_INCANTATION_STOP:
			RequestServer ();
			break;
		case commands.EGG:
			RequestServer ();
			break;
		case commands.DROP:
			RequestServer ();
			break;
		case commands.TAKE:
			RequestServer ();
			break;
		case commands.DEATH:
			RequestServer ();
			break;
		case commands.EGG_SPAWN:
			RequestServer ();
			break;
		case commands.NEW_PLAYER:
			RequestServer ();
			break;
		case commands.EGG_DEATH:
			RequestServer ();
			break;
		case commands.TIME:
			RequestServer ();
			break;
		case commands.TIME_CHANGE:
			RequestServer ();
			break;
		case commands.END_GAME:
			RequestServer ();
			break;
		case commands.SERVER_MSG:
			RequestServer ();
			break;
		case commands.WRONG_COMMAND:
			RequestServer ();
			break;
		case commands.WRONG_PARAM:
			RequestServer ();
			break;
		}
	}
*/

    public string RequestServer(string command)
	{
		byte[] bytes = new byte[1024];

		// Connect the socket to the remote endpoint. Catch any errors.
		try {
			// Encode the data string into a byte array.
			byte[] msg = Encoding.ASCII.GetBytes(command);

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
			Debug.Log("Send\n");

			// Receive the response from the remote device.
			int bytesRec = sender.Receive(bytes);
			Debug.Log("Receive " +  Encoding.ASCII.GetString(bytes, 0, bytesRec) + "\n");

			Debug.Log("Echoed test = " +
				Encoding.ASCII.GetString(bytes,0,bytesRec));

			return (Encoding.ASCII.GetString(bytes,0,bytesRec) + "\n");	
		} catch (ArgumentNullException ane) {
			Debug.Log("ArgumentNullException : " + ane.ToString());
		} catch (SocketException se) {
			Debug.Log("SocketException : " + se.ToString());
		} catch (Exception e) {
			Debug.Log("Unexpected exception : " + e.ToString());
		}
		return ("error");
	}

	public void StartClient() 
	{
		// Data buffer for incoming data.
		byte[] bytes = new byte[1024];

		// Connect to a remote device.
		try {
            // Establish the remote endpoint for the socket.
            // This example uses port 4242 on the local computer.
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = Dns.GetHostEntry(ip).AddressList[0];
            remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP  socket.
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Connect the socket to the remote endpoint. Catch any errors.
			try {
				sender.Connect(remoteEP);
				Debug.Log(sender.RemoteEndPoint.ToString() + "\n");
					sender.RemoteEndPoint.ToString();
				
				// Encode the data string into a byte array.
				byte[] msg = Encoding.ASCII.GetBytes("This is a test\n");

				// Send the data through the socket.
				int bytesSent = sender.Send(msg);
					
				// Receive the response from the remote device.
				int bytesRec = sender.Receive(bytes);
			
				//sender.Send("GRAPHIC\n");
			} catch (ArgumentNullException ane) {
				Debug.Log("ArgumentNullException : " + ane.ToString());
			} catch (SocketException se) {
				Debug.Log("SocketException : " + se.ToString());
			} catch (Exception e) {
				Debug.Log("Unexpected exception : " + e.ToString());
			}

		} catch (Exception e) {
			Debug.Log(e.ToString());
		}
	}

	public void CloseClient()
	{
		sender.Shutdown(SocketShutdown.Both);
		sender.Close();
	}
	
}
