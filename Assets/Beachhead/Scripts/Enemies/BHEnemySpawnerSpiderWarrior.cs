using UnityEngine;
using System.Collections;

using Beachhead.AI;


namespace Beachhead.Enemies {

	public class BHEnemySpawnerSpiderWarrior : BHEnemySpawner {

		public GameObject EnemyPrefab;
		public BHEnemyPath EnemyPath;

		protected override GameObject CreateEnemy () {
			GameObject newEnemyGO = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity) as GameObject;
			BHEnemySpiderWarrior spiderWarrior = newEnemyGO.GetComponent<BHEnemySpiderWarrior>();
			spiderWarrior.CurrentPath = EnemyPath;
			return newEnemyGO;
		}

	}

}