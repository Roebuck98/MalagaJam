using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<RoomManager> rooms;
    public GameEnding winScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAllRooms())
        {
            winScreen.AllEnemiesClear();
        }
    }

    bool CheckAllRooms()
    {
        int count = 0;  
        foreach (RoomManager room in rooms) {
            if(room.AllEnemiesDefeated())
                count++;
        }
        if(count == rooms.Count) {
            return true;
        }
        else
        {
            return false;
        }
            
            
    }
}
