using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class WeaponUIManager : MonoBehaviour
{
    public GameObject panelPrefab;
    public Transform container; 
    private Dictionary<WeaponType, GameObject> instantiatedPanels = new Dictionary<WeaponType, GameObject>();

    public void initializePanels(List<WeaponItem> list) {
        
        foreach (WeaponItem weapon in list)
        {
            CreatePanel(weapon);
        }
    }

    public void CreatePanel(WeaponItem item)
    {
        GameObject panelInstance = Instantiate(panelPrefab);
        instantiatedPanels.Add(item.type, panelInstance);

        panelInstance.transform.SetParent(container.transform, false);


        Sprite banana = Resources.Load<Sprite>(item.weaponImage);
        Image childImageComponent = panelInstance.transform.Find("Weapon").GetComponent<Image>();
        childImageComponent.sprite = banana;
    }

    public void ApplyWeaponInUse(WeaponType itemName) {

        foreach (GameObject value in instantiatedPanels.Values)
        {
            value.GetComponent<Outline>().enabled = false;
        }

        instantiatedPanels[itemName].GetComponent<Outline>().enabled = true;
    }

    public void ApplyWeaponOwned(WeaponType itemName)
    {
        GameObject panel;

        if (instantiatedPanels.TryGetValue(itemName, out panel))
        {
            panel.transform.Find("Weapon").GetComponent<Image>().color = Color.white;
        }
    }
}
