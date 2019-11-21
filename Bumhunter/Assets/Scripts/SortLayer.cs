using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    //name of the sorting layer
    public string sortLayerName;

    // Start is called before the first frame update
    void Start()
    {
        //get each of the sprites that are a child of the game object that the script is attached to
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            //set those sprites sorting layer names to the one we have specified
            sr.GetComponent<Renderer>().sortingLayerName = sortLayerName;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
