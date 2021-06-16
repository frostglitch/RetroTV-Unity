using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool on;
    public bool off;
    public Color onColor;
    public Color offColor;
    [Space]
    public float spinSpeedStart = 13;
    public float spinSpeedEnd = 1;
    [Space]
    public Transform spin;
    public SpriteRenderer srY;

    Animator anim;
    SpriteRenderer sr;

    bool startSpin;
    float spinSpeed;
    bool startSpin2;

    void Start()
    {
        anim = spin.GetComponent<Animator>();
        sr = spin.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(startSpin)
        {
            if(spinSpeed <= spinSpeedEnd + 0.1f)
            {
                spinSpeed = spinSpeedEnd;  
                startSpin = false;
                startSpin2 = true;
            }
            else
            {
                spinSpeed = Mathf.Lerp(spinSpeed, spinSpeedEnd, Time.deltaTime);

                spin.Rotate(Vector3.forward, spinSpeed);
            }
        }

        if (startSpin2) spin.Rotate(Vector3.forward, spinSpeed);

        if (Input.GetKeyDown(KeyCode.Space)) TurnOff();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            //player

            if(!on && !off)
            {
                StartCoroutine(TurnOn());
            }
        }
    }

    IEnumerator TurnOn()
    {
        anim.SetTrigger("On");

        yield return new WaitForSeconds(0.5f);

        on = true;

        sr.color = onColor;

        spinSpeed = spinSpeedStart;
        startSpin = true;
    }

    public void TurnOff()
    {
        anim.SetTrigger("Off");
        sr.color = offColor;
        srY.color = offColor;

        startSpin2 = false;
        on = false;
        off = true;

        spin.localEulerAngles = Vector3.zero;
    }
}
