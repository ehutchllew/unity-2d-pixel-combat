using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockbackThrust = 15f;
    [SerializeField] private int startingHealth = 3;

    private int currentHealth;
    private Flash flash;
    private Knockback knockback;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private IEnumerator CheckTriggerDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        StartCoroutine(flash.FlashRoutine());
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockbackThrust);
        StartCoroutine(CheckTriggerDeathRoutine());
    }

    private void TriggerDeath()
    {
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
