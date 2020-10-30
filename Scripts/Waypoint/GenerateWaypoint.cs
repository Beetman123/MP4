using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GenerateWaypoint : MonoBehaviour
{
    HeroMechanics heroMechanics;


    public GameObject GoblinHutPrefab;

    [SerializeField]
    private Transform[] localWaypoint; // the intial variable area

    [SerializeField]
    private Transform[] localWaypoint2;

    //private Transform[] currLocalWaypoints;

    public GameObject[] enemyGoal; // equal temp hut 

    public Transform waypointParent;

    public SpriteRenderer[] spriteRenderers;
    bool originalWaypoint = true;

    // Start is called before the first frame update
    void Start()
    {
        heroMechanics = FindObjectOfType<HeroMechanics>(); // initalize heroMechanics

        // Spawns the goblin huts
        for (int i = 0; i < localWaypoint.Length; i++)
        {
            GameObject tempHut = Instantiate(GoblinHutPrefab, localWaypoint[i].position, Quaternion.identity);

            tempHut.GetComponent<WaypointMachanics>().currentWaypoint = i;

            tempHut.transform.parent = waypointParent; // Sets where the Goblin Huts are stored in the Heirearcy

            enemyGoal[i] = tempHut;
        }

    }

    public void changeWaypoints()
    {
        originalWaypoint = !originalWaypoint;

        if(!originalWaypoint)
        {
            // move waypoints
            for (int i = 0; i < localWaypoint2.Length; i++)
            {
                enemyGoal[i].transform.position = localWaypoint2[i].transform.position;
            }

            // change enemys goal location


        }

        else
        {
            for (int i = 0; i < localWaypoint.Length; i++)
            {
                enemyGoal[i].transform.position = localWaypoint[i].transform.position;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnWaypoint(int waypointPosition/*, List<EnemyMovement> enemyMove*/)// needs to be finished
    {
        //GameObject waypoint = Instantiate(waypointPrefab) as GameObject;


        /*waypoint.transform.position = new Vector2(Random.Range((int)(waypointCenter - 15), (int)(waypointCenter + 15)), // x
            Random.Range((int)(waypointCenter - 15), (int)(waypointCenter + 15))); // y*/

        Vector3 randomPosition = new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));

        Vector3 randomHutPos;
        if (originalWaypoint)
        {
            randomHutPos = localWaypoint[waypointPosition].position + randomPosition;
        }
        else
        {
            randomHutPos = localWaypoint2[waypointPosition].position + randomPosition;
        }


        GameObject tempHut = enemyGoal[waypointPosition]; //Instantiate(GoblinHutPrefab, randomHutPos, Quaternion.identity);

        tempHut.transform.position = randomHutPos;



        //tempHut.transform.parent = waypointParent;

        tempHut.GetComponent<WaypointMachanics>().currentWaypoint = waypointPosition;


       /* foreach (EnemyMovement enemy in enemyMove)
        {
            enemy.goal = enemyGoal[waypointPosition].position;
        }*/

        //GameMechanics.sTheGlobalBehavior.UpdateEnemyCount(true);
        //Random.Range()

//        transform.position.x;
    }


    public void showHideHuts(bool hid)
    {
        for(int i = 0; i < enemyGoal.Length; i++)
        {
            enemyGoal[i].GetComponent<SpriteRenderer>().enabled = !hid;
        }
    }


}
