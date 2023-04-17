[System.Serializable]
public class User
{
    public string name;
    public string userPin;
    public bool hasStartedTutorial;
    public bool hasCompletedSafetyTraining;
    public long totalTimeTaken;
    public int currentSection;

    public User(string userName, string pin)
    {
        name = userName;
        userPin = pin;
        hasStartedTutorial = false;
        hasCompletedSafetyTraining = false;
        totalTimeTaken = 0;
        currentSection = -1;
    }

    public User()
    {
        name = "John";
        userPin = "0000";
        hasStartedTutorial = false;
        hasCompletedSafetyTraining = false;
        totalTimeTaken = 0;
        currentSection = -1;
    }
}
