using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerStateListener
{
    public void onPlayerStateChange(PlayerState newState) 
    {
        return;
    }

    public void OnWeaponStatusChange(bool isEnabled)
    {
        return;
    }
}

