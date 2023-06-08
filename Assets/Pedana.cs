using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pedana : MonoBehaviour
{
    [SerializeField] UnityEvent pressIn;
    [SerializeField] UnityEvent pressOut;

    [SerializeField] List<GameObject> oggettiAPortata;
    private bool pressed = false;

    [SerializeField] Sprite unpressedSprite;
    [SerializeField] Sprite pressedSprite;

    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    AudioClip ClipAttivaPedana;
    AudioClip ClipDisattivaPedana;


    private void Start()
    {
        oggettiAPortata = new List<GameObject>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Scatola>() != null)
        {
            oggettiAPortata.Add(collision.gameObject);
            ObjectCount();
        }

        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement)
            {
                oggettiAPortata.Add(collision.gameObject);
                ObjectCount();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Scatola>() != null)
        {
            oggettiAPortata.Remove(collision.gameObject);
            ObjectCount();
        }

        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement || collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
            {
                oggettiAPortata.Remove(collision.gameObject);
                ObjectCount();
            }
        }
    }

    private void ObjectCount()
    {
        if (oggettiAPortata.Count >= 1 && !pressed)
        {
            pressed = true;
            pressIn?.Invoke();

            spriteRenderer.sprite = pressedSprite;
        }

        if (oggettiAPortata.Count == 0)
        {
            pressed = false;
            pressOut?.Invoke();

            spriteRenderer.sprite = unpressedSprite;
        }
    }

    public void TestFuncion()
    {
        if (pressed)
        {
            Debug.Log("sono premuto");
        }
        else
        {
            Debug.Log("non sono piu premuto");
        }
    }

    public void PlayAudioClipAttivaPedana()
    {
        AudioManager.instance.PlayAduioClip(ClipAttivaPedana);
    }
    public void PlayAudioClipDisattivaPedanap()
    {
        AudioManager.instance.PlayAduioClip(ClipDisattivaPedana);
    }
}
