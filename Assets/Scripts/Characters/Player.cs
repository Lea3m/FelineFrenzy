using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spawner<PlayableCharacter>
{
    public Animator animator;
    public LayerMask trapsLayer;

   
 
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
            ChangeToNextSpawnable();
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
}