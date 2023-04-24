using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.Win32;
using UnityEngine.SceneManagement;
using static System.Collections.Specialized.BitVector32;
using System;


public class Tutorial_TextTemplateManager : MonoBehaviour
{
    public int page_counter;

    public TextMeshProUGUI page_content;
    public TextMeshProUGUI page_number;
    public GameObject[] TextSlides;
    public GameObject TextOnly;
    //public GameObject TextImg;
    //public GameObject TextVid;


    void Start()
    {
        //page_counter = User.progress
        page_counter = 0;
    }
    void Update()
    {
        if (page_counter <= 0) { page_counter = 1; }
        PageNumber();
        PageContent();
        //Slide();
    }


    //      Tutorial Button Scripts

    public void OnNext() { page_counter++; }
    public void OnPrevious() { page_counter--; }
    public void SaveExit() { /*User.progress = page_counter;*/ }


    //      Slide Tracking and Control

    void PageNumber() { page_number.text = "Page "+page_counter.ToString(); }
    void PageContent()
    {
        TextOnly.SetActive(true);
        page_content.text = slideText[page_counter - 1];
    }
    void Slide()
    {

        GameObject used = TextSlides[page_counter - 1];
        used.SetActive(true);

        foreach (GameObject notUsed in TextSlides)
        {
            if (notUsed != used) { notUsed.SetActive(false); }
        }
    }

    //      Slide Content

    string[] slideText =
    {
        //1
        "The most important thing we want you to leave with after this tutorial is all of your limbs! So please take this section seriously." +
        "\n\nThe MELD machine has numerous safety mechanisms built into itself. But there are steps you will take where you can harm yourself.",

        //2
        "Before we begin, make sure you take off all loose jewelry like long necklaces and clothing items such as unzipped jackets. " +
        "\n\nIf you have long hair, tie it back so it does not get in the way.",

        //3
        "Make sure the area around you is clear. There should be ample space to move in front of the MELD machine and the console. " +
        "With the headset, you are more prone to tripping, so check the floor as well. \n\nIf you have not done so yet, put on safety glasses. " +
        "There is a step where you use an air spray to clear the base plate and metal flakes will fly towards you. The HoloLens alone will not ensure protection from the flakes damaging your eyes.",

        //4
        "As said before, the MELD machine has many safety features. Let’s walk through them.\n\n" +
        "When the MELD machine is running, the doors must remain closed. If the doors open, the machine will stop (like a microwave), but it will mess up the print.\n\n" +
        "There is an emergency stop button at the stop left of the console. If something goes wrong, do not hesitate to press the button. Prints are cheap to restart, repairs and medical bills are not cheap.\n\n" +
        "The “remote jog handle” also has an emergency stop and a “deadman” switch. The deadman switch means you must hold the switch down in order to move the actuator during preparation. We will cover this in the tutorial.",

        //5
        "Lastly, and this is the most important, do not touch the printed material after you begin the print. The material is extremely hot and will burn you. " +
        "There are special gloves to do so, but we will not have you use them. Instead, you must wait for it to cool after printing.\n\n" +
        "That’s it! We will remind you of safety procedures to follow when you arrive at the step.",

        //6
        "{{ safety checklist will go here }}",

        //7
        "{{ END OF TEST }}\n\n" +
        "Operation portion of the tutorial will start here."

    };

}
