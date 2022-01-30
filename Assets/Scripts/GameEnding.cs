using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
	public float fadeDuration = 1f;
	public float displayImageDuration = 1f;
	//public GameObject player;
	public CanvasGroup winBackgroundImageCanvasGroup;
	public CanvasGroup looseBackgroundImageCanvasGroup;
	public AudioSource winAudio;
	public AudioSource looseAudio;

	bool winCondition;
	bool looseCondition;
	float m_Timer;
	bool m_HasAudioPlayed;

	/*void OnTriggerEnter (Collider other)
    {
	if (other.gameObject == player)
        {
		winCondition = true;
        }
    }*/

  public void CaughtPlayer ()
    {
        looseCondition = true;
    }
    public void AllEnemiesClear ()
    {
        winCondition = true;
    }

	void Update ()
    {
	if(winCondition)
        {
		EndLevel (winBackgroundImageCanvasGroup, false, winAudio);
        }
	else if(looseCondition)
    	{
        	EndLevel (looseBackgroundImageCanvasGroup, true, looseAudio);
    	}
    }

	void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        imageCanvasGroup.enabled = true;
        audioSource.enabled = true;
	if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

	m_Timer += Time.deltaTime;

	imageCanvasGroup.alpha = m_Timer / fadeDuration;

	if(m_Timer > fadeDuration + displayImageDuration)
        {
		if (doRestart)
            {
		SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}
