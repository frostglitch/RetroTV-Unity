using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    public Transform wheel;
    public ColliderCheckLayer groundCheck;
    public float spinSpeed = 3f;
    public float wheelStopSpeed = 1f;
    [Space]
    public float speed = 3f;
    public float jumpForce = 600f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space]
    public Transform head;
    public float headBobAmplitude = 1f;
    public float headBobFrequency = 1f;
    public float headYoffset = 0.5f;
    [Space]
    public Camera cam;
    public float fovSpeed = 2f;
    public float minFov;
    public float maxFov;
    [Space]
    public CameraShake shake;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.4f;
    public CameraEffectsControl cameraEffects;
    public float chromaticSpeed = 1f;
    public float chromaticMagnitude = 1f;





    float nextFov;
    float horiz = 0;

    bool canJump;
    bool jumpRequest;


    Rigidbody2D rb;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (groundCheck.check) canJump = true;
        else canJump = false;


        //Horizontal input
        horiz = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) horiz = -1;
        if (Input.GetKey(KeyCode.RightArrow)) horiz = 1;


        //moving the player
        float speedX = horiz * speed;
        rb.velocity = new Vector2(speedX, rb.velocity.y);





        //check for jump
        if(Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            jumpRequest = true;

            //shake.Shake(shakeDuration, shakeMagnitude);
            //cameraEffects.ChromaticBurst(chromaticSpeed, chromaticMagnitude);
        }













        //head bobbing
        float headY = Mathf.Sin(Time.time * headBobFrequency) * headBobAmplitude;
        head.localPosition = new Vector2(0, headY + headYoffset);


        //camera fov change
        if (speedX == 0) nextFov = minFov;
        else nextFov = maxFov;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, nextFov, Time.deltaTime * fovSpeed);


        //spinning the wheel
        if (speedX > 0) wheel.Rotate(Vector3.forward, -spinSpeed);
        else if (speedX < 0) wheel.Rotate(Vector3.forward, spinSpeed);
    }

    private void FixedUpdate()
    {
        if(jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }

        if (rb.velocity.y < 0)
        {
            //falling
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    IEnumerator ExtraTimeForJump(float t)
    {
        yield return new WaitForSeconds(t);

        canJump = false;
    }
}
