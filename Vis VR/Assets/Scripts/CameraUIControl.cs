using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIControl : MonoBehaviour
{
    private Transform player;

    private bool playerInside = false;

    [SerializeField] GameObject inventory = null;

    private void Start()
    {
        inventory.SetActive(false);
        player = GameMaster.instance.GetPlayerGaze().transform;
    }

    private void Update()
    {
        if (!playerInside)
            return;

        Vector3 lookDir = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.SetActive(false);
            playerInside = false;
        }
    }


}
