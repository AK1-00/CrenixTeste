using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearSlotsManager : MonoBehaviour
{
    public GameObject[] GearSlots;
    public GameObject[] Gears;
    public bool IsFull;

    public bool IsHoveringAny;

    public Text DialogueText;
    void Start()
    {
        int count=0;
        foreach (Transform child in transform)
        {
            count++;
        }
        GearSlots = new GameObject[count];
        for (int i=0 ; i < count ; i++)
        {
            GearSlots[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Se algum slot estiver vazio, não gira.
        IsFull = true;
        IsHoveringAny=false;
        for (int i=0 ; i < GearSlots.Length ; i++)
        {
            if (GearSlots[i].GetComponent<GearSlot>().ThisGear == null )
            {
                IsFull = false;
            } else if(GearSlots[i].GetComponent<GearSlot>().ThisGear.GetComponent<GearObject>().IsDragging)
            {
                IsFull = false;
            }

            //Isso verifica se o mouse não está em cima de nenhum dos slots. O Inventory usa para 'trancar' o que está sendo segurado.
            if(GearSlots[i].GetComponent<GearSlot>().IsHovering)
            {
                IsHoveringAny=true;
            }
        }

        if(IsFull)
        {
            DialogueText.text = "YAY, PARABÉNS, TASK CONCLUÍDA!";
            Debug.Log("All Gears in");
            for (int i=0 ; i < GearSlots.Length ; i++)
            {
                if(i % 2 == 0)
                {
                    GearSlots[i].GetComponent<GearSlot>().ThisGear.transform.Rotate(0f,0f,-36f*Time.deltaTime);
                } else
                {
                    GearSlots[i].GetComponent<GearSlot>().ThisGear.transform.Rotate(0f,0f,36f*Time.deltaTime);
                }
                
            }
        }else
        {
            DialogueText.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        }
    }
}
