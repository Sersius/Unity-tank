using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour {

	Move move;
	SteeringSeek seek;
    private BGCcMath math;
    public GameObject path;
    Vector3 currpoint;
    public float calcdistance = 0.1f;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
        math = path.GetComponent<BGCcMath>();

        Vector3 clospoint = math.Curve.Points[0].PositionWorld;

        for (int i = 0; i < math.Curve.PointsCount; i++)
        {
            Vector3 distance_tank = transform.position - clospoint;
            Vector3 distance = transform.position - math.Curve.Points[i].PositionWorld;

            if (distance.magnitude < distance_tank.magnitude)
            {
                clospoint = math.Curve.Points[i].PositionWorld;
            }
        }

        currpoint = clospoint;

	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        Vector3 distance = transform.position - currpoint;


        seek.Steer(currpoint);
	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
