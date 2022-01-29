using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerManager : MonoBehaviour
{
    public GameObject character;

    public Transform spawnPointTuto;
    public Transform spawnPointNoTuto;
    public MultiPlayer multiplayer;
    void Awake()
    {
        IsTuto temp = null;
        try
        {
            temp = GameObject.FindObjectOfType<IsTuto>();
            if (temp.is_tuto)
            {
                //Debug.Log("Entra aqui");
                character.transform.position = spawnPointTuto.position;

            }
            else
            {
                //Debug.Log("Entra aqui");
                character.transform.position = spawnPointNoTuto.position;
            }

        }
        finally
        {
            if (temp != null)
                Object.DestroyImmediate(temp);
        }
    }

    
}
