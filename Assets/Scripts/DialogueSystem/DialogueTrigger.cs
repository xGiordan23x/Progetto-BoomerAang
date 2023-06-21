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
        else
        {
            Debug.Log("aspetta per interagire");
            return;
        }
               
    }

  public void SetCanPlayDialogue()
    {
        canPlayDialogue= true;
       
    }

    public void OnNotify(object content, bool vero = false)
    {
        if(content is DialogueManager)
        {
            Invoke("SetCanPlayDialogue", 2f);
        }
    }
}