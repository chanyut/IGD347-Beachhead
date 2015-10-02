using UnityEngine;
using System.Collections;

namespace Beachhead.Enemies {

	public class BHEnemySpawner : MonoBehaviour {

		public GameObject EnemyPrefab;
		public Transform SpawnPoint;

		public float DelayRangeStart;
		public float DelayRangeEnd;
		public int Limit;

		float mTimer;
		int mCounter;

		void Start() {
			float d = DelayRangeEnd - DelayRangeStart;
			mTimer = DelayRangeStart + (Random.value * d);
			mCounter = 0;
		}

		void Update() {
			mTimer -= Time.deltaTime;
			if (mTimer <= 0) {

				GameObject enemyGO = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity) as GameObject;
				mCounter += 1;

				if (mCounter >= Limit) {
					Destroy(gameObject);
					return;
				}

				float d = DelayRangeEnd - DelayRangeStart;
				mTimer = DelayRangeStart + (Random.value * d);
			}
		}

	}
}