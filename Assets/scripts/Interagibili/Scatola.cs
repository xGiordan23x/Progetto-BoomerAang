using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scatola : Interactable
{
    [Header("OpzioniCassa")]

    [SerializeField] bool isDestroyable;
    [SerializeField] bool canMove;

    public LayerMask mask;

    Vector3 startPosition;
    Vector3 endPosition;

    private bool isMoving = false;

    private float timeToMove = 0.5f;

    private Animator animator;

    [SerializeField] UnityEvent OnBoomerangHit;

    [Header("Audio")]

    [SerializeField] AudioClip ClipRotturaCassa;
    [SerializeField] AudioClip ClipSpostamentoCassa;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }




    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement && canMove)
        {
            if (!isMoving)
            {
                StartCoroutine(MoveBox(player.lastDirection));

                //player animation
                player.SetCanMove(0);
                player.animator.SetTrigger("HumanBox");

                base.Interact(player);          //per effetti visivi/sonori
            }
        }

        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement && canMove)       //solo per animazione
        {
            //player animation
            player.SetCanMove(0);
            player.animator.SetTrigger("BoomerangBox");
        }


        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning && isDestroyable)
        {
            //gameObject.SetActive(false);
            PlayAnimation();
            OnBoomerangHit?.Invoke();
        }




    }

    private void Update()
    {

    }

    private IEnumerator MoveBox(Vector3 direction)
    {
        List<RaycastHit2D> hit2Ds = new List<RaycastHit2D>();

        isMoving = true;

        float elapsedTime = 0;

        startPosition = transform.position;
        endPosition = startPosition + direction;

        if (Physics2D.Raycast(startPosition + (direction * 0.6f), direction, 0.5f, mask))   //brutto da rivedere
        {
            isMoving = false;

            Debug.Log("non si va");
            yield break;
        }

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        isMoving = false;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("Hit");
    }

    public void DisableBox()
    {
        gameObject.SetActive(false);
    }


    public void PlayAudioClipSpostaCassa()
    {
        AudioManager.instance.PlayAudioClip(ClipSpostamentoCassa);
    }
    public void PlayAudioClipRotturaCassa()
    {
        AudioManager.instance.PlayAudioClip(ClipRotturaCassa);
    }

}
