using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// Estou usando esse objeto diferente para pegar a gear do mundo pois não encontrei um jeito de instanciar um objeto e imediatamente começar um drag. Então esse aqui faz um "fake drag" colocando 
// a posição dela pro mouse.
public class GhostGear : MonoBehaviour
{

    public GameObject CanvasObj;
    public Canvas Canvas;
    public RectTransform RectTransform;
    public CanvasGroup CanvasGroup;
    public GameObject ThisGearObj;
    
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        CanvasObj = GameObject.Find("Canvas");
        Canvas = CanvasObj.GetComponent<Canvas>();

        CanvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        CanvasGroup.blocksRaycasts = false;
    }
}
