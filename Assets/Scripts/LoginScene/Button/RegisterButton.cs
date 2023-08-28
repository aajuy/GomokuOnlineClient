using Assets.Scripts.LoginScene.Data.DTOs.LoginServer;
using System.Collections;
using System.Net;
using System.Text.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterButton : MonoBehaviour
{
    public TMP_InputField UsernameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_Text ResponseText;
    string baseUri = ServerConfig.LoginServerAddress;  // TODO

    public void OnClick()
    {
        RegisterRequestDto registerRequestDto = new RegisterRequestDto()
        {
            Username = UsernameInputField.text,
            Password = PasswordInputField.text
        };
        StartCoroutine(Register(registerRequestDto));
    }

    // TODO: 하드 코딩 제거
    IEnumerator Register(RegisterRequestDto registerRequestDto)
    {
        string uri = baseUri + "/register";
        string jsonString = JsonSerializer.Serialize(registerRequestDto);

        using (var www = UnityWebRequest.Post(uri, jsonString, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.error != null)
            {
                if (www.responseCode == (long)HttpStatusCode.Conflict)
                {
                    ResponseText.text = "Duplicate username";
                }
                else
                {
                    ResponseText.text = "Length must be between 6 and 12";
                }
            }
            else
            {
                ResponseText.text = "Registration completed!";
            }
        }
    }
}
