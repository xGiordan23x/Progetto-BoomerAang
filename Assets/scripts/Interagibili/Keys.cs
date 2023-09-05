using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    Chiave,
    Chip
}

public class Keys : Interactable
{
    public KeyType tipologiaChiave = KeyType.Chiave;

    [Header("Audio")]
    public AudioClip ClipRaccogliChiaveChip;

    public override void Interact(Player player)
    {
        Inventory inventory = player.GetComponent<Inventory>();

        if (inventory != null)
        {
            inventory.AddObject(this);
        }

    }

    public void PlayAudioClipRaccogliChiaveChip()
    {
        AudioManager.instance.PlayAudioClip(ClipRaccogliChiaveChip);
    }
}
