using UnityEngine;
using System.Collections;

namespace Beachhead.Enemies {

	public class BHEnemy : MonoBehaviour {
		
		public string EnemyName;

		public float Hp;
		public float HpDamage;

		public float Shield;
		public float ShieldDamage;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public virtual void TakeDamage(float damage) {
			float remainShield = GetRemainShield();
			if (remainShield > 0) {
				ShieldDamage += damage;
				Debug.Log("BHEnemy - Shield damage: " + damage);
			}
			else {
				HpDamage += damage;
				Debug.Log("BHEnemy - Receive damage: " + damage);
			}

			float remainHp = GetRemainHp();
			if (remainHp <= 0) {
				Destroy(gameObject);
			}
		}

		public float GetRemainShield() {
			return Shield - ShieldDamage;
		}

		public float GetRemainHp() {
			return Hp - HpDamage;
		}
	}

}