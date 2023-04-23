using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeleteConfirmationController : MonoBehaviour
{
    [SerializeField] private GameObject deleteConfirmationPanel;
    [SerializeField] private GameObject loadProfilePanel;
    [SerializeField] private TextMeshProUGUI DELETE_T;
    [SerializeField] private InputField pin_IF;
    [SerializeField] private Button DELETE_B;
    [SerializeField] private Button backButton;
    [SerializeField] private UserListManager userListManager;
    
    private string usernameToDelete;

    private void Start()
    {
        DELETE_B.onClick.AddListener(ConfirmDeleteUser);
        backButton.onClick.AddListener(CloseDeleteConfirmation);
    }

    public void ShowDeleteConfirmation(string username)
    {
        usernameToDelete = username;
        DELETE_T.text = $"Please type in the Pin for {username} to delete {username}'s account. Warning: This is an irreversible process!";
        deleteConfirmationPanel.SetActive(true);
        loadProfilePanel.SetActive(false);
    }

    private void ConfirmDeleteUser()
    {
        string pin = pin_IF.text;

        if (userListManager.ValidatePin(usernameToDelete, pin))
        {
            userListManager.AttemptDeleteUser(usernameToDelete);
            CloseDeleteConfirmation();
        }
        else
        {
            Debug.LogError("Incorrect PIN.");
        }
    }

    private void CloseDeleteConfirmation()
    {
        pin_IF.text = "";
        deleteConfirmationPanel.SetActive(false);
        loadProfilePanel.SetActive(true);
    }
}
