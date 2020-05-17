using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public List<GameObject> enemies;
    public AudioClip eatSound;
    public GameObject spawnerLeft;

    void Start()
    {
        spawnerLeft = GameObject.Find("SpawnerLeft");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float playerScaleX = gameObject.transform.localScale.x;
            float playerScaleY = gameObject.transform.localScale.y;
            float enemyScaleX = other.gameObject.transform.localScale.x;
            float enemyScaleY = other.gameObject.transform.localScale.y;

            if ((playerScaleX > enemyScaleX) && (playerScaleY > enemyScaleY))
            {
                playerScaleX = Mathf.Sqrt(Mathf.Pow(playerScaleX, 2f) + Mathf.Pow(enemyScaleX, 2f));
                playerScaleY = Mathf.Sqrt(Mathf.Pow(playerScaleY, 2f) + Mathf.Pow(enemyScaleY, 2f));
                StartCoroutine(ScaleIncreaseAnimation(0.2f, playerScaleX, playerScaleY));

                enemies = spawnerLeft.GetComponent<Spawn>().enemies;
                enemies.Remove(other.gameObject);
                StartCoroutine(ScaleDecreaseAnimation(0.2f, other.gameObject));

                AudioSource.PlayClipAtPoint(eatSound, transform.position);

                foreach (var enemy in enemies)
                {
                    gameObject.GetComponent<EditColor>().ChangeEnemyColor(enemy);
                }

                float areaSum = 0;
                
                foreach(var enemy in enemies)
                {
                    areaSum += Mathf.PI * Mathf.Pow(enemy.gameObject.transform.localScale.x, 2) / 4;
                }

                if((Mathf.PI * Mathf.Pow(gameObject.transform.localScale.x, 2) / 4) > areaSum)
                {
                    StartCoroutine("WinCoroutine");
                }
            }
        }
    }
    
    IEnumerator WinCoroutine()
    {
        yield return new  WaitForSeconds(0.3f);
        Time.timeScale = 0;
        GameObject.Find("Message").GetComponent<MyGUI>().isWin = true;
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
