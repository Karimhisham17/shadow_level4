using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAWK_Run : StateMachineBehaviour
{
    public float attackRange = 3f; // المسافة اللي يضرب فيها
    Transform player;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        // هنا بس بنشوف المسافة عشان نشغل الضرب
        // ملناش دعوة بالحركة، سكريبت HAWK هو اللي بيحرك

        float distance = Vector2.Distance(player.position, rb.position);

        if (distance <= attackRange)
        {
            // لو قرب كفاية، وقف الحركة وهجم
            if (rb != null) rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}