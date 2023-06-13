using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int maxChiavi = 1;
    [SerializeField] int quantitaChiavi=0;
    [SerializeField] int maxChip = 1;
    [SerializeField] int quantitaChip=0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddObject(Interactable interactable)
    {
        Keys chiave = interactable.GetComponent<Keys>();

        if (chiave != null)
        {
            if (chiave.tipologiaChiave == KeyType.Chiave)
            {
                if (quantitaChiavi < maxChiavi)
                {
                    quantitaChiavi++;
                    GameManager.instance.ChiaviCounter = quantitaChiavi;
                    Destroy(interactable.gameObject);
                }
                else
                {
                    Debug.Log("hai troppe chiavi");
                }
            }

            if (chiave.tipologiaChiave == KeyType.Chip)
            {
                if (quantitaChip < maxChip)
                {
                    quantitaChip++;
                    GameManager.instance.ChipCounter = quantitaChip;
                    Destroy(interactable.gameObject);
                }
                else
                {
                    Debug.Log("hai troppi chip");
                }
            }
        }
    }

    public bool UseKey()
    {
        if (quantitaChiavi != 0)
        {
            quantitaChiavi--;
            Debug.Log("Ho usato una chiave");
            GameManager.instance.ChiaviCounter = quantitaChiavi;
            return true;
        }
        else
        {
            Debug.Log("Non ho chiavi");
            return false;
        }
    }

    public bool UseChip()
    {
        if (quantitaChip != 0)
        {
            quantitaChip--;
            Debug.Log("Ho usato un chip");
            GameManager.instance.ChipCounter=quantitaChip;
            return true;
        }
        else
        {
            Debug.Log("Non ho chip");
            return false;
        }
    }
}
