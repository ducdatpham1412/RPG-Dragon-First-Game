using System;
using System.Collections.Generic;
using UnityEngine;
public class MessagingManager : MonoBehaviour {
    public static MessagingManager Instance { get; private set; }
    private List<Action> subscribers = new List<Action>();

    void Awake() {
        Debug.Log("Messaging Manager Started");
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        //Make sure that the instance is not destroyed between scenes
        //(this is optional)
        DontDestroyOnLoad(gameObject);
    }

    public void Subscribe(Action subscriber) {
        Debug.Log("Subscriber registered");
        subscribers.Add(subscriber);
    }

    public void UnSubscribe(Action subscriber) {
        Debug.Log("Subscriber registered");
        subscribers.Remove(subscriber);
    }

    public void ClearAllSubscribers() {
        subscribers.Clear();
    }

    public void Broadcast() {
        Debug.Log("Broadcast requested, No of Subscribers = " +
      subscribers.Count);
        foreach (var subscriber in subscribers) {
            subscriber();
        }
    }
}
