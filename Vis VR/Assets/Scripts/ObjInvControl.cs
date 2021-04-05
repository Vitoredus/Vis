using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjInvControl : MonoBehaviour
{
    private Gaze gaze;
    private Inventory inv;
    public Sprite icon;

    [SerializeField] private string nomeDoObjeto;
    [SerializeField] private string nomeDoObjetoEmIngles;

    [HideInInspector] public int slotNum;

    [HideInInspector]public MeshRenderer mesh;
    [HideInInspector]public BoxCollider box;

    private GameMaster gm;

    public string tagObj;

    [HideInInspector] public bool selecionado = false;

    public AudioSource somDoItem;

    private void Start()
    {
        gaze = GameMaster.instance.GetPlayerGaze();
        inv = GameMaster.instance.GetInventory();
        gm = GameMaster.instance;
    }

    public void VerificaSlots()
    {
        for (int i = 0; i < inv.objects.Length; i++)
        {
            if(inv.objects[i] == null)
            {
                Image slotIcon = inv.slots[i].GetComponent<Image>();
                transform.position = Vector3.down*3;
                inv.objects[i] = this.gameObject;
                slotIcon.sprite = icon;
                slotNum = i;
                somDoItem.Play();

                gaze.textoDoCanvas.text = gm.portugues ? nomeDoObjeto : nomeDoObjetoEmIngles;
                gaze.StartCoroutine("ApagaTexto");
                break;
            }
        }
    }

    public void TrocaImageDoGaze()
    {
        gaze.gazePoint.enabled = true;
        gaze.gazePoint.sprite = icon;
    }

    public void ResetaImagemDoGaze()
    {
        gaze.gazePoint.sprite = null;
        gaze.gazePoint.enabled = false;
    }
}
