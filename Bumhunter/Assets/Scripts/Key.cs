using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    AudioManager audioManagaer;

    [SerializeField]
    GameObject keyOn;

    [SerializeField]
    GameObject keyOff;

    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManagaer = AudioManager.instance;

        //set the key to off sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = keyOff.GetComponent<SpriteRenderer>().sprite;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //set the key to on sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = keyOn.GetComponent<SpriteRenderer>().sprite;

        //set the isOn to true when triggered
        isOn = true;

        audioManagaer.PlaySound("Key Sound");
    }
}
