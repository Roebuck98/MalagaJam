using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoors : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Unlock();
    }*/

    public void Unlock()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

}
