using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketTest : MonoBehaviour {

	public static void StartClient() {
		// Data buffer for incoming data.
		byte[] bytes = new byte[1024];

		// Connect to a remote device.
		try {
			// Establish the remote endpoint for the socket.
			// This example uses port 11000 on the local computer.
			//IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
			IPEndPoint remoteEP = new IPEndPoint(ipAddress,11000);

			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Connect the socket to the remote endpoint. Catch any errors.
			try {
				sender.Connect(remoteEP);

				Debug.Log("Socket connected to " +
					sender.RemoteEndPoint.ToString());

				// Encode the data string into a byte array.
				byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

				// Send the data through the socket.
				int bytesSent = sender.Send(msg);

				// Receive the response from the remote device.
				int bytesRec = sender.Receive(bytes);
                Debug.Log("Echoed test = {0}" + Encoding.ASCII.GetString(bytes,0,bytesRec));

				// Release the socket.
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();

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

	public static int Main(String[] args) {
		StartClient();
		return 0;
	}
	// Use this for initialization
	void Start () {
		StartClient ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
