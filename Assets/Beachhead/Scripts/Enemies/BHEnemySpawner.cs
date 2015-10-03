using UnityEngine;
using System.Collections;

namespace Beachhead.Enemies {

	public class BHEnemySpawner : MonoBehaviour {

		public Transform SpawnPoint;

		public float Delay;
		public float Interval;
		public int Limit;

		float mTimer;
		int mCounter;

		void Start() {
			mTimer = Delay;
			mCounter = 0;
		}

		void Update() {
			if (mCounter >= Limit) {
				Destroy(gameObject);
				return;
			}

			mTimer -= Time.deltaTime;
			if (mTimer <= 0) {
				GameObject enemyGO = CreateEnemy();
				mCounter += 1;
				mTimer = Interval;
			}
		}

		protected virtual GameObject CreateEnemy() {
			return null;
		}
	}
}