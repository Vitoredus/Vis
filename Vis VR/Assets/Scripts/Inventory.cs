using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] objects = new GameObject[4];
    public GameObject[] slots;
    public GameObject objetoSelecionado;

    public AudioSource pegaItem, devolveItem;

    public void SelecionaSlot(int index)
    {
        if (objects[index] != null)
        {
            if (objetoSelecionado == objects[index].gameObject)
            {
                devolveItem.Play();
                objetoSelecionado = null;
                objects[index].GetComponent<ObjInvControl>().ResetaImagemDoGaze();
            }
            else
            {
                pegaItem.Play();
                objects[index].GetComponent<ObjInvControl>().TrocaImageDoGaze();
                objetoSelecionado = objects[index].gameObject;
            }
        }
    }

}
