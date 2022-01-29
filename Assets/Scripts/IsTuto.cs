using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTuto : MonoBehaviour
{

    public bool is_tuto = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setTrue()
    {
        is_tuto = true;
    }

    public void setFalse()
    {
        is_tuto = false;
    }
}
