using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DIalogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touching");

        TriggerDialogue();
        Destroy(gameObject);
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{   Debug.Log("Exit");
    //    DestroyDialogueTrigger();
    //}


    //void DestroyDialogueTrigger()
    //{
        
    //    Hazel_Aadit.isHit = false;
    //    Destroy(gameObject);
    //}

}
