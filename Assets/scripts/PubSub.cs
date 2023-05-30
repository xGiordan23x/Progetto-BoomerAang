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


    //aggiungo quello che so io
    private Dictionary<string, List<ISubscriber>> _subscribers = new();
    public void RegisteredSubscriber(string messageType, ISubscriber subscriber)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Add(subscriber);

        }
        else
        {
            List<ISubscriber> newList = new();
            newList.Add(subscriber);
            _subscribers.Add(messageType, newList);
        }

    }
    public void SendMessageSubscriber(string messageType, object content, bool vero = false)
    {
        if (!_subscribers.ContainsKey(messageType)) return;


        foreach (ISubscriber subscriber in _subscribers[messageType])
        {
            subscriber.OnNotify(content);
        }
    }




}
