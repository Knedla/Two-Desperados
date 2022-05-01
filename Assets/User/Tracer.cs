public class Tracer : IUser
{
    private static Tracer instance = null;
    private Tracer() { }

    public static Tracer Instance
    {
        get
        {
            if (instance == null)
                instance = new Tracer();

            return instance;
        }
    }
}
