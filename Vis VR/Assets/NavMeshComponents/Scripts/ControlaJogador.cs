using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaJogador : MonoBehaviour
{
	public Camera cam;	
	public NavMeshAgent agent;

    void Update()
    {
        //int layerMask = LayerMask.GetMask("Chao");
        if (Input.GetMouseButtonDown(0)) {
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
            RaycastHit hit;
    		if(Physics.Raycast(ray, out hit)) {
                var selection = hit.transform;                
                if (selection.CompareTag("Selecionavel"))
                {
                    //Debug.Log(selection);
                    agent.SetDestination(hit.point);
                }      
    		}
	 	} 
    }

}
