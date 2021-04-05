using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Caixa : MonoBehaviour
{
    private int num1, num2, num3;
    public int resp1, resp2, resp3;
    public Text botao1, botao2, botao3;

    private bool playerIsOnArea = false;
    private bool alreadyOpen = false;

    public GameObject canvas;
    public GameObject box;

    public GameObject reward;

    public Transform puzzleSpot;

    private Gaze gaze;
    private NavMeshAgent agent;

    public Animator animaFinal, animaCanvas;

    public AudioSource soundUI, correctAnswer;

    private void Start() {
        agent = GameMaster.instance.GetPlayerMeshAgent();
        gaze = GameMaster.instance.GetPlayerGaze();
        UpdateUI();
        ClosePuzzle();
    }

    void UpdateUI()
    {
        botao1.text = ("" + num1);
        botao2.text = ("" + num2);
        botao3.text = ("" + num3);
    }

    void VerifyAnswer()
    {
        if (num1 == resp1 && num2 == resp2 && num3 == resp3)
        {
            correctAnswer.Play();
            StartCoroutine(PuzzleCompleto());
            resp1++;
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

    IEnumerator PuzzleCompleto()
    {
        animaCanvas.SetBool("isConcluido", true);
        yield return new WaitForSeconds(1.5f);
        canvas.SetActive(false);
        gaze.GazeOff();
        transform.tag = "Complete";
        reward.SetActive(true);
        animaFinal.enabled = true;
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

    #region Funçoes de alterar valores
    public void AumentaDo1()
    {
        if (num1 < 10)
        {
            num1++;
        }
        VerifyAnswer();
        UpdateUI();
    }
    public void AumentaDo2()
    {
        if(num2 < 10)
        {
            num2++;
        }
        VerifyAnswer();
        UpdateUI();
    }
    public void AumentaDo3()
    {
        if(num3 < 10)
        {
            num3++;
        }
        VerifyAnswer();
        UpdateUI();
    }
    public void DiminuiDo1()
    {
        if (num1 > 0)
        {
            num1--;
        }
        VerifyAnswer();
        UpdateUI();
    }
    public void DiminuiDo2()
    {
        if (num2 > 0)
        {
            num2--;
        }
        VerifyAnswer();
        UpdateUI();
    }
    public void DiminuiDo3()
    {
        if (num3 > 0)
        {
            num3--;
        }
        VerifyAnswer();
        UpdateUI();
    }
    #endregion
}
