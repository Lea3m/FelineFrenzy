using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public HealthBar healthBar;
    public Sprite ghostSprite;

    [SerializeField, Tooltip("Speed of the character.")]
    protected int moveSpeed;

    protected int basicAttack = 10;
    protected int SpecialAttack = 50;

    protected int maxHp = 50;
    protected int currentHp;

    private Rigidbody2D playerRb;
    public Animator animator;

    private Vector2 input; // Store movement direction

    public RuntimeAnimatorController animatorController; // Reference to the Animator Controller for this character
    public int characterID; // Unique ID for each character

    void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
        currentHp = maxHp;
        healthBar.SetMaxHealth(maxHp);

        // Ensure Animator component is set
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on PlayableCharacter.");
        }
    }

    public void TakeDamage(int damagePoints)
    {
        currentHp = Mathf.Max(0, currentHp - damagePoints);
        healthBar.SetHealth(currentHp);

        // Play damage reaction animation if it exists
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }

        if (currentHp == 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
      
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            float speed = input.magnitude;
            animator.SetFloat("speed", speed);

            if (speed == 0)
            {
                animator.SetBool("IsMoving", false);
            }
            else
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                animator.SetBool("IsMoving", true);

                Movement();
            }
        }



        public void Movement()
    {
        if (IsPlayerRbSet())
        {
            // Move the character based on the input direction
            Vector2 moveDirection = input.normalized;
            playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void Die()
    {
        // Show ghost sprite on death
        GetComponent<SpriteRenderer>().sprite = ghostSprite;

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }

    public bool IsDead()
    {
        return currentHp == 0;
    }

    public abstract void SpecialAbillity();

    protected void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(basicAttack);
    }

    protected Trap FindNearbyTrap()
    {
        Collider2D trap = Physics2D.OverlapCircle(transform.position, 1.0f, LayerMask.GetMask("Traps"));
        if (trap != null)
        {
            return trap.GetComponent<Trap>();
        }
        return null;
    }

    private bool IsPlayerRbSet()
    {
        if (playerRb == null)
        {
            Debug.LogError("Player Rigid Body Not Set.");
            return false;
        }
        return true;
    }
}
