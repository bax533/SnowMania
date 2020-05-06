using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using admob;

public class shopInfoHelper : MonoBehaviour
{
    public Image[] images = new Image[14];
    public GameObject infoDialog;
    public GameObject adButton;

    string currentSkinName;
    string interstiotialID = "ca-app-pub-1887604439907523/1402676156";

    public Text adButtonText;

    List<string> addValNames = new List<string>() { "sp1_1", "sp1_2", "sp2_1", "sp2_2", "sp2_3", "hf2", "hp1" };

    Dictionary<int, string> IndexToName = new Dictionary<int, string>
    {
        {0,"sdefault"},
        {1,"sp1_1"},
        {2,"sp1_2"},
        {3,"sp1_3"},
        {4,"sp2_1"},
        {5,"sp2_2"},
        {6,"sp2_3"},
        {7,"sgold"},
        {8,"hdefault"},
        {9,"hf1"},
        {10,"hf2"},
        {11,"hp1"},
        {12,"hp2"},
        {13,"hp3"},
        {14,"hgold"}
    };

    Dictionary<string, string> NameToInfo = new Dictionary<string, string>
    {
        {"sdefault", "default"},
        {"sp1_1", "Frontflips: "},
        {"sp1_2", "Backflips: "},
        {"sp1_3", "Finish level in woods to unlock"},
        {"sp2_1", "Frontflips off rail: "},
        {"sp2_2", "Backflips off rail: "},
        {"sp2_3", "360s: "},
        {"sgold", "Finish game to unlock"},
        {"hdefault", "default"},
        {"hf1", "Finish tutorial to unlock"},
        {"hf2", "Finish 3 levels to unlock"},
        {"hp1", "Do a double backflip to unlock"},
        {"hp2", "Finish level in woods to unlock"},
        {"hp3", "Watch ad to unlock"},
        {"hgold", "Finish game to unlock"}
    };

    void Awake()
    {
        AdProperties testProp = new AdProperties();
        
        Admob.Instance().initSDK(new AdProperties());
        Admob.Instance().loadInterstitial(interstiotialID);
        for (int i=0; i < images.Length; i++)
        {
            if (PlayerPrefs.GetInt(Values.achievmentsDict[IndexToName[i]].Item1) < Values.achievmentsDict[IndexToName[i]].Item2)
            {
                Color32 col = images[i].color;
                images[i].color = new Color32(col.r, col.g, col.b, 130);
            }
            else
            {
                Color32 col = images[i].color;
                images[i].color = new Color32(col.r, col.g, col.b, 255);
            }
        }
    }

    public void SetInfoDialog(string skinName, bool withAd)
    {
        currentSkinName = skinName;
        if (withAd)
        {
            if (skinName == "hp3")
            {
                adButtonText.text = "WATCH AD";
            }
            else
            {
                adButtonText.text = "WATCH AD +25";
            }
            adButton.SetActive(true);
        }
        else
            adButton.SetActive(false);


        int curVal = PlayerPrefs.GetInt(Values.achievmentsDict[skinName].Item1);
        int goalVal = Values.achievmentsDict[skinName].Item2;

        GetComponentInChildren<Text>().text = NameToInfo[skinName] + " " + (addValNames.Contains(skinName) ? Math.Min(curVal, goalVal).ToString() + "/" + goalVal.ToString() : "");
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
    }


    IEnumerator tryLoadingAd()
    {
        Admob.Instance().loadInterstitial(interstiotialID);
        yield return new WaitForSeconds(1);
        if (Admob.Instance().isInterstitialReady())
        {
            //Debug.Log("AD READY");
            Admob.Instance().showInterstitial();

            string prefsKey = Values.achievmentsDict[currentSkinName].Item1;
            int curValue = PlayerPrefs.GetInt(prefsKey);
            PlayerPrefs.SetInt(prefsKey, curValue + 25);
        }
        else
            adButtonText.text = "TRY LATER";
    }

    public void ShowVideo()
    {
        StartCoroutine(tryLoadingAd());
    }

    public void CloseInfoDialog()
    {
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 1500f, 0f);
    }
}
