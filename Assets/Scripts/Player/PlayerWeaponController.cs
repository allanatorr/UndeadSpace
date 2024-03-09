using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour, PlayerStateListener
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject weaponHolderObject;
    [SerializeField] private List<GameObject> startWeaponPrefabs;

    private List<GameObject> allWeapons = new List<GameObject>();
    private GameObject currentWeapon;
    private int weaponIndex;
    private bool weaponIsEnabled;

    private void Awake() 
    {
        GetComponent<PlayerStateController>().AddListener(this);
    }

    void Start()
    {
        weaponIndex = 0;
        InstantiateWeapons();
        currentWeapon = allWeapons[weaponIndex];
        currentWeapon.SetActive(true);
    }

    void FixedUpdate() 
    {

    }

    private void Update() 
    {

    }

    private void InstantiateWeapons()
    {
        foreach (GameObject weaponPrefab in startWeaponPrefabs)
        {
            GameObject weapon = Instantiate(weaponPrefab, weaponHolderObject.transform);
            WeaponController weaponController = weapon.GetComponent<WeaponController>();
            weaponController.SetFiringPoint(firingPoint);
            weapon.SetActive(false);
            allWeapons.Add(weapon);
        }
    }

    public void EquipNextWeapon()
    {
        weaponIndex = (weaponIndex + 1) % allWeapons.Count;
        EquipWeapon();
    }

    public void EquipPreviousWeapon()
    {
        if (weaponIndex == 0) weaponIndex = allWeapons.Count - 1;
        else weaponIndex -= 1;
        EquipWeapon();
 
    }

    private void EquipWeapon()
    {
        currentWeapon.SetActive(false);
        currentWeapon = allWeapons[weaponIndex];
        currentWeapon.SetActive(true);
    }
  
    public void FireCurrentWeapon()
    {
        if (!weaponIsEnabled) return;
        currentWeapon.GetComponent<WeaponController>().Fire();
    }

    public void OnWeaponStatusChange(bool isEnabled)
    {
        weaponIsEnabled = isEnabled;
        weaponHolderObject.SetActive(isEnabled);
    }
}
