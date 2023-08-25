using Assets.Scripts.MatchScene;
using Assets.Scripts.MatchScene.Data.DTOs;
using System.Collections;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;

public class QuitButton : MonoBehaviour
{
    string baseUri = ServerConfig.LoginServer;  // TODO

    public void OnClick()
    {
        ServerSession session = GameObject.Find("MatchServerNetworkManager")
            .GetComponent<MatchServerNetworkManager>()
            .Session;

        if (session != null)
        {
            session.Disconnect();
        }

        LogoutRequestDto logoutRequestDto = new LogoutRequestDto()
        {
            UserId = MyInfo.Instance.UserId,
            SessionId = MyInfo.Instance.SessionId
        };
        StartCoroutine(Logout(logoutRequestDto));

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator Logout(LogoutRequestDto logoutRequestDto)
    {
        string uri = baseUri + "/logout";
        string jsonString = JsonSerializer.Serialize(logoutRequestDto);

        using (var www = UnityWebRequest.Post(uri, jsonString, "application/json"))
        {
            yield return www.SendWebRequest();
        }
    }
}
