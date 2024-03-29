﻿using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using kadmium_sacn;

public class SACNListener : MonoBehaviour {

	public int port = 5568;
	public UInt16 universe = 1;
	public bool multicast = true;

	static public byte MULTICAST_BYTE_1 = 239;
	static public byte MULTICAST_BYTE_2 = 255;
	
	private UdpClient client;

	private IPAddress multicastAddress;
	
	Venue control;
	
	byte[] header;
	int dataLength;
	
	// Use this for initialization
	void Start () 
	{
		client = new UdpClient(port);
		if(multicast)
		{
			IPAddress multicastAddress = GetMulticastAddress();
			client.JoinMulticastGroup(multicastAddress);
		}
		control = gameObject.GetComponentInParent<Venue>();
	}

	private IPAddress GetMulticastAddress()
	{
		byte[] universeBytes = BitConverter.GetBytes(universe);
		byte[] multicastBytes = new byte[] {
			MULTICAST_BYTE_1, 
			MULTICAST_BYTE_2, 
			universeBytes[1], 
			universeBytes[0]
		};
		return new IPAddress(multicastBytes);
	}

	void Dispose ()
	{
		if(multicast)
		{
			client.DropMulticastGroup(GetMulticastAddress());
		}
	}
	
	// Update is called once per frame
	void Update () {
		SACNPacket packet = null;
		while(client.Available > 0)
		{
			System.Net.IPEndPoint point = null;
			byte[] packetData = client.Receive(ref point);
            kadmium_sacn.SACNPacket.Parse(packetData);
            SACNPacket newPacket = SACNPacket.Parse(packetData);
			if(packet == null)
			{
				packet = newPacket;
			}
		}
		if(packet != null)
		{
            control.ProcessDMX((short)(packet.UniverseID + 1), packet.Data);
		}
	}
}
