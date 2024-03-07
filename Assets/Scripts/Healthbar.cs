using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Image healthBar;

    public void Start() {
        healthBar.color = Color.green;
    }

    public void UpdateHealth(float fraction) {

        healthBar.fillAmount = fraction;

        if(fraction <= 0.4) {
            healthBar.color = Color.red;
        }
        else if(fraction <= 0.7) {
            healthBar.color = Color.yellow;
        }
        else {
            healthBar.color = Color.green;
        }
    }
}
