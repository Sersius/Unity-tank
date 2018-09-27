using UnityEngine;
using System.Collections;

public class KinematicFlee : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 6: To create flee just switch the direction to go
        Vector3 direction = -(move.target.transform.position - move.transform.position);
        direction.y = 0.0f;
        direction.Normalize();

        move.SetMovementVelocity(direction * move.max_mov_velocity);
    }
}
