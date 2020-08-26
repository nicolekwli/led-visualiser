using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

// Used code from https://www.instagram.com/p/B77X5hInP5d/ https://unitylist.com/p/uzl/Unity-Art-Net-Demo


public class ArtNetNode : MonoBehaviour
{
    const int artNetMaxPacketSize = 530; // TODO:should this be 512 really??

    Thread rxThread;
    int artNetPort=6454;
    UdpClient artNetClient;
    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
    artNetPacket artNetData = new artNetPacket();
    int frameRate = 25;

	// Array containg each LED device attached
    private List<Led> attached = new List<Led>(); //NOTE: I don't know how to use lists<> in C# :p, please correct

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;

        artNetClient = new UdpClient(artNetPort);
        rxThread = new Thread(new ThreadStart(pollArtNet));
        rxThread.IsBackground = true;
        rxThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(artNetData.hasChanged==1){
            // TODO:stuff?
            artNetData.hasChanged=0;
            //Debug.Log(artNetData.data[13]);
        }

    }

    void pollArtNet(){
        while (true) {
            try {
                byte[] data = artNetClient.Receive(ref RemoteIpEndPoint);
                if (data.Length == artNetMaxPacketSize) {
                    //Debug.Log(data[31]);
                    artNetData.parseArtNetPacket(data);
					//pushToFixtures( // TODO );
                }
            } catch (Exception err) {
                Debug.Log(err.ToString());
            }
        }
    }

	private void printDataHead(){
		// TODO print the first few DMX addresses' DMX values.
	}

    public void plugInFixture(Led fixture){
        attached.Add(fixture);
    }

	public void sayHello(){
		Debug.Log("Hello!");
	}

    void pushToFixtures(){
        // TODO
    }

}






// copied directly
public class artNetPacket
{
	public byte[] header;       			//0-6
    public byte[] opcode;             		//8-9
    public byte[] protocolVersion;    		//10-11
    public byte sequence;              		//12
    public byte physical;              		//13
    public byte[] universe;           		//14-15
    public byte[] dataLength;         		//16-17
    public byte[] data;                 	//18-530
    public byte hasChanged;
    
    public int pCnt;
    public int pIndex;
	
	public artNetPacket()
	{
		header = new byte[7];
		opcode = new byte[2];
		protocolVersion = new byte[2];
		sequence = 0;
		physical = 0;
		universe = new byte[2];
		dataLength = new byte[2];
		data = new byte[512];
		hasChanged = 0;
		pCnt = 0;
		pIndex = 0;
	}
	
	public void parseArtNetPacket(byte[] packetBuffer)
	{
	  //header
	  pIndex=0;
	  for(pCnt=0; pCnt<7; pCnt++)
	  {
		header[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //opcode
	  pIndex++;
	  for(pCnt=0; pCnt<2; pCnt++)
	  {
		opcode[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //Protocol Version
	  for(pCnt=0; pCnt<2; pCnt++)
	  {
		protocolVersion[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //sequence
	  sequence = packetBuffer[pIndex];
	  pIndex++;
	  //physical
	  physical = packetBuffer[pIndex];
	  pIndex++;
	  //universe
	  for(pCnt=0; pCnt<2; pCnt++)
	  {
		universe[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //datalengsth
	  for(pCnt=0; pCnt<2; pCnt++)
	  {
		dataLength[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //data
	  for(pCnt=0; pCnt<512; pCnt++)
	  {
		if(data[pCnt]!=packetBuffer[pIndex] && hasChanged==0)
		{
		  hasChanged=1;
		}
		data[pCnt] = packetBuffer[pIndex];
		pIndex++;
	  }
	  //check for blank
	  pIndex=0;
	  for(pCnt=0; pCnt<512; pCnt++)
	  {
		pIndex+=data[pCnt];
	  }
	  if(pIndex==0 && hasChanged==0)
	  {
		hasChanged=1;
	  }
	}
}
