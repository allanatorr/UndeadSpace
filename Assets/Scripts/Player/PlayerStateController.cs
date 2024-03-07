using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    PlayerState currentState;
    List<PlayerStateListener> listeners = new List<PlayerStateListener>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        foreach (PlayerStateListener listener in listeners)
        {
            listener.onPlayerStateChange(newState);
        }
    }

    public void AddListener(PlayerStateListener listener)
    {
        listeners.Add(listener);
    }
}
