using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Activates children of this object on trigger enter
 **/
public class Activator : MonoBehaviour {

    // If false, deactivates all children of this object
    public bool activeOnStart;

    private void Start()
    {
        if (!activeOnStart)
            SetChildrenActive(false);
        else
            SetChildrenActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            SetChildrenActive(true);
    }

    private void SetChildrenActive(bool v)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(v);
    }
}
