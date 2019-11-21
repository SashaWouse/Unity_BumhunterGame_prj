using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private GameObject key;

    void Start()
    {
        key = GameObject.Find("Key");
        key.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            key.gameObject.SetActive(true);

            Destroy(gameObject, 0.4f);
        }
    }
}

