using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSource : MonoBehaviour
{
    public GameObject projectilePrefab;

    private GameObject[] projectiles;
    private float[] projectileStartTime;

    private GameObject player;
    private int nextIndex;
    private float lastTime;

    public int numberOfProjectiles;
    public float projectileSpeed;
    public float projectileDelay;
    public float projectileLifetime;

    public float attackDelay;
    private bool isfiring;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        projectiles = new GameObject[numberOfProjectiles];
        projectileStartTime = new float[numberOfProjectiles];
 
        nextIndex = 0;
        isfiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastTime > attackDelay || nextIndex != numberOfProjectiles) && !isfiring)
        {
            if (nextIndex == numberOfProjectiles)
                nextIndex = 0;

            StartCoroutine(DelayAndFire());
            lastTime = Time.time;
        }

        DisableDeadProjectiles();
    }

    Vector2 GetPlayerDirVec()
    {
        return (player.transform.position - gameObject.transform.position).normalized;
    }
    IEnumerator DelayAndFire()
    {
        isfiring = true;
        projectiles[nextIndex] = Instantiate(projectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
        projectiles[nextIndex].GetComponent<Rigidbody2D>().velocity = GetPlayerDirVec() * projectileSpeed;
        projectileStartTime[nextIndex] = Time.time;
        nextIndex++;
        yield return new WaitForSeconds(projectileDelay);
        isfiring = false;

    }

    void DisableDeadProjectiles()
    {
        for(int i = 0; i < numberOfProjectiles; i++)
        {
            if(Time.time - projectileStartTime[i] >= projectileLifetime)
            {
                if(projectiles[i] != null)
                    projectiles[i].SetActive(false);
            }
        }
    }
}
