using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Text;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InputField pinInputField;
    private string currentUser;
    private Dictionary<string, UserData> usersData;
    private string dataPath;
    // Add a private key for AES encryption
    //private readonly string encryptionKey = "uvD4TC92ONZiw9+FdlAFhqigPf0SYD1CseS9XN12OKF5iGtox5JjrThpSZp4FJwN";

    private float timeSinceLastBreak = 0f;
    private float breakInterval = 10000000f; // 30 minutes
    private float breakDuration = 300f; // 5 minutes
    private bool isBreakTime = false;
    private bool loggedIn = false; // Add this line

    private float autosaveInterval = 60f;
    private float timeSinceLastAutosave = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dataPath = Path.Combine(Application.persistentDataPath, "usersData.json");
        LoadUserData();
    }

    private void LoadUserData()
    {
        if (File.Exists(dataPath))
        {
            string jsonString = File.ReadAllText(dataPath);
            usersData = JsonUtility.FromJson<SerializableDictionary>(jsonString).ToDictionary();
        }
        else
        {
            usersData = new Dictionary<string, UserData>();
        }
    }

    private void SaveUserData()
    {
        SerializableDictionary serializableDictionary = new SerializableDictionary(usersData);
        string jsonString = JsonUtility.ToJson(serializableDictionary);
        File.WriteAllText(dataPath, jsonString);
    }

    public void CreateUser()
    {
        string username = usernameInputField.text;
        string pin = pinInputField.text;

        if (string.IsNullOrEmpty(username))
        {
            username = "Bob";
        }
        if (string.IsNullOrEmpty(pin))
        {
            pin = "1111";
        }

        if (usersData.ContainsKey(username))
        {
            Debug.LogError("Username already exists.");
            return;
        }

        UserData newUser = new UserData(username, pin, 0, 0);
        usersData.Add(username, newUser);
        SaveUserData();

        currentUser = username;
        loggedIn = true;
        LoadUserProgress();
    }

    public bool DeleteUser(string username, string pin)
    {
        if (!usersData.ContainsKey(username))
        {
            Debug.LogError("Username not found.");
            return false;
        }

        if (usersData[username].pin != pin)
        {
            Debug.LogError("Incorrect PIN.");
            return false;
        }

        usersData.Remove(username);
        SaveUserData();
        return true;
    }

    public void Login()
    {
        string username = usernameInputField.text;
        string pin = pinInputField.text;

        if (!usersData.ContainsKey(username))
        {
            Debug.LogError("Username not found.");
            return;
        }

        if (usersData[username].pin != pin)
        {
            Debug.LogError("Incorrect PIN.");
            return;
        }

        currentUser = username;
        loggedIn = true;
        LoadUserProgress();
    }

    public void Logout()
    {
        loggedIn = false;
        currentUser = null;
    }

    public void LoadUserProgress()
    {
        int progress = usersData[currentUser].progress;
        // Load the correct section of the tutorial based on the user's progress.
    }

    public void SaveUserProgress(int progress)
    {
        usersData[currentUser].progress = progress;
        SaveUserData();
    }

    public Dictionary<string, UserData> GetUsersData()
    {
        return usersData;
    }

    public UserData LoadUsersData()
    {
        if (string.IsNullOrEmpty(currentUser))
        {
            Debug.LogError("Current username is not set.");
            return null;
        }

        if (!usersData.ContainsKey(currentUser))
        {
            Debug.LogError($"User data not found for the current user: {currentUser}");
            return null;
        }

        return usersData[currentUser];
    }

    public void SetUsername(string username)
    {
        usernameInputField.text = username;
    }

    private void Update()
    {
        if (loggedIn)
        {
            if (!isBreakTime)
            {
                timeSinceLastBreak += Time.deltaTime;
                if (timeSinceLastBreak >= breakInterval)
                {
                    isBreakTime = true;
                    StartCoroutine(BreakTime());
                }
            }

            timeSinceLastAutosave += Time.deltaTime;
            if (timeSinceLastAutosave >= autosaveInterval)
            {
                timeSinceLastAutosave = 0f;
                SaveUserData();
            }

            usersData[currentUser].timeSpent += Time.deltaTime;
        }
    }

    private IEnumerator BreakTime()
    {
        // Notify the user to take a break
        Debug.Log("Take a 5-minute break!");

        // Wait for 5 minutes (breakDuration)
        yield return new WaitForSeconds(breakDuration);

        // Reset the break timer and continue the tutorial
        timeSinceLastBreak = 0f;
        isBreakTime = false;
    }

    private void OnApplicationQuit()
    {
        if (loggedIn)
        {
            SaveUserData();
        }
    }

    /*
    private static string Encrypt(string plainText, string key)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                }

                array = memoryStream.ToArray();
            }
        }

        return Convert.ToBase64String(array);
    }

    private static string Decrypt(string cipherText, string key)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
    */
}
