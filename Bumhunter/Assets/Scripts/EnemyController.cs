using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float fireRate;
    float nextFire;

    public float health = 50f;

    public Transform target;

    public float engaugeDistance = 10f;

    public float attackDistance = 1f;

    public float moveSpeed = 3f;

    private bool facingLeft = true;

    private Animator anim;

    public PlayerController playerController;

    public float attackDamage = .2f;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);

        if (Vector3.Distance(target.position, this.transform.position) < engaugeDistance)
        {
            anim.SetBool("isIdle", false);

            //get the direction of the target
            Vector3 direction = target.position - this.transform.position;

            if (Mathf.Sign(direction.x) == 1 && facingLeft)
            {
                Flip();
            }
            else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
            {
                Flip(); 
            }

            if (direction.magnitude >= attackDistance)
            {
                anim.SetBool("isWalking", true);

                Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

                if (facingLeft)
                {
                    this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
                }
                else if (!facingLeft)
                {
                    this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
                }
            }

            if (direction.magnitude < attackDistance)
            {
                Debug.DrawLine(target.transform.position, this.transform.position, Color.red);

                anim.SetBool("isAttacking", true);

                playerController.GetComponentInChildren<PlayerHealth>().curHealth -= attackDamage;
            }
        }
        else if (Vector3.Distance(target.position, this.transform.position) > engaugeDistance)
        {
            //do nothing
            Debug.DrawLine(target.position, this.transform.position, Color.green);
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 25;

            if (health <= 0)
            {
                anim.SetBool("isDead", true);
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
