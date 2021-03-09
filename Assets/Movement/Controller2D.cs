using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller2D : RaycastController	 {
	public CollisionInfo collisions;

	public void Move (Vector3 velocity){
		UpdateRaycastOrigins();
		collisions.Reset();

		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}
		transform.Translate (velocity, Space.World);
	}

	void HorizontalCollisions(ref Vector3 velocity){
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.botL : raycastOrigins.botR;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity){
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.botL : raycastOrigins.topL;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY);
            RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public void Reset(){
			above = below = false;
			left = right = false;
		}
	}
}
