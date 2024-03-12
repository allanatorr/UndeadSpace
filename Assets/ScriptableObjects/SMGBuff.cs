using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SMGBuff")]
public class SMGBuff : PowerupEffect
{
    public WeaponType weaponType;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerWeaponController>().SetWeaponOwned(weaponType);
        GameManager.GetInstance().IncrementHighscore(300);
    }
}
