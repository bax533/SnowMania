using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkinActivation : MonoBehaviour
{
    public GameObject[] skiParts; // 0 - bottom; 1 - pattern1; 2 - pattern2;
    public GameObject[] helmetParts; // 0 - bottom; 1 - pattern;

    public Material[] skinMaterials;


    void resetSki()
    {
        for (int i = 0; i < skiParts.Length; i++)
            skiParts[i].SetActive(false);
    }

    void SetSkiSkin()
    {
        resetSki();
        if (PlayerPrefs.HasKey("currentSki"))
        {
            string skinname = PlayerPrefs.GetString("currentSki");
            skiParts[0].SetActive(true);

            if (skinname[1] == 'd')
            {
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
                return;
            }

            if (skinname[1] == 'g')
            {
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
                return;
            }


            int partIndex = Int32.Parse(skinname[2].ToString());
            int bottomIndex = Values.Instance.getMaterialIndex(skinname).Item1;
            int topIndex = Values.Instance.getMaterialIndex(skinname).Item2;

            if (partIndex == 2)
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
        else
        {
            skiParts[0].SetActive(true);
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
        }
    }

    void resetHelmet()
    {
        for (int i = 0; i < helmetParts.Length; i++)
            helmetParts[i].SetActive(false);
    }

    public void SetHelmetSkin()
    {
        resetHelmet();
        string skinname = PlayerPrefs.GetString("currentHelmet");
        helmetParts[0].SetActive(true);

        if (skinname[1] == 'd')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
            return;
        }

        if (skinname[1] == 'g')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
            return;
        }

        int partIndex = skinname[1] == 'p' ? 1 : 0;
        int bottomIndex = Values.Instance.getMaterialIndex(skinname).Item1;
        int topIndex = Values.Instance.getMaterialIndex(skinname).Item2;

        helmetParts[partIndex].SetActive(true);
        helmetParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
        helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
    }


    void ActivateSkins()
    {
        SetSkiSkin();
        SetHelmetSkin();
    }

    void Awake()
    {
        ActivateSkins();



    }
}
