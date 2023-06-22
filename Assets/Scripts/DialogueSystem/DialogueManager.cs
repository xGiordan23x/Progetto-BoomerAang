using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, ISubscriber
{
    private bool isInDialogue;
    public bool canInteract;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image dialogeBox;
    public float textSpeed;

    private Queue<string> sentences;

    void Start()
    {
        canInteract = false;
        sentences = new Queue<string>();
        ActivateDialogeBox(false);
        PubSub.Instance.RegisteredSubscriber(nameof(DialogueManager), this);
    }
    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetButtonDown("Use"))
            {
                ShowNextSentence();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isInDialogue = true;
        ActivateDialogeBox(true);
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this, true);
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this, true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        ShowNextSentence();

    }

    private void ActivateDialogeBox(bool value)
    {
        dialogeBox.gameObject.SetActive(value);
    }

    public void ShowNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string frase)
    {
        dialogueText.text = "";
        foreach (char letter in frase.ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(1 / textSpeed);

        }
        canInteract = true;
    }

    void EndDialogue()
    {
        isInDialogue = false;
        canInteract = false;
        ActivateDialogeBox(false);
        PubSub.Instance.SendMessageSubscriber(nameof(DialogueTrigger), this, false);
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this, false);
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this, false);


    }

    public void OnNotify(object content, bool vero = false)
    {
        if (content is PotionGenerator)
        {
            EndDialogue();
        }
        if (content is PlayerStateBoomerangMovement)
        {
            if (isInDialogue)
            {

                PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangMovement), this, true);
            }
            else
            {
                PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangMovement), this, false);
            }
        }
    }
}