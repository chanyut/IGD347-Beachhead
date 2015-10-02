using UnityEngine;
using System.Collections;

using Beachhead.AI;

namespace Beachhead.Enemies {

	public class BHEnemySpiderWarrior : BHEnemy {

		enum SpiderWarriorStateEnum {
			Idle,
			Move,
			Die
		}

		public float MoveSpeed;

		public BHEnemyPath CurrentPath;
		public CharacterController ChController;
		public Animator SpiderAnimator;

		Transform mMoveToPoint;
		SpiderWarriorStateEnum mState;

		void Awake() {
			mEnemyType = BHEnemyTypeEnum.SpiderWarrior;
		}

		protected override void Start () {

			mMoveToPoint = CurrentPath.GetNextPoint(null);
			mState = SpiderWarriorStateEnum.Idle;
		}

		protected override void Update () {

			if (GetRemainHp() <= 0) {
				mState = SpiderWarriorStateEnum.Die;
				SpiderAnimator.SetTrigger("Die");
			}

			if (mState == SpiderWarriorStateEnum.Idle) {
				if (mMoveToPoint != null) {
					mState = SpiderWarriorStateEnum.Move;
				}

				SpiderAnimator.SetFloat("Speed", 0);
			}
			else if (mState == SpiderWarriorStateEnum.Move) {
				if (mMoveToPoint != null) {
					Vector3 direction = mMoveToPoint.position - transform.position;
					direction.Normalize();
					transform.forward = Vector3.Lerp(transform.forward, direction, 10 * Time.deltaTime);
					ChController.SimpleMove(transform.forward * MoveSpeed);
					
					Vector3 diff = mMoveToPoint.position - transform.position;
					float distance = diff.magnitude;
					if (distance <= 1f) {
						Transform nextPoint = CurrentPath.GetNextPoint(mMoveToPoint);
						mMoveToPoint = nextPoint;
					}

					SpiderAnimator.SetFloat("Speed", MoveSpeed);
				}
				else {
					mState = SpiderWarriorStateEnum.Idle;
				}
			}
			else if (mState == SpiderWarriorStateEnum.Die) {

			}
		}

		void OnDrawGizmos() {
			if (mMoveToPoint == null) {
				return;
			}

			Gizmos.color = Color.yellow;

			Vector3 p1 = transform.position;
			Vector3 p2 = mMoveToPoint.position;
			Gizmos.DrawLine(p1, p2);
		}


	}

}