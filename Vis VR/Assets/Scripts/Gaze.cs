using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    public Camera viewCamera;

    //public Canvas inventarioUI;

    public GameObject cursorPrefabMove;
    public float maxCursorDistance = 30;
    private GameObject cursorHexagInstance;

    public Image gaze;
    private Button bt;

	public UnityEngine.AI.NavMeshAgent agent;

    private float totalTime = 1;
    private float gazeTimer;

    public int distanceRay;
    private RaycastHit hit;

    public Text textoDoCanvas;
    public Image gazePoint;

    public AudioSource somBotao;

    private bool coolDowndoGaze = true;

    public bool incioJogo = false;

    public LayerMask layers;

    private NavMeshPath path;

    void Start()
    {
        path = new NavMeshPath();
        gazePoint.enabled = false;
        cursorHexagInstance = Instantiate(cursorPrefabMove);
        //inventarioUI.enabled = false;
    }

    void Update()
    {
        //Ray ray = new Ray(viewCamera.transform.position, viewCamera.transform.rotation * Vector3.forward);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));

        if (Physics.Raycast(ray, out hit, distanceRay, layers, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.CompareTag("Chao") && coolDowndoGaze && incioJogo)
            {
                //inventarioUI.enabled = false;
                gazeTimer += Time.deltaTime;
                gaze.fillAmount = gazeTimer / 0.7f;
                if (gaze.fillAmount == 1)
                {
                    if(agent.CalculatePath(hit.point, path))
                    {
                        cursorHexagInstance.transform.position = hit.point;
                        agent.SetDestination(hit.point);
                    }
                    
                    coolDowndoGaze = false;
                    StartCoroutine("CoolDownDoTimer");
                    GazeOff();
                }
            }
            else
            if (hit.transform.CompareTag("Selecionavel"))
            {
                gazeTimer += Time.deltaTime;
                gaze.fillAmount = gazeTimer / totalTime;
                if (gaze.fillAmount == 1)
                {
//                    somBotao.Play();

                    bt = hit.transform.gameObject.GetComponent<Button>();
                    bt.onClick.Invoke();
                    GazeOff();
                }
            }
            else
            {
                GazeOff();
            }
            
        }
        else
        {
            GazeOff();
        }

    }

    public void GazeOff()
    {
        gazeTimer = 0;
        gaze.fillAmount = 0;
    }

    IEnumerator CoolDownDoTimer(){
        yield return new WaitForSeconds(1f);
        coolDowndoGaze = true;
    }

    public IEnumerator ApagaTexto()
    {
        yield return new WaitForSeconds(3f);
        textoDoCanvas.text = "";
    }
}
