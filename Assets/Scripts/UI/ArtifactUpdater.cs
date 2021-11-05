using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactUpdater : MonoBehaviour {

    public PlayerProgression PlayerProgression;

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        { 
            if (PlayerProgression.LevelsCompleted > i)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

    }
}
