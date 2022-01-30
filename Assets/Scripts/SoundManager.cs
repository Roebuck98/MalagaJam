using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource step;
    public AudioSource shot;
    

    public void PlayStep()
    {
        step.pitch = 0.9f + 0.2f * Random.value; 
        step.PlayOneShot(step.clip);
    }

    public void PlayShot()
    {
        Debug.Log("t");
        shot.Play();
    }





    
}
