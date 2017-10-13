using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class GameControl {

    public static List<Rooms> rooms = new List<Rooms>();

    public static void RestartLevel()
    {
        SceneManager.LoadScene("TestScene");
    }

    public static void CloseGame()
    {
        Application.Quit();
    }
}
