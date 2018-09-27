using UnityEngine;
using System.Collections;

public class KinematicFaceMovement : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () {
        // TODO 7: rotate the whole tank to look in the movement direction
        // Extremnely similar to TODO 2

        move.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(move.mov_velocity.x, move.mov_velocity.z)) * Mathf.Rad2Deg, Vector3.up);

    }
}
