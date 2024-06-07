using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;

   [SerializeField] private Animator anim;
   [SerializeField] private Collider2D coll;
   [SerializeField] private LayerMask ground;
   [SerializeField] private float speed = 5f;
   [SerializeField] private float jumpForce = 5f;
   [SerializeField] private int cherries = 0;
   [SerializeField] private Text cherryText;
   [SerializeField] private float hurtForce = 6f;
   private AudioSource footstep;


   private enum State { idle, running, jumping, falling, hurt };
   private State state = State.idle;


   private void Start()
   {
      rb.GetComponent<Rigidbody2D>();
      anim.GetComponent<Animator>();
      coll.GetComponent<Collider2D>();
      footstep = GetComponent<AudioSource>();

   }
   private void Update()
   {
      if (state != State.hurt)
      {
         SetVelocityState();
      }
      SetAnimationState();


      anim.SetInteger("state", (int)state);
   }

   private void SetVelocityState()
   {

      float hDirection = Input.GetAxis("Horizontal");

      if (hDirection < 0)
      {
         rb.velocity = new Vector2(-speed, rb.velocity.y);
         rb.transform.localScale = new Vector2(-1, 1);

      }

      else if (hDirection > 0)
      {
         rb.velocity = new Vector2(speed, rb.velocity.y);
         rb.transform.localScale = new Vector2(1, 1);

      }


      if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
      {
         jump();
      }
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.tag == "Collectable")
      {
         Destroy(collision.gameObject);
         cherries += 1;


         cherryText.text = cherries.ToString();
      }

   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      Enemy enemy = other.gameObject.GetComponent<Enemy>();
      if (other.gameObject.tag == "Enemy")
      {

         if (state == State.falling)
         {
            enemy.JumpedOn();
            jump();
         }

         else
         {
            state = State.hurt;
            if (other.gameObject.transform.position.x > transform.position.x)  //enemy to the right,I move left
            {
               rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }

            else                                                                  //enemy to the left, I move right

            {
               rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
         }
      }
   }

   private void jump()
   {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
      state = State.jumping;
   }
   private void SetAnimationState()

   {
      if (state == State.jumping)
      {
         if (rb.velocity.y < .1f)
         {
            state = State.falling;
         }
      }

      else if (state == State.falling)
      {
         if (coll.IsTouchingLayers(ground))
         {
            state = State.idle;
         }
      }
      else if (state == State.hurt)
      {
         if (Mathf.Abs(rb.velocity.x) < .1f)
         {
            state = State.idle;
         }
      }
      else if (Mathf.Abs(rb.velocity.x) > 2f)
      {
         //moving
         state = State.running;
      }
      else
      {
         state = State.idle;
      }
   }

   private void Footstep()
   {
      footstep.Play();
   }

}

