using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_velocity = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

    private Vector3[] velocityp = new Vector3[6];
    private float[] rotationp = new float[6];

    [Header("-------- Read Only --------")]
	public Vector3 movement = Vector3.zero;
	public float rotation = 0.0f; // degrees

	// Methods for behaviours to set / add velocities
	public void SetMovementVelocity (Vector3 velocity) 
	{
        movement = velocity;
	}

	public void AccelerateMovement (Vector3 velocity, int prio) 
	{
        if (velocityp[prio].Equals(Vector3.zero))
            velocityp[prio] = velocity;
        else
        {
            velocityp[prio] += velocity;

            if (velocityp[prio].magnitude > max_mov_velocity)
            {
                velocityp[prio].Normalize();
                velocityp[prio] *= max_mov_acceleration;
            }
        }
    }

	public void SetRotationVelocity (float rotation_velocity, int prio) 
	{
        rotation = rotation_velocity;
    }

	public void AccelerateRotation (float rotation_acceleration, int prio) 
	{
        if (rotationp[prio].Equals(Vector3.zero))
            rotationp[prio] = rotation_acceleration;
        else
        {
            rotationp[prio] += rotation_acceleration;

            if (rotationp[prio] > max_rot_acceleration)
                rotationp[prio] *= max_mov_acceleration;
        }
    }

	
	// Update is called once per frame
	void Update () 
	{
		// cap velocity
		if(movement.magnitude > max_mov_velocity)
		{
			movement.Normalize();
			movement *= max_mov_velocity;
		}

		// cap rotation
		Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

		// rotate the arrow
		float angle = Mathf.Atan2(movement.x, movement.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = movement.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += movement * Time.deltaTime;

	}
}
