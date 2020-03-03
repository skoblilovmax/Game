using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class EditColor : MonoBehaviour
{
    public int userColorR = 0, userColorG = 0, userColorB = 0;
    public int enemyFirstColorR = 0, enemyFirstColorG = 0, enemyFirstColorB = 0;
    public int enemySecondColorR = 0, enemySecondColorG = 0, enemySecondColorB = 0;

    public List<GameObject> enemies;

    void Awake()
    {
        string path = @"C:\Users\skobl\Osmos\Assets\Config\Color.txt";
        StreamReader sr = new StreamReader(path);
        string text = sr.ReadToEnd();
        sr.Close();

        text = text.Replace("{user: {color: ", "");
        text = text.Replace("}, enemy: {color1: ", "");
        text = text.Replace(", color2: ", "");
        text = text.Replace("}}", "");

        string userColorString = "";
        string enemyFirstColorString = "";
        string enemySecondColorString = "";

        EditString(ref text, ref userColorString);
        EditString(ref text, ref enemyFirstColorString);
        EditString(ref text, ref enemySecondColorString);

        GetColorValue(userColorString, ref userColorR, ref userColorG, ref userColorB);
        GetColorValue(enemyFirstColorString, ref enemyFirstColorR, ref enemyFirstColorG, ref enemyFirstColorB);
        GetColorValue(enemySecondColorString, ref enemySecondColorR, ref enemySecondColorG, ref enemySecondColorB);

        ChangePlayerColor(userColorR, userColorG, userColorB);

        enemies = GameObject.Find("SpawnerLeft").GetComponent<Spawn>().enemies;

        foreach (var enemy in enemies)
        {
            ChangeEnemyColor(enemy);
        }
    }

    void EditString(ref string text, ref string colorString)
    {
        char firstChar = '[';
        char secondChar = ']';
        int indexFirstChar = text.IndexOf(firstChar) + 1;
        int indexSecondChar = text.IndexOf(secondChar);
        colorString = text.Substring(indexFirstChar, indexSecondChar - indexFirstChar);

        text = text.Substring(indexSecondChar + 1);
    }

    void GetColorValue(string text, ref int colorR, ref int colorG, ref int colorB)
    {
        char ch = ',';
        int indexChar = text.IndexOf(ch);
        colorR = int.Parse(text.Substring(0, indexChar));
        text = text.Substring(indexChar + 2);

        indexChar = text.IndexOf(ch);
        colorG = int.Parse(text.Substring(0, indexChar));
        text = text.Substring(indexChar + 2);

        colorB = int.Parse(text);
    }

    void ChangePlayerColor(float colorR, float colorG, float colorB)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(colorR / 255, colorG / 255, colorB / 255);
    }

    public void ChangeEnemyColor(GameObject obj)
    {
        float playerScaleX = gameObject.transform.localScale.x;
        float enemyScaleX = obj.gameObject.transform.localScale.x;
        float coefficient = Mathf.Pow((enemyScaleX / playerScaleX), 2);
        float middleR = (enemySecondColorR - enemyFirstColorR) / 2 + enemyFirstColorR;
        float middleG = (enemySecondColorG - enemyFirstColorG) / 2 + enemyFirstColorG;
        float middleB = (enemySecondColorB - enemyFirstColorB) / 2 + enemyFirstColorB;
        float colorR, colorG, colorB;

        colorR = CalculateColor(coefficient, middleR, enemySecondColorR, enemyFirstColorR);
        colorG = CalculateColor(coefficient, middleG, enemySecondColorG, enemyFirstColorG);
        colorB = CalculateColor(coefficient, middleB, enemySecondColorB, enemyFirstColorB);

        obj.GetComponent<SpriteRenderer>().color = new Color(colorR / 255, colorG / 255, colorB / 255);
    }

    float CalculateColor(float coefficient, float middle, int enemySecondColor, int enemyFirstColor)
    {
        float color = 0;

        if (middle * coefficient >= enemySecondColor)
        {
            color = enemySecondColorR;
        }

        if (middle * coefficient <= enemyFirstColor)
        {
            color = enemyFirstColorR;
        }

        if ((middle * coefficient < enemySecondColorR) && (middle * coefficient > enemyFirstColorR))
        {
            color = (int)(middle * coefficient);
        }

        return color;
    }
}
