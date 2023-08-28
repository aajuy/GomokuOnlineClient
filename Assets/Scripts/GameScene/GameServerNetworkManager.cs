using Assets.Scripts.GameScene.Packet;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    internal class GameServerNetworkManager : MonoBehaviour
    {
        public ServerSession Session { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (Session != null)
            {
                Session.Disconnect();
                Session = null;
            }

            //string host = Dns.GetHostName();
            //IPHostEntry ipHost = Dns.GetHostEntry(host);
            //IPAddress iPAddr = ipHost.AddressList[0];
            //IPEndPoint endPoint = new IPEndPoint(iPAddr, 5678);

            IPAddress ipAddr = IPAddress.Parse(ServerConfig.GameServerAddress);
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 5678);

            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);

            Session = new ServerSession();
            Session.Start(socket);
            Session.Authorize();
            Session.EnterRoom();
        }

        // Update is called once per frame
        void Update()
        {
            PacketMessage packetMessage = PacketQueue.Instance.Pop();
            if (packetMessage == null)
            {
                return;
            }

            var action = PacketManager.Instance.GetPacketHandler(packetMessage.packetId, packetMessage.packet);
            if (action == null)
            {
                return;
            }

            action.Invoke(Session, packetMessage.packet);
        }
    }
}
