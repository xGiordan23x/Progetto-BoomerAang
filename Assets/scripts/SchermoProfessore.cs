using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchermoProfessore : MonoBehaviour, ISubscriber
{
    Player player;
    Animator animator;

    public void OnNotify(object content)
    {
        throw new System.NotImplementedException();
    }
}
