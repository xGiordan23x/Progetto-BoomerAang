using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubSub : MonoBehaviour
{
    private static PubSub _instance;
    public static PubSub Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            GameObject pubsubObject = new GameObject("# PubSub");

            _instance = pubsubObject.AddComponent<PubSub>();

            return _instance;
        }
    }




    private Dictionary<string, List<Action<object>>> _registeredFunction = new();

    public void RegisteredFunction(string messageType, Action<object> function)
    {
        if (_registeredFunction.ContainsKey(messageType))
        {
            _registeredFunction[messageType].Add(function);
        }
        else
        {
            List<Action<object>> newList = new();
            newList.Add(function);

            _registeredFunction.Add(messageType, newList);
        }
    }


    public new void SendMessage(string messageType, object messageContent)
    {
        foreach (Action<object> function in _registeredFunction[messageType])
        {
            function.Invoke(messageContent);
        }
    }


}
