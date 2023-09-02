using Assets.Scripts.GameScene;
using UnityEngine;

public class LeaveButton : MonoBehaviour
{
    public void OnClick()
    {
        ServerSession session = GameObject.Find("GameServerNetworkManager")
            .GetComponent<GameServerNetworkManager>()
            .Session;

        if (session != null)
        {
            session.Disconnect();
        }

        LoadingSceneController.LoadScene("MatchScene");
    }
}
