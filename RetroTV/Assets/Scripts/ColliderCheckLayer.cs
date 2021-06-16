using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckLayer : MonoBehaviour
{
    public int layerToCheck;

    public bool check;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToCheck) check = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToCheck) check = false;
    }
}
