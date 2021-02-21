using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSlot : MonoBehaviour
{
    public bool IsHovering;
    public GameObject InventoryObj;
    public Inventory Inventory;
    public GameObject GearObj;
    public GameObject ThisGear;

    public GearSlotsManager Manager;
    
    void Start()
    {
        InventoryObj = GameObject.Find("Inventory");
        Inventory = InventoryObj.GetComponent<Inventory>();
        Manager = GameObject.Find("GearSlots").GetComponent<GearSlotsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Coloca a gear no lugar se não tiver nenhuma gear em posição.
        if(Input.GetMouseButtonUp(0) && IsHovering && Inventory.LastHeldObj!=-1 && ThisGear==null)
        {
            ThisGear = Instantiate ( GearObj , transform.position , Quaternion.identity );
            ThisGear.GetComponent<SpriteRenderer>().color = Inventory.GearList[Inventory.LastHeldObj].Color;
            Inventory.GearList[Inventory.LastHeldObj] = null;
            Inventory.HeldGear = null;
            Destroy(Inventory.HeldGearObj);
        }
    }

    void OnMouseOver()
    {
        IsHovering=true;
    }

    void OnMouseExit()
    {
        IsHovering=false;
    }
}
