using TMPro;
using UnityEngine;

public class CheckWaiting : MonoBehaviour
{
    public TMP_Text waitingText;
    public TMP_Text joinButtonText;

    // Start is called before the first frame update
    void Start()
    {
        MyInfo.Instance.Waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MyInfo.Instance.Waiting)
        {
            waitingText.text = "Waiting";
            joinButtonText.text = "Cancel";
        }
    }
}
