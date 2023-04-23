using System;

[Serializable]
public class UserData
{
    public string username;
    public string pin;
    public int progress;
    public float timeSpent;

    public UserData(string username, string pin, int progress, float timeSpent)
    {
        this.username = username;
        this.pin = pin;
        this.progress = progress;
        this.timeSpent = timeSpent;
    }
}
