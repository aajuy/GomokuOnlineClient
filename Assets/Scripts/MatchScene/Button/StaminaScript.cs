using MatchServer.Web.Data.DTOs.Client;
using System;
using System.Collections;
using System.Text.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class StaminaScript : MonoBehaviour
{
    public TMP_Text staminaText;
    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetStamina());
    }

    IEnumerator GetStamina()
    {
        int userId = MyInfo.Instance.UserId;
        string uri = "http://" + ServerConfig.MatchServerAddress + $"/match/stamina/{userId}";
        using (var www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                GetStaminaResponseDto getStaminaResponseDto = JsonSerializer.Deserialize<GetStaminaResponseDto>(www.downloadHandler.text, options);

                MyInfo.Instance.LastStaminaUpdateTime = getStaminaResponseDto.LastStaminaUpdateTime;
                MyInfo.Instance.Stamina = getStaminaResponseDto.Stamina;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = (int)(DateTime.UtcNow - MyInfo.Instance.LastStaminaUpdateTime).TotalSeconds;
        int currentStamina = Math.Min(120, MyInfo.Instance.Stamina + (seconds / 360));
        staminaText.text = $"Stamina : {currentStamina}";

        int leftSecondsToIncrease = 360 - (seconds % 360);
        int minute = leftSecondsToIncrease / 60;
        int second = leftSecondsToIncrease % 60;
        if (currentStamina < 120)
        {
            timerText.text = $"{minute}:{second}";
        }
        else
        {
            timerText.text = $"Full";
        }
    }
}
