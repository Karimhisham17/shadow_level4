using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer : MonoBehaviour
{
    private Animator anim; 

    
    void Start()
    {
       
        anim = GetComponent<Animator>();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (anim != null)
            {
                anim.SetBool("haxe", true);
            }
        }
    }


}