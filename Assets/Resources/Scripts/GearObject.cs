using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GearObject : MonoBehaviour
{
    public GameObject GearUI;
    public GameObject ThisGearUI;
    public Camera Camera;
    public Vector3 MousePos;

    public Color ThisColor;

    public bool IsDragging;
    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ThisColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;

        //Bool IsDragging serve para tudo parar de girar mesmo quando a gear está invisivel ali.
        if(IsDragging)
        {
            //Se soltar o mouse enquanto não está em nada.
            if(Input.GetMouseButtonUp(0))
            {
                GetComponent<SpriteRenderer>().color = ThisColor;
                GameObject.Destroy(ThisGearUI);
                IsDragging = false;

                //Limpa o que está segurado
                GameObject.Find("Inventory").GetComponent<Inventory>().HeldGear = null;
                GameObject.Find("Inventory").GetComponent<Inventory>().HeldGearObj = null;
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked on GearObj");
        ThisGearUI = Instantiate(GearUI , transform.position , Quaternion.identity);
        GhostGear ThisGhostGear = ThisGearUI.GetComponent<GhostGear>();
        ThisGhostGear.ThisGearObj = gameObject;
        
        ThisGearUI.GetComponent<RectTransform>().anchoredPosition = MousePos;
        
        ThisGearUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
        
        ThisGearUI.GetComponent<Image>().color = GetComponent<SpriteRenderer>().color;

        GameObject.Find("Inventory").GetComponent<Inventory>().HeldGear = new Gear(){ Color = GetComponent<SpriteRenderer>().color};
        GameObject.Find("Inventory").GetComponent<Inventory>().HeldGearObj = ThisGearUI;
        
        //Ao invés de destruir a gear quando clica nela, ela fica invisivível. Quando soltar o mouse ela fica visível de novo. Mais simples do que instanciar ela de novo.
        GetComponent<SpriteRenderer>().color = Color.clear;

        IsDragging=true;
    }
}
