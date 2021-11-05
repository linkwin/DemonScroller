using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Handles player respawn and Game initialization.
 */
public class GM : MonoBehaviour {

    public GameObjectRef GameObjectRef;//testing general event system

    [Tooltip("Reference to persistant player data in Scriptable Object")]
    public PlayerProgression PlayerProgress;

    [Tooltip("Reference to SO data tracking which collectables have been collected")]
    public CollectableStates Collectables;

    private GameObject player;// ref to player

    private GameObject[] collectableGameObjects;//temp variable for disabling collected chains

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        collectableGameObjects = GameObject.FindGameObjectsWithTag("Collectable");
        InitCollectables();
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    private void InitCollectables()
    {
        bool[] collected = Collectables.Collected;
        collectableGameObjects = SortById(collectableGameObjects);
        for (int i = 0; i < collectableGameObjects.Length; i++)
        {
            if (collected[i])
                collectableGameObjects[i].SetActive(false);
        }
    }

    private GameObject[] SortById(GameObject[] collectableGameObjects)
    {
        GameObject[] temp = (GameObject[])collectableGameObjects.Clone();
        for (int i = 0; i < collectableGameObjects.Length; i++)
        {
            temp[collectableGameObjects[i].GetComponent<Collectable>().id] = collectableGameObjects[i];
        }
        return temp;
    }

    public void Respawn()
    {

    }
    
    public void PlayAnimationClip(string clip)
    {
        GameObjectRef.TheObject.GetComponent<Animator>().SetBool(clip, true);
    }

    public void CheckpointActivated()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckPoint cp = checkpoints[i].GetComponent<CheckPoint>();
            Animator cpAnim = checkpoints[i].GetComponent<Animator>();
            if (cp.id == PlayerProgress.CurrentCheckPoint)
                cpAnim.SetBool("Active", true);
            else
                cpAnim.SetBool("Active", false);
        }
    }

}
