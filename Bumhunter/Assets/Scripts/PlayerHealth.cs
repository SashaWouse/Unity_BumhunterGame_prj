using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    [SerializeField]
    GameObject DeathUI;

    Animator anim;

    public float maxHealth = 100;
    public float curHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        healthBar.value = maxHealth;
        curHealth = healthBar.value;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            healthBar.value -= .2f;
            curHealth = healthBar.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
        {
            //play death animation
            anim.SetBool("isDead", true);

            //stop all player movement
            GetComponent<PlayerController>().enabled = false;

            //enables the DeathUI
            DeathUI.gameObject.SetActive(true);
        }
    }
}
