using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public void RestartLevel()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
