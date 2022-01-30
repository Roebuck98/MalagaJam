using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    public bool hide;
    void Start()
    {
        if (hide)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
