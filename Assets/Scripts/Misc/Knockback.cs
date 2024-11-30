using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackTime = .2f;
    public bool gettingKnockedBack { get; private set; }
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform dmgSrc, float knockBackThrust)
    {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - dmgSrc.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        gettingKnockedBack = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
