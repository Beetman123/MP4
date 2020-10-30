using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool ifMouse = true;
    //int rotation = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Q quit key check
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q-key pressed: Quiting Application");
            Application.Quit();
        }



        // M key toggles between mouse or keyboard control
        if (Input.GetKey(KeyCode.M))
        {
            if(ifMouse)
            {
                Debug.Log("M-key pressed: Switching from mouse to keyboard input");

            }
            else
            {
                Debug.Log("M-key pressed: Switching from keyboard to mouse input");

            }

        }


        // Update Output


    }
}
