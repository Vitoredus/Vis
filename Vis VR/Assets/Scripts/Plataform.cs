using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plataform : MonoBehaviour
{
    private Gaze gaze;

    private Plataform plat;
    public GameObject obj;
    private Inventory inv;
    private ObjInvControl objSel;
    private GameMaster gm;

    public GameObject agua;

    [SerializeField] private string textoDaPlataforma, textoDaPlataformaEmIngles;
    private string[] textoDoItemErrado = new string[3] {"Não funciona", "Deu ruim", "Eu deveria tentar outra coisa"};
    private string[] textoDoItemErradoEmIngles  = new string[3] {"It doesn't work", "It's no use doing that", "I should try something else"};

    public string plataformTag;

    public GameObject inventarioUI;

    public AudioSource somCerto, somErrado, bombaSom;

    private void Start()
    {
        gaze = GameMaster.instance.GetPlayerGaze();
        inv = GameMaster.instance.GetInventory();
        inventarioUI = inv.transform.gameObject;
        gm = GameMaster.instance;
    }


    public void Colocar()
    {
        if (inv.objetoSelecionado != null)
        {
            objSel = inv.objetoSelecionado.GetComponent<ObjInvControl>();
            if(objSel.tagObj == plataformTag){
                somCerto.Play();
                Image slotIcon = inv.slots[objSel.slotNum].GetComponent<Image>();
                slotIcon.sprite = gm.iconeInvetarioVazio;
                objSel.transform.position = obj.transform.position;
                objSel.transform.rotation = obj.transform.rotation;
                inv.objects[objSel.slotNum] = null;
                objSel.GetComponent<ObjInvControl>().enabled = false;
                Resetar();
                //GetComponent<BoxCollider>().enabled = false;
                if(objSel.tagObj == "wrench")
                {
                    agua.SetActive(true);
                    gm.taLigado = true;
                    bombaSom.Play();
                }
                else 
                if(objSel.tagObj == "Flor1")
                {
                    gm.flor1 = true;
                }else
                if (objSel.tagObj == "Flor2")
                {
                    gm.flor2 = true;
                }
                else
                if (objSel.tagObj == "Flor3")
                {
                    gm.flor3 = true;
                }
                else
                if (objSel.tagObj == "Flor4")
                {
                    gm.taComAFlor = true;
                }else
                if(objSel.tagObj == "key")
                {
                    gm.Cabo();
                }
                GetComponent<Plataform>().enabled = false;
            }
            else
            {
                somErrado.Play();
                Resetar();
                if (gm.portugues)
                {
                    gaze.textoDoCanvas.text = textoDoItemErrado[Random.Range(0,3)];
                }
                else
                {
                    gaze.textoDoCanvas.text = textoDoItemErradoEmIngles[Random.Range(0,3)];
                }
                gaze.StartCoroutine("ApagaTexto");
            }
            
        }
        else
        {
            if (gm.portugues)
            {
                gaze.textoDoCanvas.text = textoDaPlataforma;
            }
            else
            {
                gaze.textoDoCanvas.text = textoDaPlataformaEmIngles;
            }
            gaze.StartCoroutine("ApagaTexto");
        }   
    }

    void Resetar(){
        objSel.selecionado = false;
        objSel.ResetaImagemDoGaze();
        inv.objetoSelecionado = null;
    }

}
