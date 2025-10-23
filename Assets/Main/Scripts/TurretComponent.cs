using System.Collections;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TurretComponent : Components
{
   //orient towards player at all times
   //spawn object offset from position
   //send object flying straight out; will hit player if main body is aimed properly

    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform barrelTip;
    private bool canShoot;
   [SerializeField] private float fireRatePerSec = 0.5f;

   void Start()
   {
      base.Start();
      canShoot = true;
   }

   void Update()
   {
      base.Update();

      StartCoroutine(Shoot());

      if (SeePlayer())
      {
         OrientToPlayer();
      }
      else
      {
         ScanArea();
      }
   }

   private void OrientToPlayer()
   {

      Quaternion orient = Quaternion.LookRotation(new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z) - transform.position);
      //other.position - this.position; triangulates a direction to look in
      transform.rotation = orient;
   }

   private void ScanArea()
   {
      //rotate turret 360
      this.transform.Rotate(Vector3.up, 1, Space.World);
   }

   private IEnumerator Shoot()
   {
      //when sees player Shoot()
      if (SeePlayer() && canShoot)
      {
         canShoot = false;
         //actual shoot script
         GameObject bullet = Instantiate(bulletObject, barrelTip.position, Quaternion.identity);
         //Destroy after 5 sec in case misses player
         Destroy(bullet, 5f);
         Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
         Vector3 playerDir = (playerController.transform.position - transform.position).normalized;
         bulletRB.AddForce(playerDir * 10f, ForceMode.Impulse);
         yield return new WaitForSeconds(fireRatePerSec);
         canShoot = true;
      }

   }

}
