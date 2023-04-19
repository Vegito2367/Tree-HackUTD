using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TreeBase[] treeBases;
    public List<TreeBase> ActiveBases;

    public TextMeshProUGUI TypeBox;
    public TextMeshProUGUI AgeBox;
    public TextMeshProUGUI CostBox;
    public Tree TreeGodReference1;
    public Tree TreeGodReference2;
    public Tree TreeGodReference3;

    public string TreeName1;
    public string TreeName2;
    public string TreeName3;
    public GameObject infoMenu;
    public GameObject plantMenu;
    public TextMeshProUGUI Seed1Display;
    public TextMeshProUGUI Seed2Display;
    public TextMeshProUGUI Seed3Display;

    public TextMeshProUGUI timedisplay;

    public GameObject holder;

    public TMP_Dropdown treesDropdown;
    public Button PlantButton;
    public Button CollectSeeds;

    public float TotalTime = 180f;
    int displayTime;

    public Gradient gradient;
    public Image Fill1;
    public Image Fill2;
    public Image Fill3;
    public Slider SliderSeed1;
    public Slider SliderSeed2;
    public Slider SliderSeed3;
    public int Seeds1;
    public int Seeds2;
    public int Seeds3;


    public GameObject Endscreen;
    public GameObject winstuff;
    public GameObject losestuff;

    public GameObject PauseMenu;

    public AudioSource PlantSound;
    

    public bool Gamedead = false;
    bool functionrun = false;

    float[] counttrees= new float[3];

    void Start()
    {
        Time.timeScale = 1f;
        treeBases = GameObject.FindObjectsOfType<TreeBase>();
        DisplayInfoMenu();
        Seed1Display.text = " " + Seeds1;
        Seed2Display.text = " " + Seeds2;
        Seed3Display.text = " " + Seeds3;
    }

    private void Update()
    {
        if (Gamedead)
            return;
        Timer();
        if (ActiveBases.Count == 0 && TotalTime < 140)
        {
            if (!functionrun)
            {
                KillGame();

            }
        }
        if(ActiveBases.Count!=0)
        {
            SliderValue();
        }
        

    }

    private void SliderValue()
    {
        for (int i = 0; i < counttrees.Length; i++)
        {
            counttrees[i] = 0f;
        }
        foreach (TreeBase item in ActiveBases)
        {
            if (item.tree.type == TreeType.Tree1)
            {
                counttrees[0]++;
            }
            if (item.tree.type == TreeType.Tree2)
            {
                counttrees[1]++;
            }
            if (item.tree.type == TreeType.Tree3)
            {
                counttrees[2]++;
            }

        }

        
        SliderSeed1.value =  (counttrees[0] / ActiveBases.Count);
        SliderSeed2.value = (counttrees[1] / ActiveBases.Count);
        SliderSeed3.value = (counttrees[2] / ActiveBases.Count);

        Fill1.color = gradient.Evaluate(SliderSeed1.normalizedValue);
        Fill2.color = gradient.Evaluate(SliderSeed2.normalizedValue);
        Fill3.color = gradient.Evaluate(SliderSeed3.normalizedValue);



    }
    
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    

    private void Timer()
    {
        if (TotalTime > 0 && !Gamedead)
        {
            TotalTime -= Time.deltaTime;
            displayTime = (int)TotalTime;
            timedisplay.text = (displayTime / 60) + ":" + (displayTime % 60);
        }
        else
        {

            if (!functionrun)
            {
                KillGame();

            }
        }
    }

    public void KillGame()
    {
        print("Fuck your mom");
        Gamedead = true;
        functionrun = true;
        ResetTreeBases();
        foreach (TreeBase item in treeBases)
        {
            item.Planted = false;
        }
        bool f = SliderSeed1.value>0.25f;
        bool g = SliderSeed2.value>0.25f;
        bool h = SliderSeed3.value>0.25f;

        if((f&&g)||(g&&f)||(f&&h))
        {
            Endscreen.SetActive(true);
            losestuff.SetActive(false);
            winstuff.SetActive(true);
        }
        else
        {
            Endscreen.SetActive(true);
            losestuff.SetActive(true);
            winstuff.SetActive(false);
        }
        holder.SetActive(false);
    }

    public void ResetTreeBases()
    {
        foreach (TreeBase item in treeBases)
        {
            item.Selected = false;
            item.transform.GetComponent<Animator>().SetBool("Selected", false);
        }
        
    }
    public void UpdateTreeBases()
    {
        foreach (TreeBase item in treeBases)
        {
            if (item.Selected)
            {
                item.transform.GetComponent<Animator>().SetBool("Selected", true);
            }
            
        }
        
    }
    public void UpdateSeeds()
    {
        
        foreach (TreeBase item in treeBases)
        {
            if(item.Selected)
            {
                if (item.tree.type == TreeType.Tree1)
                {
                    Seeds1 += item.seedsGenerated;
                    Seed1Display.text = " " + Seeds1;
                }
                if (item.tree.type == TreeType.Tree2)
                {
                    Seeds2 += item.seedsGenerated;
                    Seed2Display.text = " " + Seeds2;
                }
                if (item.tree.type == TreeType.Tree3)
                {
                    Seeds3 += item.seedsGenerated;
                    Seed3Display.text = " " + Seeds3;
                }
            }
            
            item.seedsGenerated = 0;
        }
        TreeBase tr = FindPlantedBase();

    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    
    public void UpdateMenu(TreeBase Tr)
    {
        holder.SetActive(true);

        
        

        if (!Tr.Planted)
        {
            TypeBox.text = "Not Planted";
            AgeBox.text = "0";
            CostBox.text = "No seed considered";
            PlantButton.interactable = true;
            CollectSeeds.GetComponentInChildren<TextMeshProUGUI>().text = "Collect Seeds : 0";
        }
        else
        {
            TypeBox.text = "Tree Type : " + Tr.tree.type;
            AgeBox.text = "Age : " + (int)Tr.Age;
            CostBox.text = "Cost : " + Tr.tree.SeedCost;
            PlantButton.interactable = false;
            if (Tr.tree.type == TreeType.Tree1)
            {
                CollectSeeds.GetComponentInChildren<TextMeshProUGUI>().text = "Collect Seeds : " + Tr.seedsGenerated;
            }
            if (Tr.tree.type == TreeType.Tree2)
            {
                CollectSeeds.GetComponentInChildren<TextMeshProUGUI>().text = "Collect Seeds : " + Tr.seedsGenerated;
            }
            if (Tr.tree.type == TreeType.Tree3)
            {
                CollectSeeds.GetComponentInChildren<TextMeshProUGUI>().text = "Collect Seeds : " + Tr.seedsGenerated;
            }
            
        }

    }
    public TreeBase FindPlantedBase()
    {
        foreach (TreeBase item in treeBases)
        {
            if (item.Selected)
                return item;
        }
        return null;
    }
    public void PlantTree()
    {
        
        TreeBase tr = FindPlantedBase();
        if (tr.Planted)
            return;
        int k = tr.tree.SeedCost;
        string s = treesDropdown.captionText.text;
       
        if(s.Equals(TreeName1) && Seeds1 > k)
        {
            
            tr.tree = TreeGodReference1;
            Seeds1 -= tr.tree.SeedCost;
            tr.NewPlantation();
            UpdateMenu(tr);
            ActiveBases.Add(tr);

        }
        if (s.Equals(TreeName2) && Seeds2 > k)
        {
            tr.tree = TreeGodReference2;
            Seeds2 -= tr.tree.SeedCost;
            tr.NewPlantation();
            UpdateMenu(tr);
            ActiveBases.Add(tr);

        }
        if (s.Equals(TreeName3) && Seeds3 > k)
        {
            tr.tree = TreeGodReference3;
            Seeds3 -= tr.tree.SeedCost;
            tr.NewPlantation();
            UpdateMenu(tr);
            ActiveBases.Add(tr);

        }
        UpdateSeeds();
        PlantSound.Play();

        
    }

    public void DisplayPlantMenu()
    {
        infoMenu.SetActive(false);
        plantMenu.SetActive(true);
    }
    public void DisplayInfoMenu()
    {
        plantMenu.SetActive(false);
        infoMenu.SetActive(true);
    }






}
