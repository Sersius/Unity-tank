using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 4: As with arrive, we first construct our ideal rotation
        // then accelerate to it. Use Mathf.DeltaAngle() to wrap around PI
        // Is the same as arrive but with angular velocities
        float rotTarget = Mathf.Atan2(move.movement.x, move.movement.z);
        float currRot = Mathf.Atan2(transform.forward.x, transform.forward.z);

        rotTarget = Mathf.Rad2Deg * rotTarget;
        currRot = Mathf.Rad2Deg * currRot;

        float rotationdif = Mathf.DeltaAngle(rotTarget, currRot);

        rotationdif = Mathf.Clamp(rotationdif, -move.max_rot_acceleration, move.max_rot_acceleration);

        float acceleration = rotationdif * move.max_rot_acceleration;

        if (Mathf.Abs(rotationdif) < slow_angle)
        {
            rotationdif *= time_to_target;

            if (Mathf.Abs(rotationdif) < min_angle)
                rotationdif = 0.0f;

            acceleration = rotationdif * move.max_rot_acceleration;
        }

        move.AccelerateRotation(acceleration);
    }
}
