using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody2d;
    private BoxCollider2D collider2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private LayerMask groundMask;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;


    // Start is called before the first frame update
    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidBody2d.velocity = new Vector2(dirX * moveSpeed, rigidBody2d.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigidBody2d.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rigidBody2d.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("movementState", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, .1f, groundMask);
    } 
}
