using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for playable characters in the game.
/// </summary>
public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public HealthBar healthBar;
    public Sprite ghostSprite;
    public RuntimeAnimatorController animatorController;

    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip hurtSound;

    [SerializeField, Tooltip("Speed of the character.")]
    protected int moveSpeed;

    protected int basicAttack = 10;
    protected int SpecialAttack = 50;

    protected int maxHp = 50;
    protected int currentHp = -1;

    protected Animator animator;

    private Rigidbody2D playerRb;

    private bool isMoving = false;

    private bool isInvincible = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
        // Assign the character's specific Animator Controller to the Animator
        animator.runtimeAnimatorController = animatorController;
    }
    void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
        currentHp = maxHp;
        healthBar.SetMaxHealth(maxHp);
    }

    public void TakeDamage(int damagePoints)
    {
        if (isInvincible) return;
        HurtSound();
        currentHp = Mathf.Max(0, currentHp - damagePoints);
        healthBar.SetHealth(currentHp);
        if (currentHp == 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        GetComponent<SpriteRenderer>().sprite = ghostSprite;
        animator.SetBool("isMoving", false);
        animator.SetBool("isDead", true);
    }

    public void Movement()
    {
        if (IsPlayerRbSet())
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(moveHorizontal, moveVertical).normalized;
            playerRb.MovePosition(playerRb.position + direction * moveSpeed * Time.deltaTime);

            if (animator != null)
            {
                isMoving = true;
                animator.SetBool("isMoving", true);
                animator.SetFloat("moveX", moveHorizontal);
                animator.SetFloat("moveY", moveVertical);
            }
        }
    }

    public void Idle()
    {
        if (isMoving)
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
        }
    }

    public bool IsDead()
    {
        return currentHp == 0;
    }

    public abstract void SpecialAbillity();

    public IEnumerator ActivateInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

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

    protected void AttackSound()
    {
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    protected void HurtSound()
    {
        if (hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
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
