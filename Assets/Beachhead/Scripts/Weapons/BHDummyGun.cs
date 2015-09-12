using UnityEngine;
using System.Collections;

using Beachhead.Enemies;


namespace Beachhead.Weapons {

	public class BHDummyGun : BHWeapon {

		public GameObject Barrel;
		
		public GameObject MuzzleFlashPrefab;
		public GameObject BulletTracerPrefab;
		public GameObject BulletShellPrefab;
		public GameObject BulletHitEffectPrefab;
		
		public Transform MuzzleProxy;
		public Transform BulletShellProxy;

		public float BulletDamage;
		public float FireCoolDownTime;
		public float ReloadTime;
		public int Ammo;
		public int MagazineSize;

		GameObject mTarget;
		Vector3 mTargetHitPoint;
		Vector3 mTargetHitNormal;
		float mCooldownTimer;
		float mReloadTimer;


		// Update is called once per frame
		protected override void Update () {
			Ray ray = new Ray(MuzzleProxy.position, MuzzleProxy.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, FireRange)) {
				mTarget = hit.collider.gameObject;
				mTargetHitPoint = hit.point;
				mTargetHitNormal = hit.normal;
			}
			else {
				mTarget = null;
			}

			if (mCooldownTimer <= 0) {
				if (Input.GetKey(KeyCode.Space)) {
					Fire();
				}
			}
			else {
				mCooldownTimer -= Time.deltaTime;
			}
		}

		public override void Fire () {
			if (Ammo <= 0) {
				Debug.Log("DummyGun - Out of Ammo");
				return;
			}

			GameObject muzzleFlashGO = Instantiate(MuzzleFlashPrefab, MuzzleProxy.position, MuzzleProxy.rotation) as GameObject;
			Destroy(muzzleFlashGO, 1f);

			GameObject bulletTracerGO = Instantiate(BulletTracerPrefab, MuzzleProxy.position, MuzzleProxy.rotation) as GameObject;
			Destroy(bulletTracerGO, 1f);

			GameObject bulletShellGO = Instantiate(BulletShellPrefab, BulletShellProxy.position, Quaternion.identity) as GameObject;
			Rigidbody rgbd = bulletShellGO.GetComponent<Rigidbody>();
			rgbd.AddForce(new Vector3( 5 * Random.value * Random.Range(-1, 1), 
			                          5 + (1 * Random.value), 
			                          -(10 + (5 * Random.value))),
			              ForceMode.Impulse);
			Destroy(bulletShellGO, 1f);

			if (mTarget != null) {
				GameObject bulletHitEffectGO = Instantiate(BulletHitEffectPrefab, mTargetHitPoint, Quaternion.identity) as GameObject;
				bulletHitEffectGO.transform.up = mTargetHitNormal;
				Vector3 pos = mTargetHitPoint + (0.1f * mTargetHitNormal);
				Destroy(bulletHitEffectGO, 1f);

				if (mTarget.tag == BHConstants.TAG_ENEMY) {
					BHEnemy enemy = mTarget.GetComponent<BHEnemy>();
					enemy.TakeDamage(BulletDamage);
				}
			}

			mCooldownTimer = FireCoolDownTime;

			Ammo -= 1;
		}
		
		void OnDrawGizmos() {
			Vector3 p1 = MuzzleProxy.transform.position;
			Vector3 p2 = p1;
			if (mTarget != null) {
				Gizmos.color = new Color(1, 0, 0, 0.25f);
				p2 = mTargetHitPoint;
			}
			else {
				Gizmos.color = new Color(0, 1, 0, 0.25f);
				p2 = p1 + (FireRange * MuzzleProxy.transform.forward);
			}
			Gizmos.DrawLine(p1, p2);
		}

	}

}