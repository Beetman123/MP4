using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    private float alphaColor = 1;

    public SpriteRenderer sRender;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        
        //alphaColor -= 0.25f;
        alphaColor *= 0.8f;

        sRender.color = new Color(1, 1, 1, alphaColor);      
        
        if (enemyHealth <= 0)
        {


            //Remove Object
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CapBullet")
        {
            TakeDamage(1);
        }
    }
}
