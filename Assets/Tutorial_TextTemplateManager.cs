using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tutorial_TextTemplateManager : MonoBehaviour
{
    public int page_counter;
    public TextMeshProUGUI page_number;
    public GameObject[] TextSlides;
    // Start is called before the first frame update
    void Start()
    {
        //page_counter = User.page_counter
        page_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (page_counter <= 0) { page_counter = 1; }
        PageNumber();
        Slide();
    }

    public void OnNext() { page_counter++; }

    public void OnPrevious() { page_counter--; }

    void PageNumber() { page_number.text = "Page "+page_counter.ToString(); }

    void Slide()
    {
        
        GameObject used = TextSlides[page_counter-1];
        used.SetActive(true);

        foreach (GameObject notUsed in TextSlides)
        {
            if (notUsed != used) { notUsed.SetActive(false); }
        }
    }

}
