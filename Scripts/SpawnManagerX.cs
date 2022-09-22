using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject mPowerupPrefab;
    public GameObject mEnemyPrefab;
    public GameObject mFocalPoint;
    public GameObject mPlayer;

    private float mEnemySpeed = 40.0f;
    private float mSpawnRangeX = 10;
    private float mSpawnZMin = 15; // set min spawn Z
    private float mSpawnZMax = 25; // set max spawn Z
    private int mWaveCount = 1;
    private int mEnemyCount= 0;    

    // Update is called once per frame
    void Update()
    {
        mEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SpawnPowerups();
        if (mEnemyCount <= 0)
        {
            SpawnEnemyWave(mWaveCount);
        }
        return;
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-mSpawnRangeX, mSpawnRangeX);
        float zPos = Random.Range(mSpawnZMin, mSpawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(mEnemyPrefab, GenerateSpawnPosition(), mEnemyPrefab.transform.rotation);
        }

        EnemyX[] enemiesList = FindObjectsOfType<EnemyX>();
        for (int i = 0; i < enemiesList.Length; i++)
        {
            enemiesList[i].mSpeed = mEnemySpeed;
        }
        mEnemySpeed += 20.0f;
        mWaveCount++;
        ResetPlayerPosition(); // put player back at start
        return;
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        mFocalPoint.transform.rotation = Quaternion.identity;
        mPlayer.transform.position = new Vector3(0, 1, -7);
        mPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        mPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        return;
    }

    void SpawnPowerups()
    {
        // If no powerups remain, spawn a powerup
        Vector3 powerupSpawnOffset = new Vector3(0, 0, 20); // make powerups spawn at player end
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(mPowerupPrefab, GenerateSpawnPosition() - powerupSpawnOffset, mPowerupPrefab.transform.rotation);
        }
        return;
    }
}
