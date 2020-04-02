using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour
{

    public Animator ski_anim, helmets_anim;
    string current = "";

    public GameObject[] skiParts; // 0 - bottom; 1 - pattern1; 2 - pattern2;
    public GameObject[] helmetParts; // 0 - bottom; 1 - pattern;

    public Material[] skinMaterials;

    void resetSki()
    {
        for (int i = 0; i < skiParts.Length; i++)
            skiParts[i].SetActive(false);
    }

    public void SetSki_Click(string name)
    {
        PlayerPrefs.SetString("currentSki", name);

        resetSki();

        skiParts[0].SetActive(true);
        if (name[1] == 'd')
        {
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
            return;
        }

        if (name[1] == 'g')
        {
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
            return;
        }


        int partIndex = Int32.Parse(name[2].ToString());
        int bottomIndex = Values.Instance.getMaterialIndex(name).Item1;
        int topIndex = Values.Instance.getMaterialIndex(name).Item2;

        if (name[2] == '2')
        {
            skiParts[partIndex].SetActive(true);
            skiParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
        }
        else
        {
            skiParts[partIndex].SetActive(true);
            skiParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
        }
    }

    void resetHelmet()
    {
        for (int i = 0; i < helmetParts.Length; i++)
            helmetParts[i].SetActive(false);
    }

    public void SetHelmet_Click(string name)
    {
        PlayerPrefs.SetString("currentHelmet", name);

        resetHelmet();

        helmetParts[0].SetActive(true);
        if (name[1] == 'd')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
            return;
        }

        if (name[1] == 'g')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
            return;
        }

        int partIndex = name[1] == 'p' ? 1 : 0;
        int bottomIndex = Values.Instance.getMaterialIndex(name).Item1;
        int topIndex = Values.Instance.getMaterialIndex(name).Item2;

        helmetParts[partIndex].SetActive(true);
        helmetParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
        helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
    }


    public void Ski_Click()
    {
        if (current != "skis")
        {
            ski_anim.SetTrigger("in");
            if(current != "")
                helmets_anim.SetTrigger("out");
        }
        current = "skis";
    }

    public void Helmets_Click()
    {
        if (current != "helmets")
        {
            if(current!="")
                ski_anim.SetTrigger("out");
            helmets_anim.SetTrigger("in");
        }
        current = "helmets";
    }

    void Awake()
    {
        if (PlayerPrefs.HasKey("currentSki"))
            SetSki_Click(PlayerPrefs.GetString("currentSki"));
        else
            SetSki_Click("sdefault");

        if (PlayerPrefs.HasKey("currentHelmet"))
            SetHelmet_Click(PlayerPrefs.GetString("currentHelmet"));
        else
            SetHelmet_Click("hdefault");
    }

}
