using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using UnityEngine.Animations;

public class EnemyMovement : MonoBehaviour
{
    private GenerateWaypoint generateWaypoint;
    public HeroMechanics heroMechanics;
    public WaypointMachanics waypointMachanics;
    public EnemyMechanics enemyMechanics;

    private Animator anim;

    //public Transform[] goblinHuts;

    public Vector2 goal;

    public int currGoal;

    public int currRandom;

    private bool firstHut = true;

    public float speed = 20;
    public float turn = 0.3f / 60f;

    public float radius = 0.5f;

    bool ifTouched;

    private Transform playerTransform;

    void Awake()
    {
        heroMechanics = GameObject.Find("Hero").GetComponent<HeroMechanics>();
        generateWaypoint = Camera.main.GetComponent<GenerateWaypoint>(); // gets the generateWaypoint script at start (otherwise will give an null error)
        enemyMechanics = this.GetComponent<EnemyMechanics>();

        playerTransform = heroMechanics.transform;

        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

       

        // Get closest or random destination
        //goal = GetClosestGoblinHut(generateWaypoint.enemyGoal).position;


        //waypointMachanics = GetClosestGoblinHut(generateWaypoint.enemyGoal).gameObject.GetComponent<WaypointMachanics>();
        //waypointMachanics.enemyMove.Add(this);
    }



    // Update is called once per frame
    void Update()
    {
        switch (enemyMechanics.enemyState)
        {
            case EnemyMechanics.EnemyState.Patrol:
                Patrol();
                break;

            case EnemyMechanics.EnemyState.Chase:

                if (Vector2.Distance(transform.position, playerTransform.position) < 40)
                {
                    Chase();
                }

                else
                {
                    enemyMechanics.enemyState = EnemyMechanics.EnemyState.ReSize;
                    
                    anim.Play("Resize");                    
                }

                break;
        }



        // get / check cordnites (check if waypoint moved)
        //transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);


/*        Vector3 dir = ((Vector3)goal - transform.position);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turn);

        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        Vector3 position = transform.position;

        position += transform.rotation * velocity;
        transform.position = position;*/

/*        if (Vector2.Distance(transform.position, goal) < 1)
        {
            NewDestination(heroMechanics.ifRandom); 
        }*/
    }

    void Chase()
    {
        // move toward player
        transform.position = Vector3.MoveTowards(this.transform.position, playerTransform.position, speed * Time.deltaTime);

        // face player
        Vector3 dir = playerTransform.position - this.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void Patrol()
    {
        Vector3 dir = ((Vector3)goal - transform.position);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turn);

        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        Vector3 position = transform.position;

        position += transform.rotation * velocity;
        transform.position = position;
    }


    public void NewCoordinates () // what if waypoint is destroyed?
    {
        goal = generateWaypoint.enemyGoal[currGoal].transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Goblin Hut")
        {
            waypointMachanics = collider.gameObject.GetComponent<WaypointMachanics>();


            if (heroMechanics.ifRandom)
            {
                if (currRandom == waypointMachanics.currentWaypoint || firstHut)
                {
                    firstHut = false;

                    NewRandomDestination();
                }
            }

            else
            {
                if (currGoal == waypointMachanics.currentWaypoint)
                {
                    NewDestination();
                }
            }
        }
    }

    public void NewRandomDestination()
    {
        currRandom = UnityEngine.Random.Range(0, generateWaypoint.enemyGoal.Length);

        while (currRandom == currGoal - 1)
        {
            currRandom = UnityEngine.Random.Range(0, generateWaypoint.enemyGoal.Length);
        }

        goal = generateWaypoint.enemyGoal[currRandom].transform.position;

        currGoal = currRandom;
    }

    public void NewDestination() // what if waypoint is destroyed?
    {
        currGoal = waypointMachanics.currentWaypoint + 1;

        if (currGoal > 5)
        {
            currGoal = 0;
        }

        currRandom = currGoal - 1;

        goal = generateWaypoint.enemyGoal[currGoal].transform.position;
    }

    public Transform GetClosestGoblinHut(GameObject[] goblinHuts)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in goblinHuts)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
