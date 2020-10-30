using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanics : MonoBehaviour
{
    public static GameMechanics sTheGlobalBehavior = null;

    public HeroMechanics mHero = null;

    public Text CapCountText = null;
    public Text enemysDestroyedText = null;
    public Text enemyCount = null;
    public Text inputText = null;
    public Text enemyPath = null;
    public Text waypointSetup = null;
    public Text hiddenHuts = null;


    public int enemysDestroyed = 0;
    public int enemys = 10;
    public int currentCaps = 0;

    private void Awake()
    {
        if (GameMechanics.sTheGlobalBehavior == null)
        {
            GameMechanics.sTheGlobalBehavior = this;
        }
        else
        {
            // Should print out error
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateEnemyCount(bool add)
    {
        if (add)
        {
            enemys++;
        }
        else
        {
            enemys--;
        }

        enemyCount.text = "Enemy Count: " + enemys;
    }


    public void AddCapCount()
    {
        currentCaps++;
        CapCountText.text = "(Space) Caps: " + currentCaps;
    }

    public void RemoveCapCount()
    {
        if(currentCaps > 0)
            currentCaps--;

        CapCountText.text = "(Space) Caps: " + currentCaps;
    }

    public void UpdateEnemysDestroyed()
    {
        enemysDestroyed++;
        enemysDestroyedText.text = "Enemys Destroyed: " + enemysDestroyed;
        UpdateEnemyCount(false);
    }

    public void UpdateInput(bool mouseControl)
    {
        if (mouseControl)
        {
            inputText.text = "(M) Controller: Mouse";
        }
        else
        { 
            inputText.text = "(M) Controller: Keyboard";
        }
    }

    public void UpdateEnemyPath(bool pathRandom) // NEED TO CALL AND SET
    {
        if (pathRandom)
        {
            enemyPath.text = "(J) Enemy Path: Random";
        }
        else
        {
            enemyPath.text = "(J) Enemy Path: Order";
        }
    }

    public void UpdateWaypointType (bool originalPath) // NEED TO CALL AND SET
    {
        if (originalPath)
        {
            waypointSetup.text = "(C) Waypoint Setup 1";
        }
        else
        {
            waypointSetup.text = "(C) Waypoint Setup 2";
        }
    }

    public void UpdateHiddenHuts (bool hutsHidden) // NEED TO CALL AND SET
    {
        if (hutsHidden)
        {
            hiddenHuts.text = "(H) Huts Hid: True";
        }
        else
        {
            hiddenHuts.text = "(H) Huts Hid: False";
        }
    }

    public void updateEnemyCords()
    {
        //EnemyMovement[] enemyMovement = FindObjectsOfTypeAll(EnemyMovement);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyMovement>().NewCoordinates();
        }
    }

}
