using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Variabili key")]
    [SerializeField] Image keyIcon;
    [SerializeField] int numberOfKey;
    [SerializeField] Sprite uncollectedKeySprite;
    [SerializeField] Sprite collectedKeySprite;


    [Header("Variabili Chip")]

    [SerializeField] Image chipIcon;
    [SerializeField] int numberOfChip;
    [SerializeField] Sprite uncollectedChipSprite;
    [SerializeField] Sprite collectedChipSprite;

    private void Start()
    {
        GameManager.instance.OnkeyPickUp += SetKeySprite;
        GameManager.instance.OnChipPickUp += SetChipSprite;
    }

    private void SetKeySprite(int quantity)
    {
        numberOfKey = quantity;

        if (numberOfKey >= 1)
        {
            keyIcon.sprite = collectedKeySprite;
        }
        else
        {
            keyIcon.sprite = uncollectedKeySprite;
        }
    }

    private void SetChipSprite(int quantity)
    {
        numberOfChip = quantity;

        if (numberOfChip >= 1)
        {
            chipIcon.sprite = collectedChipSprite;
        }
        else
        {
            chipIcon.sprite = uncollectedChipSprite;
        }

    }


}
