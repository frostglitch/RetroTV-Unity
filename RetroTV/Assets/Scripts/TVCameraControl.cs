using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVCameraControl : MonoBehaviour
{
    public GameObject irlCamera;
    public GameObject rendCam;
    public GameObject gameCam;
    public Animator roomAnim;

    public void _Out()
    {
        roomAnim.gameObject.SetActive(true);
        roomAnim.SetTrigger("Zoom Out");
        

        rendCam.SetActive(true);
        irlCamera.SetActive(true);

        gameCam.SetActive(false);

    }

    public void _In()
    {
        StartCoroutine(In());
    }

    IEnumerator In()
    {
        roomAnim.SetTrigger("Zoom In");

        yield return new WaitForSeconds(1f);

        gameCam.SetActive(true);

        roomAnim.gameObject.SetActive(false);

        rendCam.SetActive(false);
        irlCamera.SetActive(false);
    }
}
