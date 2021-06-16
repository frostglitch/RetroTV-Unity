using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHashtag : MonoBehaviour
{
    public Transform head;
    [Space]
    public float speed;
    public float chargeSpeed;
    public float stopWaitTime;
    [Space]
    public ColliderCheck playerColliderCheck;
    public ColliderCheck stopColliderCheck;

    bool facingRight;
    bool charging;
    float speedX;
    bool stopped;

    Animator anim;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Left n Right
        if (!stopped)
        {
            if(charging)
            {
                if (facingRight) speedX = chargeSpeed;
                else speedX = -chargeSpeed;
            }
            else
            {
                if (facingRight) speedX = speed;
                else speedX = -speed;
            }
        }


        //Collider checks
        if (playerColliderCheck.checkedLayer == 9) charging = true;
        if (stopColliderCheck.checkedLayer == 8 && !stopped && !charging) StartCoroutine(EnemyStop());


        //Animations
        if(!stopped)
        {
            if (charging) anim.SetTrigger("Charge");
            else anim.SetTrigger("Walk");
        }
        else anim.SetTrigger("Idle");


        //move the enemy
        rb.velocity = new Vector2(speedX, rb.velocity.y);


        //if charging, disable gravity
        if(charging)
        {
            rb.gravityScale = 0f;
        }
    }

    IEnumerator EnemyStop()
    {
        //stop the enemy
        stopped = true;
        speedX = 0;
        
        yield return new WaitForSeconds(stopWaitTime);

        //flip
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        //start moving
        stopped = false;
    }
}
