using Google.Protobuf;
using Google.Protobuf.GameProtocol;
using NetworkLibrary;
using UnityEngine;

namespace Assets.Scripts.GameScene.Packet
{
    internal class PacketHandler : MonoBehaviour
    {
        public static void S_StartHandler(PacketSession session, IMessage packet)
        {
            S_Start sStartPacket = packet as S_Start;
            ServerSession serverSession = session as ServerSession;

            MyInfo.Instance.Turn = sStartPacket.Turn;

            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.GameState = 1;
            manager.LastMoveTime = sStartPacket.Time.ToDateTime();
            manager.UpdateTurn(2);
        }

        public static void S_EndHandler(PacketSession session, IMessage packet)
        {
            S_End sEndPacket = packet as S_End;
            ServerSession serverSession = session as ServerSession;

            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.GameState = 0;
            if (sEndPacket.Y != -1)
            {
                manager.PlaceStone(sEndPacket.Y, sEndPacket.X, sEndPacket.Turn);
            }
            manager.End(sEndPacket.Result);
        }

        public static void S_MoveHandler(PacketSession session, IMessage packet)
        {
            S_Move sMovePacket = packet as S_Move;
            ServerSession serverSession = session as ServerSession;

            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.PlaceStone(sMovePacket.Y, sMovePacket.X, sMovePacket.Turn);
            manager.UpdateTurn(sMovePacket.Turn);
            manager.LastMoveTime = sMovePacket.Time.ToDateTime();
        }
    }
}
