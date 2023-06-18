using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager manager;
    private bool canPlayDialogue = true;
   
    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
        
       
    }


    public void TriggerDialogue()
    {
        if (canPlayDialogue)
        {
            manager.StartDialogue(dialogue);
            canPlayDialogue= false;
            Invoke(nameof(SetCanPlayDialogue),1f);
        }
               
    }

  public void SetCanPlayDialogue()
    {
        canPlayDialogue= true;
        Debug.Log("Puoi Interagire");
    }
}