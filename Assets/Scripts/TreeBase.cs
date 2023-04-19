using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreeBase : MonoBehaviour,IPointerDownHandler
{
    public Tree tree;
    public int GrowthState;
    public float Age;
    public Image BaseSprite;
    GameManager gameManager;
    public bool Selected = false;
    public bool Planted = false;
    public Image TreeSprite;

    //Test Variable
    private int tej;

    public float midAge;
    public float lastage;


    public int seedsGenerated;


    float Timecounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        TreeSprite.sprite = tree.TreeSprite[3];
        midAge = tree.midAge;
        lastage = tree.LastAge;
        seedsGenerated = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.ResetTreeBases();
        Selected = true;
        gameManager.UpdateTreeBases();
        gameManager.UpdateMenu(gameObject.GetComponent<TreeBase>());

    }

    //public void ChangeColor(bool selected)
    //{
    //    if (selected)
    //        BaseSprite.color = Color.yellow;
    //    else
    //        BaseSprite.color = Color.white;

    //}

    public void NewPlantation()
    {
        GrowthState = 0;
        Age = 0;
        Planted = true;
        //SpriteChanges
        TreeSprite.sprite = tree.TreeSprite[0];
        seedsGenerated = 0;
        midAge = tree.midAge;
        lastage = tree.LastAge;
        Timecounter = tree.SeedDropRate;
        
    }

    public void DestroyPlantation()
    {
        GrowthState = 0;
        Age = 0;
        Planted = false;
        TreeSprite.sprite = tree.TreeSprite[3];
        seedsGenerated = 0;
        Timecounter = 0f;
    }

    
    public void SeedGeneration()
    {
        

        if (GrowthState!=2)
        {
            return;
        }
        if (Time.time > Timecounter)
        {
            Timecounter = Time.time + tree.SeedDropRate;
            seedsGenerated += (int)tree.SeedQuantity;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Planted)
        {
            Age += Time.deltaTime * 0.5f;
        }
        if(Selected)
        {
            gameManager.UpdateMenu(gameObject.GetComponent<TreeBase>());
        }
        if(Age>midAge && Age<lastage && TreeSprite != tree.TreeSprite[1])
        {
            TreeSprite.sprite = tree.TreeSprite[1];
            GrowthState = 1;
        }
        if(Age > lastage && TreeSprite != tree.TreeSprite[2])
        {
            TreeSprite.sprite = tree.TreeSprite[2];
            GrowthState = 2;

        }

        SeedGeneration();


        
    }
}
