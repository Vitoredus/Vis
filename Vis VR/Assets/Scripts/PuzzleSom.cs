using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PuzzleSom : MonoBehaviour
{
    private bool playerIsOnArea = false;
    private bool alreadyOpen = false;

    public GameObject canvas;

    private Gaze gaze;
    private NavMeshAgent agent;

    private BoxCollider colisor;

    private int control=0;
    private int[] sequencia = new int[5]{1,3,2,1,3};

    public AudioSource nota1, nota2, nota3, musica;
    public AudioSource soundUI, correctAnswer;

    public Transform puzzleSpot;

    public Animator animaFinal, animaCanvas;

    private void Start() {
        agent = GameMaster.instance.GetPlayerMeshAgent();
        gaze = GameMaster.instance.GetPlayerGaze();
        colisor = GetComponent<BoxCollider>();
        ClosePuzzle();
    }

    void VerifyAnswer()
    {
        if (control == 5)
        {
            correctAnswer.Play();
            StartCoroutine(PuzzleCompleto());
            control++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOnArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOnArea = false;
            ClosePuzzle();
        }
    }

    public void Click()
    {
        if (!alreadyOpen && playerIsOnArea)
        {
            alreadyOpen = true;
            canvas.SetActive(true);
            animaCanvas.enabled = true;
            soundUI.Play();
        }
        agent.SetDestination(puzzleSpot.position);
    }

    void ClosePuzzle()
    {
        alreadyOpen = false;
        canvas.SetActive(false);
    }

    IEnumerator PuzzleCompleto()
    {
        animaCanvas.SetBool("isConcluido", true);
        yield return new WaitForSeconds(1.5f);
        canvas.SetActive(false);
        colisor.enabled = false;
        gaze.GazeOff();
        GetComponent<PuzzleSom>().enabled = false;
        animaFinal.enabled = true;
    }

    #region Tocar Notas
    public void Som1()
    {
        nota1.Play();
        if(sequencia[control] == 1)
        {
            control++;
        }
        else
        {
            control = 0;
        }
        VerifyAnswer();
    }
    public void Som2()
    {
        nota2.Play();
        if(sequencia[control] == 2)
        {
            control++;
        }else
        {
            control = 0;
        }
        VerifyAnswer();
    }
    public void Som3()
    {
        nota3.Play();
        if(sequencia[control] == 3)
        {
            control++;
        }else
        {
            control = 0;
        }
        VerifyAnswer();
    }
    #endregion

    public void TocaMusica()
    {
        musica.Play();
    }
}
