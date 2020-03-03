using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public List<GameObject> enemies;
    public AudioClip eatSound;

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
                transform.localScale = new Vector2(playerScaleX, playerScaleY);

                enemies = GameObject.Find("SpawnerLeft").GetComponent<Spawn>().enemies;
                enemies.Remove(other.gameObject);
                Destroy(other.gameObject);

                AudioSource.PlayClipAtPoint(eatSound, transform.position);

                foreach (var enemy in enemies)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<EditColor>().ChangeEnemyColor(enemy);
                }

                float areaSum = 0;
                foreach(var enemy in enemies)
                {
                    areaSum += Mathf.PI * Mathf.Pow(enemy.gameObject.transform.localScale.x, 2) / 4;
                }

                if((Mathf.PI * Mathf.Pow(gameObject.transform.localScale.x, 2) / 4) > areaSum)
                {
                    Time.timeScale = 0;
                    GameObject.Find("Message").GetComponent<MyGUI>().isWin = true;
                }
            }
        }
    }
}
