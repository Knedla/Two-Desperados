public class Player : IUser
{
    private static Player instance = null;
    private Player() { }

    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = new Player();
            
            return instance;
        }
    }
}
