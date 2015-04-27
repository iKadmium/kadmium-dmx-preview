﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.IO;

public class SACNPacket : IDMXPacket
{
	//root layer
	public UInt16 PreambleSize {get;set;}
	public UInt16 PostambleSize {get;set;}
	public UInt16 Flags1 {get;set;}
	public string PacketIdentifier {get;set;}
	public UInt32 ProtocolVector {get;set;}
	public byte[] CID;

	//framing layer
	public UInt16 Flags2 {get;set;}
	public UInt32 FramingVector {get;set;}
	public string SourceName {get;set;}
	public byte Priority {get;set;}
	public UInt16 Reserved {get;set;}
	public byte SequenceNumber {get;set;}
	public byte Options {get;set;}
	public UInt16 Universe {get;set;}

	//dmp layer
	public UInt16 Flags3 {get;set;}
	public byte DMPVector {get;set;}
	public byte AddressTypeAndDataType {get;set;}
	public UInt16 FirstPropertyAddress {get;set;}
	public UInt16 AddressIncrement {get;set;}
	public UInt16 PropertyValueCount {get;set;}
	public byte StartCode {get;set;}
	public byte[] Data {get;set;}
	
	public SACNPacket (byte[] data)
	{
		MemoryStream stream = new MemoryStream(data, false);
		BinaryReader reader = new BinaryReader(stream);

		//root layer
		PreambleSize = reader.ReadUInt16();
		PostambleSize = reader.ReadUInt16();
		PacketIdentifier = new string(reader.ReadChars(12));
		Flags1 = reader.ReadUInt16();
		ProtocolVector = reader.ReadUInt32();
		CID = reader.ReadBytes(16);

		//framing layer
		Flags2 = reader.ReadUInt16();
		FramingVector = reader.ReadUInt32();
		SourceName = new string(reader.ReadChars(64));
		Priority = reader.ReadByte();
		Reserved = reader.ReadUInt16();
		SequenceNumber = reader.ReadByte();
		Options = reader.ReadByte();
		Universe = reader.ReadUInt16();

		//dmp layer
		Flags3 = reader.ReadUInt16();
		DMPVector = reader.ReadByte();
		AddressTypeAndDataType = reader.ReadByte();
		FirstPropertyAddress = reader.ReadUInt16();
		AddressIncrement = reader.ReadUInt16();
		PropertyValueCount = reader.ReadUInt16();
		StartCode = reader.ReadByte();
		
		Data = reader.ReadBytes(PropertyValueCount - 1);
	}
}

