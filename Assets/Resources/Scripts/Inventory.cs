using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// O bulk do código está aqui. Estou usando esse Script como uma espécie de administrador para todo o resto.


//Classe dos itens no inventário.
[System.Serializable]
public class Gear
{
    public Color Color;
}

public class Inventory : MonoBehaviour
{
    public Gear[] GearList;
    public GameObject[] Slots;

    public GameObject Canvas;

    public GameObject GearObject;

    public GameObject[] GearUIList;

    public GearSlotsManager GearSlotsManager;

    [Header("Object Held on Mouse")]

    //A gear. Serve para colocar a Gear no array do inventário.
    public Gear HeldGear;

    //O Objeto que está sendo segurado. Serve para o script saber o que deve ser destruído quando a gear for colocada em algum lugar.
    public GameObject HeldGearObj;

    //Slot do inventário;
    public int HeldObj;

    //Slot do inventário de novo, para a gambiarra.
    public int LastHeldObj;

    public bool IsOverASlot;

    //Isso aqui cria os slots automaticamente.
    [Header("Automatic Startup - ENSURE THE SIZE HERE IS THE SAME SIZE AS THE GEAR LIST")]

    public bool AutoStart;
    public int Size;

    public GameObject ItemSlotObj;
    public float PosX;
    public float PosY;
    public float Spacing;

    public enum Rotations
    {
        Vertical,
        Horizontal
    };
    public Rotations Rotation;



    void Start()
    {
        //Startup do inventário. Tudo baseado no array e posições. Tem como fazer o startup por meio do editor da Unity com elementos de UI, mas teria que adicionar um código a mais pra, ao invés
        //de escrever os objetos em cada slot, ler eles. Exemplo: Colocar um script no objeto com as informações dele, aí detectar se tem algum objeto em cima de cada slot e puxar a informação de cada script.
        // Estou fazendo toda a inicialização por meio de código pois tem poucos slots e objetos disponíveis, mas para uma interface mais complexa, como em um jogo de administração, talvez seja melhor trabalhar
        //por meio do editor da Unity.

        //Cria os slots automaticamente se o AutoStart estiver marcado no inspector.
        if(AutoStart)
        {
            Slots = new GameObject[Size];
            for ( int i=0 ; i < Size ; i++ )
            {
                GameObject NewSlot = Instantiate( ItemSlotObj , transform.position , Quaternion.identity );
                RectTransform NewSlotTransform = NewSlot.GetComponent<RectTransform>();
                NewSlot.transform.SetParent(Canvas.transform, false);

                if (Rotation == Rotations.Horizontal)
                {
                    NewSlotTransform.anchoredPosition = new Vector2( PosX + ( i * (Spacing + NewSlotTransform.sizeDelta.x/2f) ) , PosY );
                } 
                else
                {
                    NewSlotTransform.anchoredPosition = new Vector2( PosX , PosY + ( i * (Spacing + NewSlotTransform.sizeDelta.y/2f)));
                }
                Slots[i] = NewSlot;
                NewSlot.GetComponent<InventorySlot>().ThisInventoryPosition=i;
            }
        }

        //Cria as gears na canvas e coloca elas no array.
        GearUIList = new GameObject[GearList.Length];
        for( int i = 0 ; i<GearList.Length ; i++)
        {
            GameObject NewGear = Instantiate(GearObject , transform.position , Quaternion.identity);
            NewGear.transform.SetParent(Canvas.transform,false);
            NewGear.GetComponent<RectTransform>().anchoredPosition = Slots[i].GetComponent<RectTransform>().anchoredPosition;
            NewGear.GetComponent<Image>().color = GearList[i].Color;
            NewGear.GetComponent<GearUI>().InventorySlot = i;
            GearUIList[i] = NewGear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Essa é a gambiarra. Pra ter certeza de que ele não vai aceitar um -1 no slot, primeiro ele verifica se o mouse não está em cima de um dos slots.
        IsOverASlot = GearSlotsManager.IsHoveringAny;
        if(HeldObj == -1  & !IsOverASlot)
        {
            LastHeldObj=-1;
        }

        //Esc fecha o programa para conveniência.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
