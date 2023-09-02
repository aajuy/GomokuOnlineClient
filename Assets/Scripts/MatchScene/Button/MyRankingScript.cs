using System.Collections;
using System.Text.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RankScript : MonoBehaviour
{
    public TMP_Text rankText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRank());
    }

    IEnumerator GetRank()
    {
        int userId = MyInfo.Instance.UserId;
        string uri = "http://" + ServerConfig.MatchServerAddress + $"/ranking/{userId}";
        using (var www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                int ranking = JsonSerializer.Deserialize<int>(www.downloadHandler.text, options);
                if (ranking == -1)
                {
                    rankText.text = "Ranking: -";
                }
                else
                {
                    rankText.text = $"Ranking: {ranking}";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
