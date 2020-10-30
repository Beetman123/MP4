using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapMechanics : MonoBehaviour
{
    static private HeroMechanics sHero = null;
    private Vector3 screenBounds;
    static public void SetHero(HeroMechanics h)
    {
        sHero = h;
    }

    private const float CapSpeed = 40f;
    private const int LifetTime = 300; // life for this number of cycles
    private int LifeCount = 0;


    void Awake()
    {
        SetHero(FindObjectOfType<HeroMechanics>());
    }


    // Start is called before the first frame update
    void Start()
    {
        LifeCount = LifetTime;
        GameMechanics.sTheGlobalBehavior.AddCapCount();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (CapSpeed * Time.smoothDeltaTime);
        LifeCount--;
        if (LifeCount <= 0)
        {
            Destroy(transform.gameObject); // suiside
            CapMechanics.sHero.SubtractCap();

            GameMechanics.sTheGlobalBehavior.RemoveCapCount();
        }

        leftScreen();
    }

    void leftScreen()
    {
        bool destroy = false;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 1.01f, Screen.height * 1.01f));

        if (transform.position.x > screenBounds.x)
            destroy = true;
        if (transform.position.x < -screenBounds.x)
            destroy = true;

        if (transform.position.y > screenBounds.y)
            destroy = true;
        if (transform.position.y < -screenBounds.y)
            destroy = true;

        if (destroy)
        {
            Destroy(this.gameObject);
            GameMechanics.sTheGlobalBehavior.RemoveCapCount();
        }
    }
}
