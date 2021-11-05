using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {

    public int id;

    public GameObjectRef GameObjectRef;

    public PlayerProgression PlayerProgression;

    public GameEvent ActivationEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObjectRef.TheObject = this.gameObject;
            PlayerProgression.CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerProgression.CurrentCheckPoint = id;
            if (PlayerProgression.CurrentLevel > PlayerProgression.LevelsCompleted + 1)
                PlayerProgression.LevelsCompleted = PlayerProgression.CurrentLevel;
            ActivationEvent.Raise();
        }
    }

    public int GetID()
    {
        return id;
    }

    public void PlayAudioClip(AudioSource audioSource, AudioClip audioClip = null, bool loop = false)
    {
        if (loop)
            audioSource.Play();
        else
            audioSource.PlayOneShot(audioClip);
    }

    public void PlayAnimationClip(Animator anim, string clipName)
    {
    }

}
