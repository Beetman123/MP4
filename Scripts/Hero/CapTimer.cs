using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CapTimer : MonoBehaviour
{
    bool continueUpdating = false;

    Image HealthBar2;

    public float maxTime; //= 0.2f;

    float timeLeft;

    public GameObject capBulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        //HealthBar2 = GetComponent<Image>();
        timeLeft = maxTime;
        capBulletTimer.SetActive(false);// HealthBar2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (continueUpdating)
        {
            ifShot();
        }
    }

    // call if bullet was shot
    private void ifShot()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            //HealthBar2.fillAmount = timeLeft / maxTime;

            transform.localScale = new Vector2(timeLeft, 1);
        }

        else
        {
            timeLeft = maxTime;


            // reset picture
            continueUpdating = false;

            capBulletTimer.SetActive(false); //HealthBar2.enabled = false;
            
            /*HealthBar2.fillAmount = 1; // reset fill amount*/
        }
        
    }        

    public void changeHealth(float health)
    {
        maxTime = health;

        capBulletTimer.SetActive(true); //.enabled = true;
        continueUpdating = true;
    }
}
