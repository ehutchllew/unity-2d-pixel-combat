using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private int currentHealth;
    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);

        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        Destroy(gameObject);
    }
}
