using Google.Protobuf;
using Google.Protobuf.MatchProtocol;
using NetworkLibrary;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MatchScene.Packet
{
    internal class PacketHandler
    {
        public static void S_ReadyHandler(PacketSession session, IMessage packet)
        {
            S_Ready sReadyPacket = packet as S_Ready;
            ServerSession serverSession = session as ServerSession;

            MyInfo.Instance.RoomId = sReadyPacket.RoomId;

            session.Disconnect();
            SceneManager.LoadScene("GameScene");
        }
    }
}
