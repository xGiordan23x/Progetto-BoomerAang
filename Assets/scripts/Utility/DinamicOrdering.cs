using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DinamicOrdering : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
            ChangeLayer();
        
       
    }

  
    private void ChangeLayer()
    {
        renderer.sortingOrder = (int)(-transform.position.y * 200);
    }

    private void Awake()
    {
        renderer= GetComponent<SpriteRenderer>();
       
            ChangeLayer();

        if (gameObject.isStatic)
        {
            Destroy(this);
        }
    }


}
