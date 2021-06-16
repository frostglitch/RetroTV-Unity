using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    [HideInInspector]
    public int checkedLayer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkedLayer = collision.gameObject.layer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        checkedLayer = collision.gameObject.layer;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkedLayer = -1;
    }
}
