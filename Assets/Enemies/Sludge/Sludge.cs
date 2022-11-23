using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTOTIR.Combat;

public class Sludge : Character
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int attackDelay;
    
    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Time.time >= nextAttackTime) {
            SpawnProjectile(projectilePrefab, projectileSpawnPosition, projectileSpeed);
            nextAttackTime = Time.time + attackDelay;
        }
    }
}
