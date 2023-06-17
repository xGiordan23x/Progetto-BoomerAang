using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager manager;
   
    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
        
       
    }


    public void TriggerDialogue()
    {
     
         manager.StartDialogue(dialogue);          
    }

   
}