using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator myAnimator;
    private PlayerControls playerControls;

    private void Attack()
    {
        // Fire sword animation
        myAnimator.SetTrigger("Attack");
    }
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
