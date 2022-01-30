using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource step;
    public AudioSource shot;
    
    void Start()
    {
        step = GetComponent<AudioSource>();
        shot = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        step.pitch = 0.9f + 0.2f * Random.value; 
        step.Play();
    }

    public void PlayShot()
    {
        shot.Play();
    }





    
}
