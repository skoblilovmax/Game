using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGUI : MonoBehaviour
{
    public GUIStyle customButton;
    public GUIStyle customBox;

    public bool isLose;
    public bool isWin;


    void OnGUI()
    {
        if (isLose)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 4, 200, 30), "You lose", customBox);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 2 * Screen.height / 4, 200, 50), "PLAY", customButton))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 2 * Screen.height / 4 + 100, 200, 50), "QUIT", customButton))
            {
                Application.Quit();
            }
        }

        if (isWin)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 4, 200, 30), "You win", customBox);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 2 * Screen.height / 4, 200, 50), "PLAY", customButton))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 2 * Screen.height / 4 + 100, 200, 50), "QUIT", customButton))
            {
                Application.Quit();
            }
        }
    }
}