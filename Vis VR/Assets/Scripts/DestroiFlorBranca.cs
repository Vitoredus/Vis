using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiFlorBranca : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.Find("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.taComAFlor && gm.taLigado)
        {
            StartCoroutine(florVerde());
        }
    }

    IEnumerator florVerde()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
