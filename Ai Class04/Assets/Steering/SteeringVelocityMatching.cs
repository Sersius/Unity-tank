using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : MonoBehaviour {

	public float time_to_target = 0.25f;

	Move move;
	Move target_move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target_move)
		{
            // TODO 5: First come up with your ideal velocity
            // then accelerate to it.
            Vector3 vel = target_move.movement - move.movement;

            Vector3 acceleration = vel / time_to_target;

            if(acceleration.magnitude > move.max_rot_acceleration)
            {
                acceleration.Normalize();
                acceleration *= move.max_mov_acceleration;
            }

            move.AccelerateMovement(acceleration);

		}
	}
}
