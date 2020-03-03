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
            GUI.Box(new Rect(870, 200, 200, 30), "You lose", customBox);

            if (GUI.Button(new Rect(870, 500, 200, 50), "PLAY", customButton))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect(870, 600, 200, 50), "QUIT", customButton))
            {
                Application.Quit();
            }
        }

        if (isWin)
        {
            GUI.Box(new Rect(870, 200, 200, 30), "You win", customBox);

            if (GUI.Button(new Rect(870, 500, 200, 50), "PLAY", customButton))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect(870, 600, 200, 50), "QUIT", customButton))
            {
                Application.Quit();
            }
        }
    }
}
