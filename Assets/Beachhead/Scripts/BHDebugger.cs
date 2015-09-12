using UnityEngine;
using System.Collections;

public class BHDebugger : MonoBehaviour {

	public float DebugTimeScale;

	float mLastTimeScale;
	
	// Update is called once per frame
	void Update () {
		if (mLastTimeScale != DebugTimeScale) {
			Time.timeScale = DebugTimeScale;
			mLastTimeScale = DebugTimeScale;
		}
	}
}
