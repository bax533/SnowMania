using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class score_Script : MonoBehaviour {


    public TextMeshProUGUI levelScore_Text;
    public TextMeshProUGUI levelScoreEnd_Text, levelHighscoreEnd_Text;
    public Text currentTrickName_Text, currentTrickScore_Text, currentSpin_Text, currentFlip_Text, grindFlips_Text, achievment_Text;
    public GameObject grindFlipsArrow, achievmentPanel;
    public static GameObject newHighscoreLabel;

    public static int levelScore;
    public static float multiplier = 1f;
    int currentLevelScore, _flips = 0, _grindFlips = 0;
    int currentTrickScore, currentSpinScore, currentFlipScore;
    bool already_trick, isGrinding;
    bool alreadyFlip = false, alreadyOnGround = false;


    int sceneNr;
    public Animator anim, scoresAnim;

    void Awake()
    {
        sceneNr = Values.GetNrFromScene(SceneManager.GetActiveScene().name);
        currentFlip_Text.text = "";
        currentTrickName_Text.text = "";
        currentSpin_Text.text = "";
        currentFlip_Text.text = "";
        currentSpinScore = 0;
        currentFlipScore = 0;
        currentTrickScore = 0;
        currentLevelScore = 0;
    }

	// Update is called once per frame
	void Update () {
        
        if (Values.Instance.state == Values.State.Air || Values.Instance.state == Values.State.Grind)
        {
            alreadyOnGround = false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("truckDriverHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("truckDriverHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["truckDriver"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Truck Driver";
                }
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("bluntGrabHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("bluntGrabHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["bluntGrab"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Blunt Grab";
                }
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("muteGrabHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("muteGrabHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["muteGrab"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Mute Grab";
                }
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("tailGrabHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("tailGrabHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["tailGrab"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Tail Grab";
                }
                already_trick = true;
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("shiftyHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("shiftyHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["shifty"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Shifty";
                }
                already_trick = true;
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("bowArrowHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("bowArrowHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["bowArrow"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Bow Arrow";
                }
                already_trick = true;
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("japanGrabHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("japanGrabHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["japanGrab"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Japan Grab";
                }
                already_trick = true;
                already_trick = true;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("crossAirHold") || anim.GetCurrentAnimatorStateInfo(0).IsName("crossAirHold_g"))
            {
                currentTrickScore += already_trick ? Values.trickAdd_score : Values.trick_scores["crossAir"];
                if (!already_trick)
                {
                    if (currentTrickName_Text.text.Length > 0)
                        currentTrickName_Text.text += " -> ";
                    currentTrickName_Text.text += "Cross Air";
                }
                already_trick = true;
                already_trick = true;
            }
            else
                already_trick = false;


            currentSpinScore += anim.GetInteger("180s") * 5 * (isGrinding ? 2 : 1);

            currentTrickScore_Text.text = currentTrickScore > 0 ? currentTrickScore.ToString() : "";
            currentSpin_Text.text = anim.GetInteger("180s") > 0 ? (180 * anim.GetInteger("180s")).ToString() : "";

            if (anim.GetInteger("Flips") > 0)
            {
                if (anim.GetBool("wasGrinding"))
                    _grindFlips += 1;
                else
                    _flips += 1;

                anim.SetInteger("Flips", anim.GetInteger("Flips")-1);

                if (anim.GetFloat("triggers") > 0 && !anim.GetBool("alreadyFlip"))
                {
                    if(!anim.GetBool("wasGrinding"))
                    {
                        if (!anim.GetBool("backwards"))
                        {
                            currentFlip_Text.text = "Backflip";
                            if(_flips > 1)
                                currentFlip_Text.text = _flips.ToString()+'x' + ' ' + currentFlip_Text.text;
                        }
                        else
                        {
                            currentFlip_Text.text = "Frontflip";
                            if (_flips > 1)
                                currentFlip_Text.text = _flips.ToString() + 'x' + ' ' + currentFlip_Text.text;
                        }
                    }
                    else
                    {
                        grindFlipsArrow.SetActive(true);
                        if (!anim.GetBool("backwards"))
                        {
                            grindFlips_Text.text = "Backflip";
                            if (_grindFlips > 1)
                                grindFlips_Text.text = _grindFlips.ToString() + 'x' + ' ' + grindFlips_Text.text;
                        }
                        else
                        {
                            grindFlips_Text.text = "Frontflip";
                            if (_grindFlips > 1)
                                grindFlips_Text.text = _grindFlips.ToString() + 'x' + ' ' + grindFlips_Text.text;
                        }
                    }

                    anim.SetBool("alreadyFlip", true);
                }
                else if (anim.GetFloat("triggers") < 0 && !anim.GetBool("alreadyFlip"))
                {
                    if (!anim.GetBool("wasGrinding"))
                    {
                        if (!anim.GetBool("backwards"))
                        {
                            currentFlip_Text.text = "Frontflip";
                            if (_flips > 1)
                                currentFlip_Text.text = _flips.ToString() + 'x' + ' ' + currentFlip_Text.text;
                        }
                        else
                        {
                            currentFlip_Text.text = "Backflip";
                            if (_flips > 1)
                                currentFlip_Text.text = _flips.ToString() + 'x' + ' ' + currentFlip_Text.text;
                        }
                    }
                    else
                    {
                        grindFlipsArrow.SetActive(true);
                        if (!anim.GetBool("backwards"))
                        {
                            grindFlips_Text.text = "Frontflip";
                            if (_grindFlips > 1)
                                grindFlips_Text.text = _grindFlips.ToString() + 'x' + ' ' + grindFlips_Text.text;
                        }
                        else
                        {
                            grindFlips_Text.text = "Backflip";
                            if (_grindFlips > 1)
                                grindFlips_Text.text = _grindFlips.ToString() + 'x' + ' ' + grindFlips_Text.text;
                        }
                    }

                    anim.SetBool("alreadyFlip", true);
                }

            }
            //else
            //    currentFlip_Text.text = "";

            multiplier = 1 + 0.3f * (_flips + _grindFlips) + 0.2f * anim.GetInteger("180s");
            currentTrickScore_Text.text = currentTrickScore > 0 ? currentTrickScore.ToString() : "";
            currentTrickScore_Text.text  += multiplier > 1 && currentTrickScore > 0 ? " x " + multiplier.ToString() : "";
            currentFlipScore += anim.GetInteger("Flips") * 20;
        }
        else
        {
            if (!alreadyOnGround)
            {
                if (currentSpin_Text.text.Contains("360") && currentTrickName_Text.text.Length > 5) //tutorial stuff
                    PlayerPrefs.SetInt("360withGrab", 27);

                scoresAnim.Play("scorePop");
                alreadyOnGround = true;
            }
            else
                resetCurrentScore();
        }

        levelScoreEnd_Text.text = levelScore.ToString();
        levelHighscoreEnd_Text.text = Math.Max(levelScore, PlayerPrefs.GetInt("LEVEL"+sceneNr.ToString())).ToString();

        if(Values.Instance.END)
        {
            _flips = 0;
            _grindFlips = 0;
            currentTrickScore = 0;
            currentSpinScore = 0;
            currentFlipScore = 0;
            levelScore_Text.text = "Score: " + levelScore.ToString();
            currentTrickName_Text.text = "";
            currentTrickScore_Text.text = "";
            currentSpin_Text.text = "";
            currentFlip_Text.text = "";
            anim.SetBool("alreadyFlip", false);
        }
	}

    public void resetCurrentScore()
    {
        if (scoresAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            addToAchievments();

            _flips = 0;
            _grindFlips = 0;
            levelScore += (int)Math.Ceiling(currentTrickScore * multiplier);
            currentLevelScore = levelScore;
            currentTrickScore = 0;
            multiplier = 1f;
            levelScore_Text.text = "Score: " + levelScore.ToString();
            currentTrickName_Text.text = "";
            currentTrickScore_Text.text = "";
            currentSpin_Text.text = "";
            currentFlip_Text.text = "";
            grindFlips_Text.text = "";

            grindFlipsArrow.SetActive(false);

            anim.SetInteger("Flips", 0);
            anim.SetInteger("180s", 0);
            anim.SetBool("alreadyFlip", false);
        }
    }


    void addToAchievments()
    {
        //(_grindFlips);
        //(grindFlips_Text.text.Contains("Backflip"));

        

        if (currentFlip_Text.text.Contains("Backflip"))
        {
            PlayerPrefs.SetInt("Backflips", PlayerPrefs.GetInt("Backflips") + _flips);
            if (PlayerPrefs.GetInt("Backflips") == Values.achievmentsDict["sp1_2"].Item2)
            {
                achievment_Text.text = "New ski unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
        }

        if (currentFlip_Text.text.Contains("Frontflip"))
        {
            PlayerPrefs.SetInt("Frontflips", PlayerPrefs.GetInt("Frontflips") + _flips);
            if (PlayerPrefs.GetInt("Frontflips") == Values.achievmentsDict["sp1_1"].Item2)
            {
                achievment_Text.text = "New ski unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
        }

        if (grindFlips_Text.text.Contains("Backflip"))
        {
            PlayerPrefs.SetInt("Backflips_off_rail", PlayerPrefs.GetInt("Backflips_off_rail") + _grindFlips);
            if (PlayerPrefs.GetInt("Backflips_off_rail") == Values.achievmentsDict["sp2_2"].Item2)
            {
                achievment_Text.text = "New ski unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
        }
        if (grindFlips_Text.text.Contains("Frontflip"))
        {
            PlayerPrefs.SetInt("Frontflips_off_rail", PlayerPrefs.GetInt("Frontflips_off_rail") + _grindFlips);
            if (PlayerPrefs.GetInt("Frontflips_off_rail") == Values.achievmentsDict["sp2_1"].Item2)
            {
                achievment_Text.text = "New ski unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
        }
        if (currentSpin_Text.text.Contains("360"))
        {
            PlayerPrefs.SetInt("360s", PlayerPrefs.GetInt("360s") + 1);
            if (PlayerPrefs.GetInt("360s") == Values.achievmentsDict["sp2_3"].Item2)
            {
                achievment_Text.text = "New ski unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
        }

        if (currentFlip_Text.text.Contains("Backflip") && (_flips == 2 || _grindFlips == 2))
        {
            if (PlayerPrefs.GetInt("DoubleBackflip") == 0)
            {
                achievment_Text.text = "New helmet unlocked!";
                achievmentPanel.GetComponent<Animator>().Play("achievmentAnim");
            }
            PlayerPrefs.SetInt("DoubleBackflip", 1);
        }
    }

    public static void EndLVL()
    {

        int sceneNr = Values.GetNrFromScene(SceneManager.GetActiveScene().name);
        Debug.Log(PlayerPrefs.GetInt("LEVEL" + sceneNr.ToString()));
        if (!PlayerPrefs.HasKey("LEVEL" + sceneNr.ToString()) || levelScore > PlayerPrefs.GetInt("LEVEL" + sceneNr.ToString()))
        {
            PlayerPrefs.SetInt("Levels", Math.Max(PlayerPrefs.GetInt("Levels"), sceneNr));
            PlayerPrefs.SetInt("LEVEL" + sceneNr.ToString(), levelScore);
        }
    }
}
