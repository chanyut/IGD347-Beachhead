using UnityEngine;

using System.Collections;
using System.Collections.Generic;


namespace Beachhead.AI {


	public class BHEnemyPath : MonoBehaviour {

		public List<Transform> Waypoint;

		public Transform GetNextPoint(Transform fromPoint) {
			if (fromPoint == null) {
				return Waypoint[0];
			}
			else {
				int index = Waypoint.IndexOf(fromPoint);
				if (index + 1 < Waypoint.Count) {
					return Waypoint[index + 1];
				}
				else {
					return null;
				}
			}
		}

		void OnDrawGizmos() {

			Vector3 p1;
			Vector3 p2;

			Gizmos.color = Color.blue;

			for (int i=0; i<Waypoint.Count - 1; i++) {
				p1 = Waypoint[i].position;
				p2 = Waypoint[i+1].position;
				Gizmos.DrawLine(p1, p2);
				Gizmos.DrawWireSphere(p1, 0.5f);
				Gizmos.DrawWireSphere(p2, 0.5f);
			}

		}

	}

}