using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaVilao : MonoBehaviour
{
	public GameObject player;
	public NavMeshAgent agent;

	public float machineTime = 3f;
	public float changeState = 50f;
	public bool walking = false;
	private float stateTime = 0;
	private float totalSecs = 0;

    // Update is called once per frame
    void Update()
	{
	    totalSecs += Time.deltaTime;
		stateTime += Time.deltaTime;
	
		if (walking) 
		{
		    agent.SetDestination(player.transform.position);
		}
		if (totalSecs >= 1f) 
		{
		    totalSecs = 0f;
		    if (stateTime >= machineTime) {
		        stateTime = 0f;
		        if (Random.Range(0f, 100f) <= changeState) {
		            walking = true;
		        }
		        else {
		            walking = false;
		        }
		    }
		}
	
	}
}
	