using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    public static PlayerController Instance;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private TrailRenderer trail;

    private bool facingLeft = false;
    private bool isDashing = false;
    private Animator myAnimator;
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRender;
    private Vector2 movement;
    private float startingMoveSpeed;

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenLoc = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenLoc.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }
    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trail.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        trail.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
}
