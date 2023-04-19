using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTree : MonoBehaviour
{
    
    public GameManager gameManager;
    int maxlength;
    public int SlashRate = 10;
    float counter;
    public AudioSource breakAudio;

    // Update is called once per frame
    private void Start()
    {
        counter = SlashRate;
        breakAudio.loop = false;
    }
    void Update()
    {
        if (Time.time > counter && gameManager.ActiveBases.Count!=0)
        {
            counter = Time.time + SlashRate;
            Slashingg();
        }

    }

    private void Slashingg()
    {
        maxlength = gameManager.ActiveBases.Count;
        int k = Random.Range(0, maxlength);
        print(k);
        gameManager.ActiveBases[k].DestroyPlantation();
        gameManager.ActiveBases[k].GetComponent<Animator>().SetTrigger("TreeBreak");
        gameManager.ActiveBases.Remove(gameManager.ActiveBases[k]);
        breakAudio.Play();
    }
}
