using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void AdminLogin()
    {
        SceneManager.LoadScene(1);
    }

    public void Logout()
    {
        SceneManager.LoadScene(0);
    }
}
