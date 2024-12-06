
public static class SCR_GameStatus
{
    static int redPoints;
    static int bluePoints;
    static int greenPoints;
    static int yellowPoints;
    static int gameMoviments;
    static float maxTimer;

    static int actualLevel;
    static int lastLevelPlayed;

    public static void AddRedPoints()
    {
        redPoints++;
    }
    public static void AddBluePoints()
    {
        bluePoints++;
    }
    public static void AddGreenPoints()
    {
        greenPoints++;
    }
    public static void AddYellowPoints()
    {
        yellowPoints++;
    }

    public static bool CheckRedPoints(int val)
    {
        if (greenPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckBluePoints(int val)
    {
        if (greenPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckYellowPoints(int val)
    {
        if (yellowPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckGreenPoints(int val)
    {
        if (greenPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static void ResetAllPoints()
    {
        redPoints = 0;
        bluePoints = 0;
        greenPoints = 0;
        yellowPoints = 0;
    }

    public static void ResetAll()
    {
        redPoints = 0;
        bluePoints = 0;
        greenPoints = 0;
        yellowPoints = 0;
        gameMoviments = 0;
        maxTimer = 0;
    }

    public static void SetMovimentLimit(int val)
    {
        gameMoviments = val;
    }
    public static void DecreaseMoviment()
    {
        gameMoviments--;
    }

    public static int CheckMovimentsLeft()
    {
        return gameMoviments;
    }

    /*
    public static void SetMaxMoviments(int move)
    {
         gameMoviments = move;
    }
    public float GetMaxTimer()
    {
        return maxTimer;
    }*/
}
