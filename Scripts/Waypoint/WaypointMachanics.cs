using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMachanics : MonoBehaviour
{
    private bool invincible = true;

    public int enemyHealth;

    private float alphaColor = 1;

    private GenerateWaypoint generateWaypoint;

    public int currentWaypoint;

    public SpriteRenderer sRender;

    // public EnemyMovement[] enemyMove;
    //public List<EnemyMovement> enemyMove;

    void Start()
    {
        generateWaypoint = Camera.main.GetComponent<GenerateWaypoint>();
        StartCoroutine(RemoveInvinciblity());
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        alphaColor -= 0.25f;
        sRender.color = new Color(1, 1, 1, alphaColor);

        if (enemyHealth <= 0)
        {
            generateWaypoint.SpawnWaypoint(currentWaypoint/*, enemyMove*/);

            //Remove Object
            //Destroy(this.gameObject);

            // move 

            // reset variables
            alphaColor = 1;
            enemyHealth = 4;
            sRender.color = new Color(1, 1, 1, alphaColor);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CapBullet" && !invincible)
        {
            TakeDamage(1);
        }
        if (collision.gameObject.tag == "CapBullet")
        {
            Destroy(collision.gameObject);

            GameMechanics.sTheGlobalBehavior.RemoveCapCount();
        }
    }

    IEnumerator RemoveInvinciblity()
    {
        yield return new WaitForSeconds(1f);
        invincible = false;
    }


}
