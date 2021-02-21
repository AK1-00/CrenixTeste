using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GearUI : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public GameObject CanvasObj;
    public Canvas Canvas;
    public RectTransform RectTransform;

    public GameObject InventoryObj;
    public Inventory Inventory;

    public CanvasGroup CanvasGroup;

    public int InventorySlot;

    public float ScaleFactor;
    void Start()
    {
        CanvasObj = GameObject.Find("Canvas");
        Canvas = CanvasObj.GetComponent<Canvas>();
        RectTransform = GetComponent<RectTransform>();

        CanvasGroup = GetComponent<CanvasGroup>();

        InventoryObj = GameObject.Find("Inventory");
        Inventory = InventoryObj.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        ScaleFactor = Canvas.scaleFactor;
    }

    public void OnDrag(PointerEventData EventData)
    {
        RectTransform.anchoredPosition += EventData.delta / Canvas.scaleFactor;
        CanvasGroup.blocksRaycasts = false;

        //Para o script detectar qual slot o jogador está usando. 
        Inventory.HeldObj = InventorySlot;
        Inventory.HeldGear = Inventory.GearList[InventorySlot];
        Inventory.HeldGearObj = gameObject;
        Inventory.LastHeldObj = InventorySlot;
    }

    public void OnEndDrag(PointerEventData EventData)
    {
        //Se você soltar o mouse ele sempre volta por slot que ele estava.

        RectTransform.anchoredPosition = Inventory.Slots[InventorySlot].GetComponent<RectTransform>().anchoredPosition;
        CanvasGroup.blocksRaycasts = true;
        Inventory.HeldObj = -1;
        Inventory.HeldGear = null;
    }
}
