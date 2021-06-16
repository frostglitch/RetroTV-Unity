using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPlatform : MonoBehaviour
{
    public Animator anim;
    public ColliderCheckLayer colliderCheck;

    public float waitForDown = 0.5f;

    bool isUp;

    private void Update()
    {
        if (colliderCheck.check && !isUp)
        {
            anim.SetInteger("state", 0);
            isUp = true;
        }
        else if (!colliderCheck.check && isUp)
        {
            StartCoroutine(WaitBeforeDown());
        }
    }

    IEnumerator WaitBeforeDown()
    {
        yield return new WaitForSeconds(waitForDown);

        if (!colliderCheck.check)
        {
            anim.SetInteger("state", 1);
            isUp = false;
        }
    }
}
