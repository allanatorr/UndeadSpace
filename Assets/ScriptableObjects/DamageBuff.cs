using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/DamageBuff")]
public class DamageBuff : PowerupEffect
{
    public float amount;
    public float time;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerWeaponController>().ApplyDamageBuff(amount, time);
        GameManager.GetInstance().incrementHighscore(100);
    }
}
