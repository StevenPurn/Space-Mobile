using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public float menuTimer = 5.0f;
    public float menuTimerActual;

    public Sprite menuSpriteTwo;
    public Sprite menuSpriteThree;

    public int currentMenu = 0;
    public bool buttonPressed = false;

	// Use this for initialization
	void Start () {
        menuTimerActual = menuTimer;
	}

    public void ButtonPressed()
    {
        Destroy(GameObject.Find("MainMenuCanvas"));
        currentMenu += 1;
        ShowNextMenu();
        buttonPressed = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (buttonPressed)
        {
            menuTimerActual -= Time.deltaTime;

            if (menuTimerActual < 0)
            {
                currentMenu += 1;
                if (currentMenu == 3)
                {
                    GoToScene(1);
                    //Destroy(this);
                }
                ShowNextMenu();
                menuTimerActual = menuTimer;
            }
        }
	}

    public void ShowNextMenu()
    {
        if (currentMenu == 1)
        {
            GameObject.Find("Menu_Obj").GetComponent<SpriteRenderer>().sprite = menuSpriteTwo;
        }
        else if (currentMenu == 2)
        {
            GameObject.Find("Menu_Obj").GetComponent<SpriteRenderer>().sprite = menuSpriteThree;
        }

    }

    public void GoToScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
