using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private ActiveWeapon activeWeapon;
    private Animator myAnimator;
    private PlayerController playerController;
    private PlayerControls playerControls;

    private void Attack()
    {
        // Fire sword animation
        myAnimator.SetTrigger("Attack");
    }
    private void Awake()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        playerControls = new PlayerControls();
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenLoc = Camera.main.WorldToScreenPoint(playerController.transform.position);

        if (mousePos.x < playerScreenLoc.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        if (mousePos.x > playerScreenLoc.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
        MouseFollowWithOffset();
    }
}
