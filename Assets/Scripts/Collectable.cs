using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public CollectableStates CollectableStates;

    public IntVariable CollectableCounter;

    public int id;

    private bool collected;

    private void Start()
    {
        collected = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            if (collision.gameObject.tag == "Player")
            {
                CollectableCounter.RuntimeValue++;
                CollectableCounter.InitialValue = CollectableCounter.RuntimeValue;
                collected = true;
                CollectableStates.Collected[id] = collected;
                this.gameObject.SetActive(false);
            }
        }
    }

    public bool Collected
    {
        get
        {
            return collected;
        }
    }

    private void OnDrawGizmosSelected()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Collectable");
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<Collectable>().id = i;
    }
}
