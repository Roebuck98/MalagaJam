using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<UnlockDoors> doors;
    private int enemiesCleared;
    void Start()
    {
        enemiesCleared = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
            if(enemy == null)
            {
                enemiesCleared++;
            }

        if(enemiesCleared == enemies.Count)
        {
            foreach (UnlockDoors door in doors)
                door.Unlock();

        }

        enemiesCleared = 0;
    }

    public bool AllEnemiesDefeated()
    {
        bool t = true;
        foreach (GameObject enemy in enemies)
            if (enemy != null)
            {
                return false;
            }
        return t;
        /*
        if (enemies.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }

    /*void Update2()
    {
       
        if (enemies.All(x => x == null))
        {
            foreach (UnlockDoors door in doors)
                door.Unlock();
        }

    }*/
}
