using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.3f;
    [SerializeField] private Transform weaponCollider;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;
    private Animator myAnimator;
    private PlayerController playerController;
    private PlayerControls playerControls;
    private GameObject slashAnim;

    private void Attack()
    {
        isAttacking = true;
        // Fire sword animation
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = transform.parent;

        StartCoroutine(AttackCDRoutine());
    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }
    private void Awake()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        playerControls = new PlayerControls();
    }

    private void DoneAttackAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenLoc = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y - playerScreenLoc.y, Mathf.Abs(mousePos.x - playerScreenLoc.x)) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenLoc.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        if (mousePos.x > playerScreenLoc.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollowWithOffset();
        if (attackButtonDown && !isAttacking)
        {
            Attack();
        }
    }
}
