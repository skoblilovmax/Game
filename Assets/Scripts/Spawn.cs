using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Spawn : MonoBehaviour
{
    public GameObject SpawnerRight;
    public List<GameObject> enemies;
    public List<GameObject> enemyPrefab;
    public int numOfEnemies;

    void Awake()
    {
        string path = @"C:\Users\skobl\Osmos\Assets\Config\NumberOfEnemies.txt";
        StreamReader sr = new StreamReader(path);
        string text = sr.ReadToEnd();
        sr.Close();
        numOfEnemies = Int32.Parse(text);

        for (int i = 0; i < numOfEnemies; i++)
        {
            float randomScale = UnityEngine.Random.Range(0.5f, 1.5f);
            bool validPosition = false;
            Vector3 pos = new Vector3();

            while (!validPosition)
            {
                pos = new Vector3(UnityEngine.Random.Range(gameObject.transform.position.x, SpawnerRight.transform.position.x), UnityEngine.Random.Range(gameObject.transform.position.y, SpawnerRight.transform.position.y), 0);
                if (Physics2D.OverlapCircle(pos, 0.5f * randomScale + 0.5f) == null)
                {
                    validPosition = true;
                }
            }

            var enemy = Instantiate(enemyPrefab[0], pos, gameObject.transform.rotation) as GameObject;

            enemy.transform.localScale = new Vector2(randomScale, randomScale);

            Vector2 direction = new Vector2(UnityEngine.Random.Range(gameObject.transform.position.x, SpawnerRight.transform.position.x), UnityEngine.Random.Range(gameObject.transform.position.x, SpawnerRight.transform.position.x));
            float acceleration = UnityEngine.Random.Range(0.1f, 0.5f);
            enemy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * acceleration, ForceMode2D.Impulse);
            enemies.Add(enemy);
        }
    }
}
