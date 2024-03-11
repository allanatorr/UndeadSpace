using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
    public float amount;
    public float time;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerPowerUp>().ApplySpeedBuff(amount, time);
        GameManager.GetInstance().incrementHighscore(200);
    }
}
