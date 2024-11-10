using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Spawner<PlayableCharacter>
{
    public Animator animator;
    public LayerMask trapsLayer;

    public GameManager gameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private PlayableCharacter activeCharacter
    {
        get { return currentSpawnable; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeToNextCharacter();

        }
        if (Input.GetKeyDown(KeyCode.E) && IsActiveCharacterSet())
        {
            activeCharacter.SpecialAbillity();
        }
    }


    void FixedUpdate()
    {
        if (IsActiveCharacterSet())
        {
            if ((Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D)))
            {
                activeCharacter.Movement();
            }
            else 
            {
                activeCharacter.Idle();
            }
        }
    }

    private bool IsActiveCharacterSet()
    {
        if (activeCharacter == null)
        {
            Debug.LogError("Active Character Not Set.");
            return false;
        }
        return !activeCharacter.IsDead();
    }

    private void ChangeToNextCharacter() 
    {
        int numOfCharacters = spawnables.Count;
        for (int i = 0; i < numOfCharacters; i++)
        {
            PlayableCharacter next= GetNextSpawnable();
            if (!next.IsDead())
            {
                StartCoroutine(next.ActivateInvincibility(1f));
                Swap(next);
                return;
            }
        }
        gameManager.RestartGame();
    }

}