using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //
    Animator anim;

    GameManager gm;

    //is entry or exit door
    [SerializeField]
    GameObject DoorType;

    //track the state of the door
    int stateOfDoor = 1;

    public int nextLevel;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GetDoorState() == 3)
        {
            gm.LoadNextLevel(nextLevel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        //initialize the animator
        anim = GetComponent<Animator>();

        //set entry door to open
        if (DoorType.name == "EntryDoor")
        {
            OpenDoor();
        }

        //set exit door to closed
        if (DoorType.name == "ExitDoor")
        {
            LockDoor();
        }
    }

    //function to lock the door and set it's state
    public void LockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 1);
            stateOfDoor = 1;
        }
    }

    //function to unlock the door and set it's state
    public void UnlockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 2);
            stateOfDoor = 2;
        }
    }

    //function to open the door and set it's state
    public void OpenDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 3);
            stateOfDoor = 3;
        }
    }

    //function to set the state of the door 
    public void SetDoorState(int state)
    {
        if (state == 1 && DoorType.name == "ExitDoor")
        {
            LockDoor();
        }

        if (state == 2 && DoorType.name == "ExitDoor")
        {
            UnlockDoor();
        }

        if (state == 3 && DoorType.name == "ExitDoor")
        {
            OpenDoor();
        }
    }

    //function to the current door state
    public int GetDoorState()
    {
        return stateOfDoor;
    }

}
