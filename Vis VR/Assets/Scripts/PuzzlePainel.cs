using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class PuzzlePainel : MonoBehaviour
{
    private bool playerIsOnArea = false;
    private bool alreadyOpen = false;

    public GameObject canvas;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Image> images;

    private int num1=0, num2=0, num3=0, num4=0, num5=0, num6=0;

    private BoxCollider colisor;

    public AudioSource soundUI, correctAnswer;

    private bool resolvido;

    public Animator animaFinal, animaCanvas;

    public Transform puzzleSpot;
    private NavMeshAgent agent;

    private Gaze gaze;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = sprites[0];
        }

        colisor = GetComponent<BoxCollider>();
        agent = GameMaster.instance.GetPlayerMeshAgent();
        gaze = GameMaster.instance.GetPlayerGaze();
        ClosePuzzle();
    }

    void VerifyAnswer()
    {
        if (num1 == 5 && num2 == 2 && num3 == 1 && num4 == 3 && num5 == 0 && num6 == 4 && !resolvido)
        {
            correctAnswer.Play();
            StartCoroutine(PuzzleCompleto());
            resolvido = true;
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
        colisor.enabled = false;
        gaze.GazeOff();
        GetComponent<PuzzlePainel>().enabled = false;
        animaFinal.enabled = true;
    }

    public void Click()
    {
        if (!alreadyOpen && playerIsOnArea)
        {
            alreadyOpen = true;
            canvas.SetActive(true);
            colisor.enabled = false;
            soundUI.Play();
        }
        agent.SetDestination(puzzleSpot.position);
    }

    void ClosePuzzle()
    {
        alreadyOpen = false;
        canvas.SetActive(false);
        colisor.enabled = true;
    }

    #region Mudar Imagens
    public void MudaImage1()
    {
        if (num1 > 5)
        {
            num1 = 0;
        }
        else
        {
            num1++;
        }
        images[0].sprite = sprites[num1];
        VerifyAnswer();
    }

    public void MudaImage2()
    {
        if (num2 > 5)
        {
            num2 = 0;
        }
        else
        {
            num2++;
        }
        images[1].sprite = sprites[num2];
        VerifyAnswer();
    }

    public void MudaImage3()
    {
        if (num3 > 5)
        {
            num3 = 0;
        }
        else
        {
            num3++;
        }
        images[2].sprite = sprites[num3];
        VerifyAnswer();
    }

    public void MudaImage4()
    {
        if (num4 > 5)
        {
            num4 = 0;
        }
        else
        {
            num4++;
        }
        images[3].sprite = sprites[num4];
        VerifyAnswer();
    }

    public void MudaImage5()
    {
        if (num5 > 5)
        {
            num5 = 0;
        }
        else
        {
            num5++;
        }
        images[4].sprite = sprites[num5];
        VerifyAnswer();
    }

    public void MudaImage6()
    {
        if (num6 > 5)
        {
            num6 = 0;
        }
        else
        {
            num6++;
        }
        images[5].sprite = sprites[num6];
        VerifyAnswer();
    }
    #endregion
}
