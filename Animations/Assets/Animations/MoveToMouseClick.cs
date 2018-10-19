using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MoveToMouseClick : MonoBehaviour {

	public LayerMask mask;
    public NavMeshAgent agent;
	public int mouse_button = 0;
    public Animator animation;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(mouse_button))
		{
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit, 10000.0f, mask) == true)
				transform.position = hit.point;
		}

        agent.destination = transform.position;

        if (Vector3.Distance(agent.transform.position, transform.position) > 0.0f)
            animation.SetBool("Movement", true);
	}

}
