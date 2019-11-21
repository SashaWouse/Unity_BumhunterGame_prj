using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] key;

    [SerializeField]
    GameObject exitDoor;

    int noOfKeys = 0;

    [SerializeField]
    Text keyCount;

    public static GameManager gm;

    void Awake()
    {
        gm = this;
    }

    public int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel(int x)
    {
        SceneManager.LoadScene(x);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNoOfKeys();
    }

    public int GetNoOfKeys()
    {
        int x = 0;

        for (int i = 0; i < key.Length; i++)
        {
            if (key[i].GetComponent<Key>().isOn == false)
            {
                x++;
            }
            else if (key[i].GetComponent<Key>().isOn == true)
            {
                noOfKeys--;
            }
        }
            noOfKeys = x;

            return noOfKeys;
    }

    public void GetExitDoorState()
    {
        if (noOfKeys <= 0)
        {
            exitDoor.GetComponent<Door>().OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        keyCount.text = GetNoOfKeys().ToString();

        GetExitDoorState();
    }
}
