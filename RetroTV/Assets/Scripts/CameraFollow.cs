using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Follow
    Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public Transform player;

    public Vector2 playerOffset;


    //Bounds
    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;


    void FixedUpdate()
    {
        //Follow
        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x + playerOffset.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y + playerOffset.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
        


        //Bounds
        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
}
