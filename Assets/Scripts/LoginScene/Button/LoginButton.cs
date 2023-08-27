using Assets.Scripts.LoginScene.Data.DTOs.LoginServer;
using System.Collections;
using System.Text.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour
{
    public TMP_InputField UsernameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_Text ResponseText;
    string baseUri = ServerConfig.LoginServer;  // TODO

    public void OnClick()
    {
        LoginRequestDto loginRequestDto = new LoginRequestDto()
        {
            Username = UsernameInputField.text,
            Password = PasswordInputField.text
        };
        StartCoroutine(Login(loginRequestDto));
    }

    // TODO: 하드 코딩 제거
    IEnumerator Login(LoginRequestDto loginRequestDto)
    {
        string uri = baseUri + "/login";
        string jsonString = JsonSerializer.Serialize(loginRequestDto);

        using (var www = UnityWebRequest.Post(uri, jsonString, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.error != null)
            {
                ResponseText.text = "Login failed";
            }
            else
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                LoginResponseDto loginResponseDto = JsonSerializer.Deserialize<LoginResponseDto>(www.downloadHandler.text, options);

                MyInfo.Instance.UserId = loginResponseDto.UserId;
                MyInfo.Instance.SessionId = loginResponseDto.SessionId;

                ServerConfig.MatchServer = loginResponseDto.MatchServerAddress;
                ServerConfig.GameServer = loginResponseDto.GameServerAddress;

                SceneManager.LoadScene("MatchScene");
            }
        }
    }
}
