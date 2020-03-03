using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    public List<GameObject> enemies;
    public AudioClip deadSound;

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
                transform.localScale = new Vector2(firstEnemyScaleX, firstEnemyScaleY);

                //поражение
                if (other.gameObject.tag == "Player")
                {
                    Time.timeScale = 0;
                    GameObject.Find("Message").GetComponent<MyGUI>().isLose = true;
                    AudioSource.PlayClipAtPoint(deadSound, transform.position);
                }

                enemies = GameObject.Find("SpawnerLeft").GetComponent<Spawn>().enemies;
                enemies.Remove(other.gameObject);
                Destroy(other.gameObject);

                GameObject.FindGameObjectWithTag("Player").GetComponent<EditColor>().ChangeEnemyColor(gameObject);
            }
        }
    }
}
