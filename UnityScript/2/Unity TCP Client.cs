using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class Client2 : MonoBehaviour
{
    private NetworkStream stream;
    private TcpClient client;
    private string responseData = "";  //string.Empty

    public InputField msgInput;
    public Text msgText;
    Thread thread;
    string nickname="";

    Dictionary<int, string> funcMsg = new Dictionary<int, string>();
    
    private void Start()
    {
        DicInit();
        client = new TcpClient("127.0.0.1", 12000);
        stream = client.GetStream();
        thread = new Thread(new ThreadStart(ReceiveThread));
        thread.Start();
        SendMsg("누군가가 연결하였습니다.");
    }

    void DicInit()
    {
        funcMsg.Add(1, "%DISCNT-QUITCNTTU0|");
    }

    private void Update()
    {
        if (client.Client.Connected)
        {
            HandleData();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    public void NicknameSet(InputField nick)
    {
        nickname = nick.text;
    }

    private void ReceiveThread()
    {
        while(true)
        {
            byte[] receiveData = new byte[256];
            int bytes = stream.Read(receiveData, 0, receiveData.Length);
            responseData = Encoding.UTF8.GetString(receiveData, 0, bytes);
        }
    }

    void HandleData()
    {
        if(responseData!="")
        {
            if(responseData.Contains("%DISCNT-QUITCNTTU0"))
            {
                responseData = "";
                return;
            }
            msgText.text += "\n" + responseData;
            responseData = "";
        }
    }
    public void Send()
    {
        string sendMsg = nickname+": "+msgInput.text;
        msgInput.text = "";
        byte[] sendData = Encoding.UTF8.GetBytes(sendMsg);
        stream.Write(sendData, 0, sendData.Length);
        stream.Flush();
    }

    public void SendMsg(string msg)
    {
        byte[] sendData = Encoding.UTF8.GetBytes(msg);
        stream.Write(sendData, 0, sendData.Length);
        stream.Flush();
    }
    
    public void SendScMsg(int mKey)
    {
        byte[] sendData = Encoding.UTF8.GetBytes(funcMsg[mKey]);
        stream.Write(sendData, 0, sendData.Length);
        stream.Flush();
    }

    private void OnApplicationQuit()
    {
        funcMsg[1] = "%DISCNT-QUITCNTTU0|" + nickname;
        SendScMsg(1);
        stream.Close();
        client.Close();
        stream = null;
        client = null;
    }
}
