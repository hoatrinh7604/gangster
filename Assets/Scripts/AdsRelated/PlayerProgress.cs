

public class PlayerProgress   {

    public static GameData gameData;

    public static void Init()
    {
        gameData = new GameData();
        gameData.ImportantValues = new int[3];
    }
   
    
}

public struct GameData
{
    public int[] ImportantValues;
}

