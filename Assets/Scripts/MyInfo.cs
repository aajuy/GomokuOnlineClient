public class MyInfo
{
    private static MyInfo instance = new MyInfo();
    public static MyInfo Instance { get { return instance; } }

    public int UserId { get; set; }
    public string SessionId { get; set; }
    public int RoomId { get; set; }
    public int Turn { get; set; }
}
