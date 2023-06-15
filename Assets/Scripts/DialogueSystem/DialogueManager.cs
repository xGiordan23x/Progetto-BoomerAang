using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image dialogeBox;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        ActivateDialogeBox(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ActivateDialogeBox(true);

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
            yield return null;
        }
    }

    void EndDialogue()
    {
        ActivateDialogeBox(false);
    }
}