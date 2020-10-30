using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class HeroMechanics : MonoBehaviour
{
    [SerializeField]
    private Transform capPos;

    private float nextFireTime;
    private float fireRate = 0.2f; 

    public bool ifMouse = true;
/*    int rotation = 0;*/
    public float mRotationSpeed = 90f / 2f; // 90 degreees in 2 seconds
    public float mHeroSpeed = 20f;

    private int mTotalCapCount = 0;

    [SerializeField]
    private GameObject cap;

    public bool ifRandom = true;
    public bool ifOriginal = true;
    public bool hid = false;

    //public bool originalWaypoint = true;

    Quaternion rot;

    float z;

    GenerateWaypoint generateWaypoint;

    private CapTimer capTimer;

    //public static GameManager sTheGlobalBehavior = null; // Single pattern



    // teacher vars
    /*    public GameObject mMyTarget = null;
        public SliderWithEcho mTurnRate = null;
        private const float kMySpeed = 5f;*/


    // Start is called before the first frame update
    void Start()
    {
        generateWaypoint = FindObjectOfType<GenerateWaypoint>();

        capTimer = FindObjectOfType<CapTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hid = !hid;
            generateWaypoint.showHideHuts(hid);
            GameMechanics.sTheGlobalBehavior.UpdateHiddenHuts(hid);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ifOriginal = !ifOriginal;

            generateWaypoint.changeWaypoints();
            GameMechanics.sTheGlobalBehavior.updateEnemyCords();
            GameMechanics.sTheGlobalBehavior.UpdateWaypointType(ifOriginal);
        }


        // Q quit key check    
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnityEngine.Debug.Log("Q-key pressed: Quiting Application");
            Application.Quit();


        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            ifRandom = !ifRandom;
            GameMechanics.sTheGlobalBehavior.UpdateEnemyPath(ifRandom);
        }


        // Keyboard control
        if(!ifMouse /*&& Input.GetAxis("Vertical") > 0*/)
        {
            // move in rotational directon
            //transform.up = transform.up * mHeroSpeed * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, transform.position.y + (mHeroSpeed * Time.deltaTime), transform.position.z);
            //transform.Translate(transform.up * (mHeroSpeed * Time.deltaTime));


            //transform.Translate(transform.up * (mHeroSpeed * Time.deltaTime));

            Vector3 Position = transform.position;
            Vector3 Velocity = new Vector3(0, /*Input.GetAxis("Vertical") **/ mHeroSpeed * Time.deltaTime);


            if(Input.GetAxis("Vertical") > 0) // w
            {
                mHeroSpeed += Time.deltaTime * 2;
            }
            
            if (Input.GetAxis("Vertical") < 0 && mHeroSpeed > 0) // s
            {
                mHeroSpeed -= Time.deltaTime * 2;
            }

            Position += rot * Velocity;
            transform.position = Position;

            //transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }


        // Mouse control                                                                                DONE
        else if (ifMouse /*&& UnityEngine.Input.GetMouseButtonDown(0)*/)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }




        // M key toggles between mouse or keyboard control                                              DONE
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ifMouse)
            {
                UnityEngine.Debug.Log("M-key pressed: Switching from mouse to keyboard input");
                ifMouse = false;
            }
            else
            {
                UnityEngine.Debug.Log("M-key pressed: Switching from keyboard to mouse input");
                ifMouse = true;
            }

            GameMechanics.sTheGlobalBehavior.UpdateInput(ifMouse);
        }


        // A D rotate hero check
        rot = transform.rotation;

        z = rot.eulerAngles.z;

        z -= Input.GetAxis("Horizontal") * mRotationSpeed * Time.deltaTime;

        rot = Quaternion.Euler(0, 0, z);

        transform.rotation = rot;

        //
        if (Input.GetKey(KeyCode.Space) /*|| UnityEngine.Input.GetMouseButtonDown(0)*/)
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                //GameObject e = Instantiate(Resources.Load("Prefabs/capA") as GameObject); 
                GameObject e = Instantiate(cap, capPos.position, Quaternion.identity);
                e.transform.localPosition = transform.localPosition;
                e.transform.up = transform.up;
                mTotalCapCount++;

                // start timer shrink
                capTimer.changeHealth(0.2f);
            }
        }

/*        // update egg count text
        mCapCountEcho.text = mHero.EggStatus();*/
    }


/*    private void Awake()
    {
        if(GameMechanics.sTheGlobalBehavior == null) 
        {
            GameMechanics.sTheGlobalBehavior = this; // Singleton pattern
        }
    }*/

    private void DirectionPointer (Vector3 p, float r)
    {
        Vector3 v = p - transform.localPosition;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    public void SubtractCap() 
    {
        mTotalCapCount--;
    }

/*    public string CapCount()
    {
        return "Eggs on screen: " + mTotalCapCount;
    }*/

    /*public string GetIfMouse()
    {
        if(ifMouse)
        {
            return "Mouse";
        }
        else
        {
            return "Keyboard";
        }
    }*/

/*    void OnTriggerEnter2D(Collider2D collision)
    {

    }*/

/*    void OnCollisionEnter2D(Collision2D collision)
    {

    }*/
}
