using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector2 screenBounds;
    int enemyCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        for(int i = 0; i < 10; i++)
        {
            spawnEnemy();
        }
    }

    public void spawnEnemy()
    {
        GameObject a = Instantiate(enemyPrefab) as GameObject;

        //a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));

        //a.transform.position = new Vector2(Random.RandomRange((float)(-screenBounds.x * 0.9), (float)(screenBounds.x * 0.9)), Random.RandomRange((float)(-screenBounds.y * 0.9), (float)(screenBounds.y * 0.9)));


        //        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));

        //a.transform.position = new Vector2(Random.Range(-screenBounds.x * 0.9, screenBounds.x * 0.9), Random.Range(-screenBounds.y * 0.9, screenBounds.y * 0.9));

        a.transform.position = new Vector2(Random.Range((int)(-screenBounds.x * 0.9), (int)(screenBounds.x * 0.9)), Random.Range((int)(-screenBounds.y * 0.9), (int)(screenBounds.y * 0.9)));

        GameMechanics.sTheGlobalBehavior.UpdateEnemyCount(true);
        //Random.Range()
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
