using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    AudioManager audioManagaer;

    PlayerHealth playerHealth;

    public float healthBonus = 10f;

    void Start()
    {
        audioManagaer = AudioManager.instance;
    }

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerHealth.curHealth <= playerHealth.maxHealth)
        {
            Destroy(gameObject, 0.2f);

            playerHealth.curHealth = playerHealth.curHealth + healthBonus;

            audioManagaer.PlaySound("Boost");
        }
    }
}
