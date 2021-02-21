using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory Inventory;
    public GameObject Canvas;
    public GameObject[] GearSlots;

    public Text ResetText;
    public GameObject ResetTextObj;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ResetFunction()
    {
        Debug.Log("PRESSED");
        Inventory.GearList = new Gear[5]{
            new Gear(){Color = Color.magenta},
            new Gear(){Color = Color.cyan},
            new Gear(){Color = Color.yellow},
            new Gear(){Color = Color.green},
            new Gear(){Color = new Color(0.5f,0f,1f,1f)},
         };

        for( int i = 0 ; i<GearSlots.Length ; i++ )
        {
            Destroy(GearSlots[i].GetComponent<GearSlot>().ThisGear);
        }

        for( int i = 0 ; i<Inventory.GearUIList.Length ; i++ )
         {
            Destroy(Inventory.GearUIList[i]);
         }

        for( int i = 0 ; i<Inventory.GearList.Length ; i++)
        {
            GameObject NewGear = Instantiate(Inventory.GearObject , transform.position , Quaternion.identity);
            NewGear.transform.SetParent(Canvas.transform,false);
            NewGear.GetComponent<RectTransform>().anchoredPosition = Inventory.Slots[i].GetComponent<RectTransform>().anchoredPosition;
            NewGear.GetComponent<Image>().color = Inventory.GearList[i].Color;
            NewGear.GetComponent<GearUI>().InventorySlot = i;
            Inventory.GearUIList[i] = NewGear;
         }

         
    }

    public void OnPointerDown( PointerEventData EventData )
    {
        Debug.Log("Click");
        ResetText.color = new Color(0.5f , 0.5f , 0.5f , 1f);
        ResetTextObj.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f , -20f);

    }

    public void OnPointerUp( PointerEventData EventData )
    {
        Debug.Log("Up!");
        ResetText.color = Color.white;
        ResetTextObj.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f , 20f);
    }
}

