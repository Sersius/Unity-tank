﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FSM_Alarm : MonoBehaviour {
    private bool player_detected = false;
    private bool in_alarm = false;
    private Vector3 patrol_pos;

    public GameObject alarm;
    public BansheeGz.BGSpline.Curve.BGCurve path;
    public NavMeshAgent destiny;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == alarm)
            in_alarm = true;
    }

    // Update is called once per frame
    void PerceptionEvent(PerceptionEvent ev)
    {
        if (ev.type == global::PerceptionEvent.types.NEW)
        {
            player_detected = true;
        }
    }

    // TODO 1: Create a coroutine that executes 20 times per second
    // and goes forever. Make sure to trigger it from Start()

    // Use this for initialization
    void Start()
    {
        StartCoroutine("routine");
    }

    IEnumerator routine()
    {
        while (player_detected == false)
        {
            Debug.Log("entering Patrol");
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("spotted");
    }

    // TODO 2: If player is spotted, jump to another coroutine that should
    // execute 20 times per second waiting for the player to reach the alarm

    IEnumerator spotted()
    {
        patrol_pos = transform.position;
        StopCoroutine("routine");
        path.gameObject.SetActive(false);
        while (in_alarm == false)
        {
            destiny.destination = alarm.transform.position;
            Debug.Log("going to alarm");
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("patrol");
    }


    // TODO 3: Create the last coroutine to have the tank waiting to reach
    // the point where he left the path, and trigger again the patrol

    IEnumerator patrol()
    {
        StopCoroutine("spotted");
        player_detected = false;
        in_alarm = false;
        while (Vector3.Distance(transform.position, patrol_pos) > 0.5f)
        {
            destiny.destination = patrol_pos;
            Debug.Log("returning to path");
            yield return new WaitForSeconds(0.05f);
        }
        destiny.ResetPath();
        path.gameObject.SetActive(true);
        StartCoroutine("routine");
        StopCoroutine("patrol");
    }

}