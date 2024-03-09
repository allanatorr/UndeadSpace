using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform firingPoint;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private int projectilesPerShot;
    [SerializeField] private int shotSpeed;
    [SerializeField] private float range;
    [SerializeField] private float horizontalSpread;
    [SerializeField] private float verticalSpread;

    [SerializeField] public float shootingCooldown = 0.5f; // Cooldown-Zeit in Sekunden zwischen den Schüssen
    private float shootingTimer; // Timer, der verfolgt, wann zuletzt geschossen wurde

    private void FixedUpdate() 
    {
        shootingTimer += Time.deltaTime;
    }

    public void SetFiringPoint(Transform firingPoint)
    {
        this.firingPoint = firingPoint;
    }

    public void Fire()
    {
        if (shootingTimer < shootingCooldown) return;
        shootingTimer = 0f;

        for (int i = 0; i < projectilesPerShot; i++)
        {
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.SetDirection(calcProjectileSpread());
        projectileController.SetSpeed(shotSpeed);
    }

    private Vector3 calcProjectileSpread()
    {
        float horizontalOffset = Random.Range(-horizontalSpread, horizontalSpread);
        float verticalOffset = Random.Range(-verticalSpread, verticalSpread);
        Vector3 direction = Quaternion.Euler(verticalOffset, horizontalOffset, verticalOffset) * firingPoint.forward;
        return direction;
    }
}
