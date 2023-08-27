using UnityEngine;

public class ServerConfig : MonoBehaviour
{
    public static string LoginServerAddress { get; set; } = "http://";
    public static string MatchServerAddress { get; set; }
    public static string GameServerAddress { get; set; }
}
