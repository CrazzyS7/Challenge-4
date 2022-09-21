using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Vector3 mPowerupIndicatorOffset = new Vector3(0, 0.5f, 0);
    private float mPowerupStrength = 25;            // how hard to hit enemy with powerup
    private float mNormalStrength = 10;             // how hard to hit enemy without powerup
    private bool mHasPowerup = false;
    private int mPowerupDuration = 5;
    private GameObject mFocalPoint;
    private float mSpeed = 500.0f;
    private Rigidbody mPlayerRB;

    public GameObject mPowerupIndicator;

    void Start()
    {
        mPlayerRB = GetComponent<Rigidbody>();
        mFocalPoint = GameObject.Find("FocalPoint");
        return;
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        mPlayerRB.AddForce(mFocalPoint.transform.forward * verticalInput * mSpeed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        mPowerupIndicator.transform.position = transform.position - mPowerupIndicatorOffset;
        return;
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            mHasPowerup = true;
            mPowerupIndicator.SetActive(mHasPowerup);
            StartCoroutine(PowerupCooldown());
        }
        return;
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(mPowerupDuration);
        mHasPowerup = false;
        mPowerupIndicator.SetActive(mHasPowerup);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (mHasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * mPowerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * mNormalStrength, ForceMode.Impulse);
            }
        }
        return;
    }
}
