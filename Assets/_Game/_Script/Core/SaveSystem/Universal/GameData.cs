[System.Serializable]
public class GameData
{
    public int totalMoney;
    public int Level;
    public int currentCustomerCount;

    public bool isSection2Unlocked;
    public bool isSection3Unlocked;
    public GameData()
    {
        totalMoney = 0;
        Level = 0;
        currentCustomerCount = 0;
        isSection2Unlocked = false;
        isSection3Unlocked = false;

    }
}
