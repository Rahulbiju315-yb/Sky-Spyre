using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNew : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DIalogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touching");
        StartCoroutine(DisplayDialogFor(5));
        //Destroy(gameObject);
    }

    private IEnumerator DisplayDialogFor(float t)
    {
        DIalogueManager dman = FindObjectOfType<DIalogueManager>();
        dman.StartDialogue(dialogue);
        foreach(string str in dialogue.sentences)
        {
            yield return new WaitForSeconds(t);
            dman.StartCoroutine(dman.TypeSentence(str));
            
            Debug.Log(str);
        }
        yield return new WaitForSeconds(t);
        dman.EndDialogue();
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
