using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberController : MonoBehaviour {

    private void Awake()
    {
        SetEnabled(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SetEnabled(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SetEnabled(false);
    }

    private void SetEnabled(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject item = transform.GetChild(i).gameObject;
            if (item.tag != "Collectable" && item.tag != "Checkpoint")
                item.SetActive(enable);
        }
    }
}
