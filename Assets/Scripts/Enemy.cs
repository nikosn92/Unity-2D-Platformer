using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    protected virtual void Start() 
    {   
    
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        
    }
  public void JumpedOn() // methodos gia thanato vatraxou
   {
       anim.SetTrigger("Death");
       
   }
   private void Death()
   {
       Destroy(this.gameObject);
   }
}
