using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class score_Script : MonoBehaviour {


    public TextMeshProUGUI levelScore_Text;
    public TextMeshProUGUI levelScoreEnd_Text;
    public Text currentTrickName_Text, currentTrickScore_Text, currentSpin_Text, currentFlip_Text;

    public static int levelScore;
    public static float multiplier = 1f;
    int currentLevelScore;
    int currentTrickScore, currentSpinScore, currentFlipScore;
    bool already_trick, isGrinding;
    bool alreadyFlip = false, alreadyOnGround = false;

    public Animator anim, scoresAnim;

    void Start()
    {
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
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("truckDriverHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("bluntGrabHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("muteGrabHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("tailGrabHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("shiftyHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("bowArrowHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("japanGrabHold"))
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
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("crossAirHold"))
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
                if (anim.GetFloat("triggers") > 0 && !alreadyFlip)
                {
                    if (!anim.GetBool("backwards"))
                        currentFlip_Text.text = "Backflip";
                    else
                        currentFlip_Text.text = "Frontflip";

                    alreadyFlip = true;
                }
                else if (anim.GetFloat("triggers") < 0 && !alreadyFlip)
                {
                    if(anim.GetBool("backwards"))
                        currentFlip_Text.text = "Backflip";
                    else
                        currentFlip_Text.text = "Frontflip";

                    alreadyFlip = true;
                }

                if (anim.GetInteger("Flips") > 1)
                {
                    if(currentFlip_Text.text.Contains("x"))
                    {
                        int _flips = anim.GetInteger("Flips");
                        currentFlip_Text.text = _flips.ToString() + currentFlip_Text.text.Substring(1, currentFlip_Text.text.Length-1);
                    }
                    else
                        currentFlip_Text.text = anim.GetInteger("Flips").ToString() + "x " + currentFlip_Text.text;
                }
            }
            else
                currentFlip_Text.text = "";

            multiplier = 1 + 0.3f * anim.GetInteger("Flips") + 0.2f * anim.GetInteger("180s");
            currentTrickScore_Text.text = currentTrickScore > 0 ? currentTrickScore.ToString() : "";
            currentTrickScore_Text.text  += multiplier > 1 && currentTrickScore > 0 ? " x " + multiplier.ToString() : "";
            currentFlipScore += anim.GetInteger("Flips") * 20;
        }
        else
        {
            if (!alreadyOnGround)
            {
                scoresAnim.Play("scorePop");
                alreadyOnGround = true;
            }
            else
                resetCurrentScore();
        }

        levelScoreEnd_Text.text = levelScore.ToString();

        if(Values.Instance.END)
        {
            currentTrickScore = 0;
            currentSpinScore = 0;
            currentFlipScore = 0;
            levelScore_Text.text = "Score: " + levelScore.ToString();
            currentTrickName_Text.text = "";
            currentTrickScore_Text.text = "";
            currentSpin_Text.text = "";
            currentFlip_Text.text = "";
            alreadyFlip = false;
        }
	}
    

    void SetAchievements()
    {
        if(currentFlip_Text.text.Contains("Backflip"))
        {
            PlayerPrefs.SetInt("Backflips", PlayerPrefs.GetInt("Backflips") + anim.GetInteger("Flips"));
        }
        else if (currentFlip_Text.text.Contains("Frontflip"))
        {
            PlayerPrefs.SetInt("Frontflips", PlayerPrefs.GetInt("Frontflips") + anim.GetInteger("Flips"));
        }
    }

    public void resetCurrentScore()
    {
        
        if (scoresAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            levelScore += (int)Math.Ceiling(currentTrickScore * multiplier);
            currentLevelScore = levelScore;
            currentTrickScore = 0;
            multiplier = 1f;
            levelScore_Text.text = "Score: " + levelScore.ToString();
            currentTrickName_Text.text = "";
            currentTrickScore_Text.text = "";
            currentSpin_Text.text = "";
            currentFlip_Text.text = "";
            anim.SetInteger("Flips", 0);
            anim.SetInteger("180s", 0);
            alreadyFlip = false;
        }
    }



    public static void EndLVL()
    {
        if(!PlayerPrefs.HasKey("LEVEL"+Values.Instance.current_lvl.ToString()) || levelScore > PlayerPrefs.GetInt("LEVEL"+Values.Instance.current_lvl.ToString()))
        {
            PlayerPrefs.SetInt("LEVEL" + Values.Instance.current_lvl.ToString(), levelScore);
        }
    }
}
