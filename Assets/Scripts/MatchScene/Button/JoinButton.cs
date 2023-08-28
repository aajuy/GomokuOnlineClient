using Assets.Scripts.MatchScene;
using Google.Protobuf.MatchProtocol;
using TMPro;
using UnityEngine;

public class MatchButton : MonoBehaviour
{
    //bool waiting = false;
    public TMP_Text currentState;
    public TMP_Text buttonText;
    public MatchServerNetworkManager networkManager;

    public void OnClick()
    {
        if (MyInfo.Instance.Waiting)
        {
            Cancel();
            //waiting = false;
            MyInfo.Instance.Waiting = false;
            currentState.text = string.Empty;
            buttonText.text = "Join Match";
        }
        else
        {
            //waiting = true;
            //currentState.text = "Waiting...";
            //buttonText.text = "Cancel";
            Join();
        }
    }

    void Join()
    {
        ServerSession session = networkManager.Session;

        C_Join cJoinPacket = new C_Join();

        session.Send(cJoinPacket);
    }

    void Cancel()
    {
        ServerSession session = networkManager.Session;

        C_Cancel cCancelPacket = new C_Cancel();

        session.Send(cCancelPacket);
    }
}
