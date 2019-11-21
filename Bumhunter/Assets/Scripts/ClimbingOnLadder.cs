﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingOnLadder : MonoBehaviour
{
    public float speed = 0;

    void OnTriggerStay2D(Collider2D other)
    {
       if (other.tag == "Player" && Input.GetKey(KeyCode.W))
       {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
       }
       else if (other.tag == "Player" && Input.GetKey(KeyCode.S))
       {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
       }
       else
       {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
    }
}
