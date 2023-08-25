using Assets.Scripts.GameScene;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        SceneManager.LoadScene("MatchScene");
    }
}
