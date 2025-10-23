using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class MortarComponent : Components
{
   //maybe change to projectile based if have time
   //non projectile based: use same transform, spawn object directly above transform and fall down into it
   //get position of player
   //spawn area indication
   // spawn projectile far above it 
   //object will fall down and damage player if hit
   //maybe stun

   private bool bCanSpawn;
   [SerializeField] private float mortarCooldown;
   [SerializeField] private float bulletSpawnTime;
   [SerializeField] private int bulletSpeed;
   [SerializeField] private GameObject hitAreaPrefab;
   [SerializeField] private GameObject mortarProjectile;
   GameObject hitArea;

    void Start()
    {
      base.Start();
      bCanSpawn = true;
    }

    void Update()
   {
      base.Update();
      if (SeePlayer() && bCanSpawn)
      {
         StartCoroutine(SpawnHitArea());
      }
   }

   IEnumerator SpawnHitArea()
   {
      bCanSpawn = false;
      GameObject newHitArea = Instantiate(hitAreaPrefab, player.transform.position, Quaternion.identity);
      hitArea = newHitArea;
      yield return new WaitForSeconds(bulletSpawnTime);
      SpawnProjectile();
      yield return new WaitForSeconds(mortarCooldown);
      bCanSpawn = true;
   }
   
   private void SpawnProjectile()
    {
      GameObject bullet = Instantiate(mortarProjectile, new Vector3(hitArea.transform.position.x, hitArea.transform.position.y + 10, hitArea.transform.position.z), Quaternion.identity);
      Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
      bulletRB.AddForce(-transform.up * bulletSpeed, ForceMode.Impulse);
    }
}
