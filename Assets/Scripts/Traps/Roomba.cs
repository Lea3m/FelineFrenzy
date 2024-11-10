using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : Trap
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Die()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        base.Die();
    }
}
