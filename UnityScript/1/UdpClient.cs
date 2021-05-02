using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class Client3 : MonoBehaviour
{
    private string nickname = "";
    public Text chatTxt;
    public InputField chatInput;
    private string receivedMsg = "";
    public UIManager uiManager;

    [SerializeField] string strIP = "127.0.0.1";
    [SerializeField] string broadcastIP = "230.185.192.108";
    [SerializeField] string multicastIP = "229.1.1.229";
    [SerializeField] int port = 13000;
    private byte[] rB = new byte[10000];  // 버퍼의 크기 필요한 만큼 크기를 정하도록 하자 작을수록 좋음
    Thread ServerCheck_thread;  // 서버에서 보내는 패킷을 체크하기 위한 스레드
    Queue<string> netBuffer = new Queue<string>();  // 버퍼를 저장하기 위한 큐
    private Socket client;  // 소켓(클라)
    private IPAddress ip;
    private IPEndPoint endPoint;

    object buffer_lock = new object();  //Queue 처리 충돌 방지용 lock
    #region 서버코드
    private readonly string connectCode = "%FirstConnectToServerCHECKTPU5570001";
    private readonly string connectCode2 = "%FirstConnectToServerCHECKTPU55700012";
    private readonly string positionCode = "%PlayerPositionAsyn677RTV_+";
    private readonly string discntCode = "%PlayerDisCnt5Y9O";
    private readonly string othersCode = "%YDOTHER-";
    private readonly string rotCode = "%ROT78YTV";
    private readonly string hpCode = "%HP^677GR";
    #endregion
    public int id;
    private PlayerMove player = null;
    private Dictionary<int, PlayerMove> playerDict = new Dictionary<int, PlayerMove>();
    public GameObject playerPrefab;
    public Text fText;

    private void Start()
    {
        ServerOn();
        StartCoroutine(buffer_updata());  //업데이트에서 안하는 것은 많은 패킷이 오면 렉을 유발해서
    }

    IEnumerator buffer_updata()
    {
        while (true)
        {
            yield return null;
            BufferSystem();
        }
    }

    private void ServerOn()
    {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ip = IPAddress.Parse(strIP);
        endPoint = new IPEndPoint(ip, port);

        IPAddress multicIP = IPAddress.Parse(multicastIP);
        client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicIP, ip));

        client.Connect(endPoint);
        ServerCheck_thread = new Thread(ServerCheck);
        ServerCheck_thread.Start(); // 서버 체크 시스템 시작, 스레드를 사용하지않으면 서버체크 Receive 에서 유니티가 멈춰버림
        ServerSend(connectCode);
    }

    private void ServerCheck()
    {
        while (true)
        {
            try
            {
                client.Receive(rB, 0, rB.Length, SocketFlags.None);  //서버에서 온 패킷을 버퍼에 담기
                string t = Encoding.UTF8.GetString(rB);  //큐에 버퍼를 넣을 준비
                t = t.Replace("\0", string.Empty);  //버퍼 마지막에 공백이 있는지 검사하고 공백 삭제
                lock (buffer_lock)  //queue 충돌 방지
                {
                    netBuffer.Enqueue(t);  //큐에 버퍼 저장
                }
                Array.Clear(rB, 0, rB.Length);  //버퍼를 사용후 초기화
            }
            catch (Exception e)
            {
                chatTxt.text += "\n" + e.Message;
            }
        }
    }

    private void BufferSystem()
    {
        while (netBuffer.Count != 0)
        {
            receivedMsg = netBuffer.Dequeue();
            if (!MsgCodeInspect(receivedMsg))
            {
                chatTxt.text += "\n" + receivedMsg;
                receivedMsg = "";
            }
        }
    }

    private bool MsgCodeInspect(string m = "")
    {
        if (receivedMsg.Contains("|"))
        {
            if (receivedMsg.Split('|')[1] == connectCode)
            {
                chatTxt.text += "\n" + receivedMsg.Split('|')[0];
                id = int.Parse(receivedMsg.Split('|')[2]);
                receivedMsg = "";

                GameObject o = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                PlayerMove p = o.GetComponent<PlayerMove>();
                player = p;
                player.client = this;
                player.id = id;
                playerDict.Add(id, player);

                return true;
            }
            else if (receivedMsg.Split('|')[1] == connectCode2)
            {
                chatTxt.text += "\n" + receivedMsg.Split('|')[0];
                int _id = int.Parse(receivedMsg.Split('|')[2]);
                receivedMsg = "";

                GameObject o = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                PlayerMove p = o.GetComponent<PlayerMove>();
                p.id = _id;
                playerDict.Add(_id, p);

                string msg = $"{othersCode}|{_id}|{player.transform.position.x}|{player.transform.position.y}|{player.transform.position.z}|{id}";
                ServerSend(msg);

                return true;
            }
            else if (m.Split('|')[0] == discntCode)
            {
                int _id = int.Parse(m.Split('|')[1]);
                GameObject o = playerDict[_id].gameObject;
                playerDict.Remove(_id);
                Destroy(o);

                return true;
            }
            else if (m.Split('|')[0] == positionCode)
            {
                if (id == int.Parse(m.Split('|')[4])) return true;

                playerDict[int.Parse(m.Split('|')[4])].transform.position = new Vector3(float.Parse(m.Split('|')[1]), float.Parse(m.Split('|')[2]), float.Parse(m.Split('|')[3]));

                return true;
            }
            else if (m.Split('|')[0] == rotCode)
            {
                if (id == int.Parse(m.Split('|')[4])) return true;

                playerDict[int.Parse(m.Split('|')[4])].transform.rotation = Quaternion.Euler(new Vector3(float.Parse(m.Split('|')[1]), float.Parse(m.Split('|')[2]), float.Parse(m.Split('|')[3])));

                return true;
            }
            else if (m.Split('|')[0] == hpCode)
            {
                if (id == int.Parse(m.Split('|')[2]))
                {
                    player.Damage(int.Parse(m.Split('|')[1]));
                }
                return true;
            }
            else if (m.Split('|')[0] == othersCode)
            {
                string[] strs = m.Split('|');
                if (int.Parse(strs[1]) == id)
                {
                    GameObject o = Instantiate(playerPrefab, new Vector3(float.Parse(strs[2]), float.Parse(strs[3]), float.Parse(strs[4])), Quaternion.identity);
                    PlayerMove p = o.GetComponent<PlayerMove>();
                    p.id = int.Parse(strs[5]);
                    playerDict.Add(p.id, p);
                }
                return true;
            }

            return false;
        }
        return false;
    }

    private void ServerSend(string str)
    {
        byte[] sbuff = Encoding.UTF8.GetBytes(str);
        client.Send(sbuff, 0, sbuff.Length, SocketFlags.None);
    }

    public void Send()
    {
        if (chatInput.text.Trim() == "") return;

        string msg = nickname != "" ? nickname + ": " + chatInput.text : "NULL" + ": " + chatInput.text;
        if (msg.Contains(connectCode) || msg.Contains(positionCode) || msg.Contains(othersCode) || msg.Contains(discntCode))
        {
            chatInput.text = "";
            return;
        }

        ServerSend(msg);
        chatInput.text = "";
    }

    public void PlayerPositionSend(float x, float y, float z)
    {
        string msg = $"{positionCode}|{x}|{y}|{z}|{id}";
        ServerSend(msg);
    }

    public void PlayerRotationSend(float x, float y, float z)
    {
        string msg = $"{rotCode}|{x}|{y}|{z}|{id}";
        ServerSend(msg);
    }

    public void PlayerHpSend(int value, int _id)  //only dec
    {
        string msg = $"{hpCode}|{value}|{_id}";
        ServerSend(msg);
    }

    public void SetNick(InputField inputf) => nickname = inputf.text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            /* if (!chatInput.isFocused) chatInput.Select();
             else
             {
                 if (chatInput.text.Trim() == "") chatInput.Select();
                 else Send();
             }*/
            Send();
        }
        else if (Input.GetKeyDown(KeyCode.R) && !chatInput.isFocused)
        {
            chatTxt.text = "";
        }
    }

    private void OnApplicationQuit()
    {
        ServerSend(discntCode + "|" + id.ToString());
        ServerCheck_thread.Abort();  //스레드 종료
        client.Close();
    }
}
