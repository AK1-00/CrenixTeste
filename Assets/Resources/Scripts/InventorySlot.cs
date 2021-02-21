using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsHovering;
    public Inventory Inventory;
    public int ThisInventoryPosition;
    public GameObject GearObject;
    void Start()
    {
        Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsHovering && Inventory.HeldGear!=null)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Debug.Log("Drop");
                Inventory.GearList[ThisInventoryPosition] = Inventory.HeldGear;
                GameObject NewGear = Instantiate(GearObject , transform.position , Quaternion.identity);
                NewGear.transform.SetParent(GameObject.Find("Canvas").transform,false);
                NewGear.GetComponent<RectTransform>().anchoredPosition = Inventory.Slots[ThisInventoryPosition].GetComponent<RectTransform>().anchoredPosition;
                NewGear.GetComponent<Image>().color = Inventory.GearList[ThisInventoryPosition].Color;
                NewGear.GetComponent<GearUI>().InventorySlot = ThisInventoryPosition;

                Inventory.GearUIList[ThisInventoryPosition] = NewGear;

                //Destrói a GhostGear e a Gear invisível
                Destroy(Inventory.HeldGearObj.GetComponent<GhostGear>().ThisGearObj);
                Destroy(Inventory.HeldGearObj);

                //Limpa o que está sendo segurado.
                GameObject.Find("Inventory").GetComponent<Inventory>().HeldGear = null;
                GameObject.Find("Inventory").GetComponent<Inventory>().HeldGearObj = null;
            }
        }
    }

    public void OnPointerEnter( PointerEventData EventData )
    {
        Debug.Log("Enter");
        IsHovering=true;
    }

    public void OnPointerExit( PointerEventData EventData )
    {
        Debug.Log("Exit");
        IsHovering=false;
    }
}
