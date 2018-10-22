using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {

    public LayerMask mask;
    public float distance = 10.0f;
    public Camera camera;
    Ray ray;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, mask);

        foreach(Collider col in colliders)
        {
            Plane[] frustrum= GeometryUtility.CalculateFrustumPlanes(camera);
            bool isOnFrustrum = GeometryUtility.TestPlanesAABB(frustrum, col.bounds);

            if (isOnFrustrum == true)
            {
                Debug.Log("Aha, te vi pillín!");
            }
        }
	}

    private void OnDrawGizmosSelected()
    {
        if(isActiveAndEnabled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
}
