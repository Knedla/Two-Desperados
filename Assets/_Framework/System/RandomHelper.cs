using UnityEngine;

public class RandomHelper
{
    public static bool RandomBool { get { return (Random.Range(0, 2) == 0); } }
    public static int RandomInverse { get { return (Random.Range(0, 2) == 0) ? 1 : -1; } }

    public static bool RandomChance(int chance)
    {
        int randomChance = Random.Range(1, 101);
        return randomChance <= chance;
    }
}
