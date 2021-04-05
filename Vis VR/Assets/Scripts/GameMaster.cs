using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    [HideInInspector] public bool portugues;//controla o idioma do jogo, true para portugues e false para ingles

    [HideInInspector] public bool flor1, flor2, flor3;

    public bool taLigado, taComAFlor;

    public GameObject florVerde, obj, porta;

    [SerializeField] private NavMeshAgent playerAgent = null;
    [SerializeField] private Gaze playerGaze = null;

    [SerializeField] private Inventory inventory;

    public Canvas menuInicial;

    public Text gameOverText;

    private bool funfo;

    public Animator animaFinalChave, animaFinalPortao;

    public Sprite iconeInvetarioVazio;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        playerAgent.enabled = false;
    }

    private void Update()
    {
        if(flor1 && flor2 && flor3)
        {
            animaFinalChave.enabled = true;
            flor1 = false;
        }

        if (taLigado && taComAFlor && !funfo)
        {
            StartCoroutine(TrocaFlor());
            funfo = true;
        }

    }

    #region Gets

    public NavMeshAgent GetPlayerMeshAgent()
    {
        return playerAgent;
    }

    public Gaze GetPlayerGaze()
    {
        return playerGaze;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    #endregion

    IEnumerator TrocaFlor()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(florVerde, obj.transform.position, obj.transform.rotation);
    }

    public void SetLanguage(bool chosedLanguage)//true para portugues e false para ingles
    {
        portugues = chosedLanguage;
        playerAgent.enabled = true;
        playerGaze.incioJogo = true;
        Destroy(menuInicial.gameObject);
    }

    public void Cabo()
    {
        Destroy(porta.gameObject);
        if(portugues){
            gameOverText.text = "Parabens. Voce escapou.";
        }else{
            gameOverText.text = "Congratulations. You escaped.";
        }
        playerAgent.enabled = false;
        animaFinalPortao.enabled = true;
        playerGaze.enabled = false;
    }
}
