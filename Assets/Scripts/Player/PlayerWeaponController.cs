using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour, PlayerStateListener, WeaponListener
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject weaponHolderObject;
    [SerializeField] private GameObject startWeapon;
    [SerializeField] private List<GameObject> startWeaponPrefabs;

    private List<GameObject> currentWeapons = new List<GameObject>();
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
        currentWeapon = currentWeapons[weaponIndex];
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
            InstantiateWeapon(weaponPrefab);
        }
    }

    private void InstantiateWeapon(GameObject weaponPrefab)
    {
        GameObject weapon = Instantiate(weaponPrefab, weaponHolderObject.transform);
        WeaponController weaponController = weapon.GetComponent<WeaponController>();
        weaponController.SetFiringPoint(firingPoint);
        weaponController.AddListener(this);
        weapon.SetActive(false);
        currentWeapons.Add(weapon);
    }

    public void EquipNextWeapon()
    {
        weaponIndex = (weaponIndex + 1) % currentWeapons.Count;
        EquipWeapon();
    }

    public void EquipPreviousWeapon()
    {
        if (weaponIndex == 0) weaponIndex = currentWeapons.Count - 1;
        else weaponIndex -= 1;
        EquipWeapon();
 
    }

    private void EquipWeapon()
    {
        currentWeapon.SetActive(false);
        currentWeapon = currentWeapons[weaponIndex];
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

    public void OnWeaponDeletion(GameObject weapon)
    {
        currentWeapons.Remove(weapon);
        if (currentWeapons.Count == 0)
        {
            InstantiateWeapon(startWeapon);
        }
        EquipNextWeapon();
    }
}
