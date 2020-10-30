using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    private GenerateEnemy generateEnemy;
    private int health = 3;

    private SpriteRenderer sRender;
    private float alphaColor = 1;

    private bool ifHit = false;

    public float turn = 90f / 1;

    public Sprite[] enemySprites;

    private int knockbackMultiplyer = 1;

    public enum EnemyState
    {
        Patrol,
        CCW,
        Chase,
        ReSize, // Enlarge & size Decrease
        Stunned,
    }

    public EnemyState enemyState; 

    void Awake()
    {
        sRender = this.GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        generateEnemy = Camera.main.GetComponent<GenerateEnemy>();

        enemyState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        

    }


/*    void OnDisable() //OnDestroy()
    {
        generateEnemy.spawnEnemy();
        GameMechanics.sTheGlobalBehavior.UpdateEnemysDestroyed();
    }*/


    // to make sure he is destroyed if we "wait on enemy" / on top of him
    void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            if (ifHit)
            {
                Destroy(gameObject);
                generateEnemy.spawnEnemy();
                GameMechanics.sTheGlobalBehavior.UpdateEnemysDestroyed();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision) //OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Hero" && enemyState != EnemyState.Stunned)
        {
            //this.gameObject.SetActive(false);

            if (ifHit)
            {
                Destroy(gameObject);
                generateEnemy.spawnEnemy();
                GameMechanics.sTheGlobalBehavior.UpdateEnemysDestroyed();
            }

            else if (enemyState != EnemyState.CCW)
            {
                // turn red
                sRender.color = new Color(1, 0, 0); 


                enemyState = EnemyState.CCW;
                
                //ifHit = true;

                // CCW & CW
                StartCoroutine(RotateObjectCCW(90, Vector3.forward, 1, false));
            }

        }

        if (collision.gameObject.tag == "CapBullet")
        {
            StopAllCoroutines();


            StartCoroutine(knockback(collision.transform, 4 * knockbackMultiplyer));

            knockbackMultiplyer++;

            health--;

            alphaColor -= 0.25f;

            enemyState = EnemyState.Stunned;

            /*            if(enemyState == EnemyState.Chase)
                        {
                            sRender.color = new Color(1, 0, 0, alphaColor);
                        }
                        else 
                        {
                            sRender.color = new Color(1, 1, 1, alphaColor);
                        }*/

            // gets the current color and only changes the alpha
            /*sRender.color = new Color(sRender.color.r, sRender.color.g, sRender.color.b, alphaColor);*/

            if (health > 0)
            {
                sRender.sprite = enemySprites[health - 1];
            }

            if (health == 2)
            {
                StartCoroutine(RotateObjectCCW(90, Vector3.forward, 1, true));
            }

            /*else if (health == 1)
            {
                //StopAllCoroutines();
            }*/

            Destroy(collision.gameObject); //collision.gameObject.SetActive(false); 
            GameMechanics.sTheGlobalBehavior.RemoveCapCount();


            if (health == 0)
            {
                Destroy(this.gameObject);  //this.gameObject.SetActive(false);
                generateEnemy.spawnEnemy();
                GameMechanics.sTheGlobalBehavior.UpdateEnemysDestroyed();
            }
        }
    }

    IEnumerator RotateObjectCCW (float angle, Vector3 axis, float inTime, bool ifContinue)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        // save starting rotation position
        Quaternion startRotation = transform.rotation;

        float deltaAngle = 0;

        // rotate until reaching angle
        while (deltaAngle < angle)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

            yield return null;
        }

        if (ifContinue)
        {
            StartCoroutine(RotateObjectCCW(angle, Vector3.forward, inTime, ifContinue));
        }
        else
        {
            StartCoroutine(RotateObjectCW(angle, -axis, inTime));
        }
    }

    IEnumerator RotateObjectCW (float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        // save starting rotation position
        Quaternion startRotation = transform.rotation;

        float deltaAngle = 0;

        // rotate until reaching angle
        while (deltaAngle < angle)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

            yield return null;
        }

        // change to Chase function
        ifHit = true;

        if (enemyState != EnemyState.Stunned)
        {
            enemyState = EnemyState.Chase;
        }

    }

    public void changeToPatrol()
    {
        enemyState = EnemyState.Patrol;
        sRender.color = new Color(1, 1, 1);
        ifHit = false;
    }

    IEnumerator knockback (Transform capPostion, float knockbackDistance)
    {
        Vector3 direction = capPostion.up; //- transform.position;

        Vector3 targetPos = this.transform.position + (direction * knockbackDistance);

        while (transform.position != targetPos)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, 2 * Time.deltaTime);// .MoveTowards(this.transform.position, targetPos, 20 * Time.deltaTime);

            yield return null;
        }
    }
}
