using Assets.Scripts.GameScene;
using Google.Protobuf.GameProtocol;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[,] board;
    public int GameState { get; set; }
    public int CurrentTurn { get; set; } = 1;
    public DateTime LastMoveTime { get; set; }
    public int Result { get; set; }

    [SerializeField]
    private GameObject transparentStone;

    [SerializeField]
    private GameObject whiteStone;

    [SerializeField]
    private GameObject blackStone;

    public TMP_Text turnText;

    public TMP_Text timerText;

    public TMP_Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        board = new GameObject[15, 15];

        for (int i = 0; i < 15; i++)
        {
            float x = ConvertIdxToPos(i);
            for (int j = 0; j < 15; j++)
            {
                float y = ConvertIdxToPos(j);
                Vector2 position = new Vector2(x, y);
                GameObject obj = Instantiate(transparentStone, position, Quaternion.identity);
                obj.GetComponent<GridInfo>().Y = i;
                obj.GetComponent<GridInfo>().X = j;
                board[i, j] = obj;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == 0)
        {
            return;
        }

        UpdateTimer();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 posistion = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posistion, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                GridInfo gridInfo = obj.GetComponent<GridInfo>();

                Debug.Log($"Y: {gridInfo.Y}, X: {gridInfo.X} Clicked!");

                ServerSession session = GameObject.Find("GameServerNetworkManager")
                    .GetComponent<GameServerNetworkManager>()
                    .Session;

                if (session != null)
                {
                    C_Move packet = new C_Move()
                    {
                        Y = gridInfo.Y,
                        X = gridInfo.X
                    };
                    session.Send(packet);
                }
            }
        }
    }

    public void PlaceStone(int y, int x, int turn)
    {
        Destroy(board[y, x]);

        float yPos = ConvertIdxToPos(y);
        float xPos = ConvertIdxToPos(x);
        Vector2 position = new Vector2(yPos, xPos);

        board[y, x] = Instantiate((turn == 1 ? blackStone : whiteStone), position, Quaternion.identity);
    }

    private float ConvertIdxToPos(int idx)
    {
        return 0.52f * (idx - 7);
    }

    private void UpdateTimer()
    {
        int leftTime = 30 - (int)(DateTime.UtcNow - LastMoveTime).TotalSeconds;
        leftTime = Math.Max(0, leftTime);
        timerText.text = leftTime.ToString();
    }

    public void UpdateTurn(int turn)
    {
        turnText.text = (turn != MyInfo.Instance.Turn ? "Your turn" : "Opponent's turn");
    }

    public void UpdateResult(int result)
    {
        switch (result)
        {
            case 0:
                resultText.text = "Draw";
                break;
            default:
                resultText.text = (result == MyInfo.Instance.Turn ? "You win!" : "You lose");
                break;
        }
    }

    public void End(int result)
    {
        GameState = 0;

        UpdateResult(result);
    }
}
