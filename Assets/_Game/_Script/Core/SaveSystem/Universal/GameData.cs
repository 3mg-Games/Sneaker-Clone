[System.Serializable]
public class GameData
{
    public int totalMoney;
    public int Level;
    public int currentCustomerCount;

    public bool isSection2Unlocked;
    public bool isSection3Unlocked;

    public bool cinematic1;
    public bool cinematic2;

    public bool Section1;
    public bool Section2;

    public bool Tutorial1;    
    public bool Tutorial2;    
    public bool finalTutorialOver;
    public GameData()
    {
        totalMoney = 0;
        Level = 0;
        currentCustomerCount = 0;
        isSection2Unlocked = false;
        isSection3Unlocked = false;

        cinematic1 = false;
        cinematic2 = false;

        Section1 = false;
        Section2 = false;

        finalTutorialOver = false;
        Tutorial1 = false;
        Tutorial2 = false;
    }
}
