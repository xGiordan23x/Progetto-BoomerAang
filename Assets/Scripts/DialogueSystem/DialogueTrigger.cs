using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogueTrigger : MonoBehaviour, ISubscriber
{
    public Dialogue dialogue;
    private DialogueManager manager;
    private bool canPlayDialogue = true;
   
    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
        PubSub.Instance.RegisteredSubscriber(nameof(DialogueTrigger), this);
        
       
    }


    public void TriggerDialogue()
    {
        if (canPlayDialogue)
        {
            manager.StartDialogue(dialogue);
            canPlayDialogue= false;
           
        }
               
    }

  public void SetCanPlayDialogue()
    {
        canPlayDialogue= true;
        Debug.Log("Puoi Interagire");
    }

    public void OnNotify(object content, bool vero = false)
    {
        if(content is DialogueManager)
        {
            Invoke(nameof(SetCanPlayDialogue), 1f);
        }
    }
}