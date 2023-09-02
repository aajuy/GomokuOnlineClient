using TMPro;
using UnityEngine;

public class UsernameScript : MonoBehaviour
{
    public TMP_Text usernameText;

    // Start is called before the first frame update
    void Start()
    {
        usernameText.text = MyInfo.Instance.Username;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
