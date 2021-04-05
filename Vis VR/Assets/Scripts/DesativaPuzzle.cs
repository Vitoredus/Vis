using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesativaPuzzle : MonoBehaviour
{
    public bool puzzleAberto = false;

    private void OnTriggerExit(Collider other)
    {
        puzzleAberto = false;
    }
}
