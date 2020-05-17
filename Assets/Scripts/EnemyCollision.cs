using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public List<GameObject> enemies;
    public AudioClip deadSound;
    public GameObject player;
    public GameObject spawnerLeft;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnerLeft = GameObject.Find("SpawnerLeft");
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Enemy")||(other.gameObject.tag=="Player"))
        {
            float firstEnemyScaleX = gameObject.transform.localScale.x;
            float firstEnemyScaleY = gameObject.transform.localScale.y;
            float secondEnemyScaleX = other.gameObject.transform.localScale.x;
            float secondEnemyScaleY = other.gameObject.transform.localScale.y;

            if ((firstEnemyScaleX > secondEnemyScaleX) && (firstEnemyScaleY > secondEnemyScaleY))
            {
                firstEnemyScaleX = Mathf.Sqrt(Mathf.Pow(firstEnemyScaleX, 2f) + Mathf.Pow(secondEnemyScaleX, 2f));
                firstEnemyScaleY = Mathf.Sqrt(Mathf.Pow(firstEnemyScaleY, 2f) + Mathf.Pow(secondEnemyScaleY, 2f));
                StartCoroutine(ScaleIncreaseAnimation(0.2f, firstEnemyScaleX, firstEnemyScaleY));

                if (other.gameObject.tag == "Player")
                {
                    AudioSource.PlayClipAtPoint(deadSound, transform.position);
                    StartCoroutine("LoseCoroutine");
                }
                enemies = spawnerLeft.GetComponent<Spawn>().enemies;
                enemies.Remove(other.gameObject);
                StartCoroutine(ScaleDecreaseAnimation(0.2f, other.gameObject));

                player.GetComponent<EditColor>().ChangeEnemyColor(gameObject);
            }
        }
    }

    IEnumerator LoseCoroutine()
    {
        yield return new  WaitForSeconds(0.3f);
        Time.timeScale = 0;
        GameObject.Find("Message").GetComponent<MyGUI>().isLose = true;
    }
    
    IEnumerator ScaleIncreaseAnimation(float time, float x, float y)
    {
        float i = 0;
        float rate = 1 / time;
    
        Vector2 fromScale = transform.localScale;
        Vector2 toScale = new Vector2(x, y);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(fromScale, toScale, i);
            yield return 0;
        }
    }
    
    IEnumerator ScaleDecreaseAnimation(float time, GameObject obj)
    {
        float i = 0;
        float rate = 1 / time;

        Vector2 fromScale = obj.transform.localScale;
        Vector2 toScale = new Vector2(0, 0);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            obj.transform.localScale = Vector2.Lerp(fromScale, toScale, i);
            yield return 0;
        }
        Destroy(obj.gameObject);
    }

}