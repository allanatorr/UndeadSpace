using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponItem
{
    public WeaponType type;
    public String weaponImage;
    public KeyCode weaponKey;
    public bool isOwned; // Besitzstatus der Waffe
    public bool isUsed; // Wird gerade benutzt
    public GameObject weaponPrefab; // UI-Element für diese Waffe
}

public class PlayerWeaponController : MonoBehaviour, PlayerStateListener
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject weaponHolderObject;
    [SerializeField] private List<GameObject> startWeaponPrefabs;

    // private List<GameObject> allWeapons = new List<GameObject>();

    [SerializeField] private List<WeaponItem> weaponsCollection = new List<WeaponItem>();

    [SerializeField] private WeaponUIManager weaponUIManager;


    private WeaponItem currentWeapon;
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
        currentWeapon = weaponsCollection[weaponIndex];
        currentWeapon.weaponPrefab.SetActive(true);
        currentWeapon.isOwned = true;
        currentWeapon.isUsed = true;
        weaponUIManager.initializePanels(weaponsCollection);
        weaponUIManager.ApplyWeaponInUse(currentWeapon.type);
        weaponUIManager.ApplyWeaponOwned(currentWeapon.type);
    }

    void FixedUpdate() 
    {

    }

    private void Update() 
    {

    }

    private void InstantiateWeapons()
    {
        foreach (WeaponItem weaponPrefab in weaponsCollection)
        {
            GameObject weapon = Instantiate(weaponPrefab.weaponPrefab, weaponHolderObject.transform);
            WeaponController weaponController = weapon.GetComponent<WeaponController>();
            weaponController.SetFiringPoint(firingPoint);
            weapon.SetActive(false);
            weaponPrefab.weaponPrefab = weapon;
        }
    }

    public void EquipNextWeapon()
    {
        weaponIndex = (weaponIndex + 1) % weaponsCollection.Count;
        if(!weaponsCollection[weaponIndex].isOwned) EquipNextWeapon();
        EquipWeapon();
    }

    public void EquipPreviousWeapon()
    {
        if (weaponIndex == 0) weaponIndex = weaponsCollection.Count - 1;
        else weaponIndex -= 1;
        if(!weaponsCollection[weaponIndex].isOwned) EquipPreviousWeapon();
        EquipWeapon();
        
    }

    private void EquipWeapon()
    {
        currentWeapon.weaponPrefab.SetActive(false);
        currentWeapon = weaponsCollection[weaponIndex];
        currentWeapon.weaponPrefab.SetActive(true);

        foreach (WeaponItem weaponPrefab in weaponsCollection)
        {
            weaponPrefab.isUsed = false;
        }

        currentWeapon.isUsed = true;

        weaponUIManager.ApplyWeaponInUse(currentWeapon.type);
    }
  
    public void FireCurrentWeapon()
    {
        if (!weaponIsEnabled) return;
        currentWeapon.weaponPrefab.GetComponent<WeaponController>().Fire();
    }

    public void OnWeaponStatusChange(bool isEnabled)
    {
        weaponIsEnabled = isEnabled;
        weaponHolderObject.SetActive(isEnabled);
    }

    public void ApplyDamageBuff(float amount, float time) {

        // foreach (var weapon in startWeaponPrefabs)
        // {
        //     weapon.GetComponent<WeaponController>().IncreaseDamage(amount, time);
        // }

        currentWeapon.weaponPrefab.GetComponent<WeaponController>().IncreaseDamage(amount, time);
    }

    public void SetWeaponOwned(WeaponType type) {

        foreach (WeaponItem weaponPrefab in weaponsCollection)
        {
            if(weaponPrefab.type == type) {
                weaponPrefab.isOwned = true;
                weaponUIManager.ApplyWeaponOwned(type);
            }
        }
    }
}
