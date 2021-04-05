using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class PuzzleCores : MonoBehaviour
{
    private string[] numRomanos = new string[5] { "I", "II", "III", "VI", "V" };
    public Text esp1, esp2, esp3, esp4;
    private int num1 = 0, num2 = 0, num3 = 0, num4 = 0;

    private bool playerIsOnArea= false;
    private bool alreadyOpen = false;

    public GameObject canvas;

    private BoxCollider colisor;

    public AudioSource soundUI, correctAnswer;

    private bool resolvido;

    private Gaze gaze;
    private NavMeshAgent agent;

    public Transform puzzleSpot;

    public Animator animaFinal, animaCanvas;

    private void Start() {
        colisor = GetComponent<BoxCollider>();
        agent = GameMaster.instance.GetPlayerMeshAgent();
        gaze = GameMaster.instance.GetPlayerGaze();
        UpdateUI();
        ClosePuzzle();
    }

    void UpdateUI()
    {
        esp1.text = numRomanos[num1];
        esp2.text = numRomanos[num2];
        esp3.text = numRomanos[num3];
        esp4.text = numRomanos[num4];

        if (num1 == 3 && num2 == 1 && num3 == 4 && num4 == 0 && !resolvido)
        {
            correctAnswer.Play();
            StartCoroutine(PuzzleCompleto());
            resolvido = true;
        }
    }

    IEnumerator PuzzleCompleto()
    {
        animaCanvas.SetBool("isConcluido", true);
        yield return new WaitForSeconds(1.5f);
        canvas.SetActive(false);
        colisor.enabled = false;
        gaze.GazeOff();
        GetComponent<PuzzleCores>().enabled = false;
        animaFinal.enabled = true;
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


    #region Controla valores
    public void Aumenta1()
    {
        if(num1==4)
        {
            num1=0;
        }else
        {
            num1++;
        }
        UpdateUI();
    }
    public void Aumenta2()
    {
        if(num2==4)
        {
            num2=0;
        }else
        {
            num2++;
        }
        UpdateUI();
    }
    public void Aumenta3()
    {
        if(num3==4)
        {
            num3=0;
        }else
        {
            num3++;
        }
        UpdateUI();
    }
    public void Aumenta4()
    {
        if(num4==4)
        {
            num4=0;
        }else
        {
            num4++;
        }
        UpdateUI();
    }

    public void Diminui1()
    {
        if(num1==0)
        {
            num1=4;
        }else
        {
            num1--;
        }
        UpdateUI();
    }
    public void Diminui2()
    {
        if(num2==0)
        {
            num2=4;
        }else
        {
            num2--;
        }
        UpdateUI();
    }
    public void Diminui3()
    {
        if(num3==0)
        {
            num3=4;
        }else
        {
            num3--;
        }
        UpdateUI();
    }
    public void Diminui4()
    {
        if(num4==0)
        {
            num4=4;
        }else
        {
            num4--;
        }
        UpdateUI();
    }
    #endregion
}
