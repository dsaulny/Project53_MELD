using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountScreenController : MonoBehaviour
{
    private LoginManager loginManager;
    public TextMeshProUGUI accountInfoText;

    void Start()
    {
        loginManager = LoginManager.Instance;
    }

    public void UpdateAccountInfo()
    {
        UserData userData = loginManager.LoadUsersData();
        if (userData != null)
        {
            accountInfoText.text = $"Username: {userData.username}\n\n\nTutorial Progress: {((userData.progress / 95f) * 100f)}%";
        }
        else
        {
            accountInfoText.text = "No user is currently logged in.";
        }
    }

    public void Logout()
    {
        loginManager.Logout();
    }
}
