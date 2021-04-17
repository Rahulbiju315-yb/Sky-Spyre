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
    public bool hasActivated;

    // Start is called before the first frame update
    void Start()
    {
        hasActivated = true;
        player = GameObject.Find("Player");
        projectiles = new GameObject[numberOfProjectiles];
        projectileStartTime = new float[numberOfProjectiles];
 
        nextIndex = 0;
        StartCoroutine(DelayAndFire());
    }

    // Update is called once per frame
    void Update()
    {
        DisableDeadProjectiles();
    }

    Vector2 GetPlayerDirVec()
    {
        return (player.transform.position - gameObject.transform.position).normalized;
    }
    IEnumerator DelayAndFire()
    {
        while(true)
        {
            
            nextIndex = 0;
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                if (hasActivated)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Attack");
                    yield return new WaitForSeconds(0.5f);
                    Debug.Log("Enter");
                    projectiles[nextIndex] = Instantiate(projectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
                    projectiles[nextIndex].GetComponent<Rigidbody2D>().velocity = GetPlayerDirVec() * projectileSpeed;
                    projectileStartTime[nextIndex] = Time.time;
                    nextIndex++;
                    gameObject.GetComponent<Animator>().SetTrigger("Idle");
                }
                yield return new WaitForSeconds(projectileDelay);
            }
            yield return new WaitForSeconds(attackDelay);
      
        }

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

    public void Stop()
    {
        StopAllCoroutines();
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (projectiles[i] != null)
                projectiles[i].SetActive(false);
        }
    }
}
