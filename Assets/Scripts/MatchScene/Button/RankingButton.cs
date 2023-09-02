using Assets.Scripts.MatchScene.Data.DTOs;
using DG.Tweening;
using System.Collections;
using System.Text.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RankingButton : MonoBehaviour
{
    public PanelHandler popupWindow;
    public TMP_Text rankingArea;

    public void OnButtonClick()
    {
        StartCoroutine(GetRankings());
    }

    IEnumerator GetRankings()
    {
        string uri = $"http://{ServerConfig.MatchServerAddress}/ranking?from={0}&to={99}";
        using (var www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();


            if (www.error != null)
            {
                ;
            }
            else
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                RankingEntry[] rankingEntries = JsonSerializer.Deserialize<RankingEntry[]>(www.downloadHandler.text, options);

                rankingArea.text = "";
                for (int i = 0; i < rankingEntries.Length; i++)
                {
                    rankingArea.text += $"{rankingEntries[i].Rank,-3} \t\t {rankingEntries[i].Username,-15} \t\t {rankingEntries[i].WinCount,-3}\n";
                }

                var seq = DOTween.Sequence();
                seq.Append(transform.DOScale(0.95f, 0.1f));
                seq.Append(transform.DOScale(1.05f, 0.1f));
                seq.Append(transform.DOScale(1f, 0.1f));
                seq.Play().OnComplete(() =>
                {
                    popupWindow.Show();
                });
            }
        }
    }
}