using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float mSpeed = 200;

    public GameObject mPlayer;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * mSpeed * Time.deltaTime);
        transform.position = mPlayer.transform.position; // Move focal point with player
        return;
    }
}
