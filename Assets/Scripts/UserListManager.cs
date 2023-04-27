using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UserListManager : MonoBehaviour
{
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject userButtonPrefab;
    [SerializeField] private Transform userListContent;
    [SerializeField] private InputField pinInputField;
    [SerializeField] private DeleteConfirmationController deleteConfirmationController;

    private void Start()
    {
        loginManager = LoginManager.Instance;
    }

    public void PopulateUserList()
    {
        ClearUserList();

        Dictionary<string, UserData> usersData = loginManager.GetUsersData();

        if (usersData == null)
        {
            Debug.LogError("usersData is null.");
            return;
        }

        foreach (KeyValuePair<string, UserData> entry in usersData)
        {
            GameObject userButton = Instantiate(userButtonPrefab, userListContent);
            //95 == number of slides total
            float progressPercentage = entry.Value.progress / 95f; // Assuming progress is stored as a float between 0 and 1.
            userButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.Key} \r\n Tutorial Progress: {progressPercentage}% \r\n Total Time Taken: {FormatTime(entry.Value.timeSpent)}";
            userButton.GetComponent<Button>().onClick.AddListener(() => {
                loginManager.SetUsername(entry.Key);
                loginManager.LoadUserProgress();
                LoadHomeScene();   
            });

            Button deleteButton = userButton.transform.Find("DeleteButton").GetComponent<Button>();
            deleteButton.onClick.AddListener(() => deleteConfirmationController.ShowDeleteConfirmation(entry.Key));
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private void ClearUserList()
    {
        for (int i = 0; i < userListContent.childCount; i++)
        {
            Destroy(userListContent.GetChild(i).gameObject);
        }
    }

    public void AttemptDeleteUser(string username)
    {
        string pin = pinInputField.text;

        if (loginManager.DeleteUser(username, pin))
        {
            Debug.Log("User deleted.");
            PopulateUserList();
        }
        else
        {
            Debug.LogError("Failed to delete user.");
        }

        deleteConfirmationController.ShowDeleteConfirmation(username);
    }

    public bool ValidatePin(string username, string pin)
    {
        UserData userData;
        if (loginManager.GetUsersData().TryGetValue(username, out userData))
        {
            return userData.pin == pin;
        }
        return false;
    }

    private void LoadHomeScene()
    {
        SceneManager.LoadScene("S1_Home");
    }
}

