using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveModifier : MonoBehaviour, ISubscriber
{
    public Vector2 newDirection;
    public bool activated;

    public void OnNotify(object content)
    {
        if(content is Player && !activated)
        {
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
            
        }
        else if (content is PlayerStateBoomerangMovement)
        {
            activated= false;
        }
    }

    private void Start()
    {
      PubSub.Instance.RegisteredSubscriber(nameof(CurveModifier),this);
        activated= false;
    }
}
