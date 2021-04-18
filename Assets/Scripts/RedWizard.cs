using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWizard : MonoBehaviour
{
    CaptureLightScript lcapL;
    CaptureLightScript lcapR;
    CaptureLightScript lcapT;
    CaptureLightScript lcapB;

    bool notDead;
    // Start is called before the first frame update
    void Start()
    {
        lcapL = GameObject.Find("BossLeft").GetComponent<CaptureLightScript>();
        lcapR = GameObject.Find("BossRight").GetComponent<CaptureLightScript>();
        lcapT = GameObject.Find("BossTop").GetComponent<CaptureLightScript>();
        lcapB = GameObject.Find("BossBottom").GetComponent<CaptureLightScript>();
        notDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(lcapL.IsLit() && lcapB.IsLit() && lcapT.IsLit() && lcapR.IsLit() && notDead)
        {
            StartCoroutine(OnDeath());
            GameObject.Find("GODTop").SetActive(false);
            Destroy(GameObject.Find("BGM1"));
        }
    }

    IEnumerator OnDeath()
    {
        notDead = false;
        gameObject.GetComponent<ProjectileSource>().Stop();
        gameObject.GetComponent<Animator>().speed *= 0.25f;
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(4f);
        Destroy(GameObject.Find("Boss"));
    }
}
