using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float mSpeed = 40.0f;
    private Rigidbody mEnemyRB;
    private GameObject mPlayerGoal;

    // Start is called before the first frame update
    void Start()
    {
        mEnemyRB = GetComponent<Rigidbody>();
        mPlayerGoal = GameObject.Find("PlayerGoal");
        return;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = mPlayerGoal.transform.position - transform.position;
        mEnemyRB.AddForce(lookDirection.normalized * mSpeed * Time.deltaTime);
        return;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "EnemyGoal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "PlayerGoal")
        {
            Destroy(gameObject);
        }
        return;
    }
}
