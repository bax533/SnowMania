using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Values : MonoBehaviour {

    public static int levelsCount = 5;
    public static float maxGround_Speed = 20f, addGround_Speed = 0.08f, minGround_Speed = 0.0f, brakeGround_Speed = 0.05f, airSpeed = 9f, flipRotation = 325f, addSpinSpeed = 0.25f, maxSpinSpeed = 13.5f, brakeFinish_Mult = 0.98f;

    public enum State { Ground, Air, Finish, Grind };
    public static State state = State.Ground;
    public static bool END = false;
    public static bool PAUSE = false;

    public static List<string> adSkins = new List<string>()
    {
        "hp3",
        "sp1_1",
        "sp1_2",
        "sp2_1",
        "sp2_2",
        "sp2_3"
    };

    public static int[] highscores = new int[10];

    public static string currentSki = "default", currentHelmet = "default";


    public static int trickAdd_score = 25, spin_score = 100;
    public static Dictionary<string, int> trick_scores = new Dictionary<string, int>
    {
        { "truckDriver", 1100 },
        { "bluntGrab" , 1100},
        { "muteGrab", 1100 },
        { "tailGrab", 1100 },
        { "shifty", 700 },
        { "japanGrab", 1100 },
        { "bowArrow", 1100 },
        { "crossAir", 1100 },
        { "flip", 500 }
    };


    public static Dictionary<string, Tuple<string, int>> achievmentsDict= new Dictionary<string, Tuple<string, int>> //skinname -> achievment_name, achievment_val
    {
        { "sdefault", new Tuple<string, int>("default", 1) },
        { "sp1_1", new Tuple<string, int>("Frontflips", 100) },
        { "sp1_2", new Tuple<string, int>("Backflips", 100) },
        { "sp1_3", new Tuple<string, int>("Levels", 5) },
        { "sp2_1", new Tuple<string, int>("Frontflips_off_rail", 50) },
        { "sp2_2", new Tuple<string, int>("Backflips_off_rail", 50) },
        { "sp2_3", new Tuple<string, int>("360s", 75) },
        { "sgold", new Tuple<string, int>("Levels", 9) },
        { "hdefault", new Tuple<string, int>("default", 1) },
        { "hf1", new Tuple<string, int>("Tutorial", 1) },
        { "hf2", new Tuple<string, int>("Levels", 3) },
        { "hp1", new Tuple<string, int>("DoubleBackflip", 1) },
        { "hp2", new Tuple<string, int>("Levels", 5) },
        { "hp3", new Tuple<string, int>("ad", 1) },
        { "hgold", new Tuple<string, int>("Levels", 9) },

    };

    public static int GetNrFromScene(string sceneName)
    {
        string nr = "";
        for(int i=5; i<sceneName.Length; i++)
        {
            nr += sceneName[i];
        }
        return int.Parse(nr);
    }

    public static Tuple<int, int> getMaterialIndex(string skinName)
    {
        switch(skinName)
        {
            case "sdefault":
            case "hdefault":
                return Tuple.Create(0, 0);
            case "sgold":
            case "hgold":
                return Tuple.Create(1, 1);

            case "hf1":
                return Tuple.Create(2, 2);

            case "hf2":
                return Tuple.Create(3, 3);

            case "hp1":
                return Tuple.Create(4, 5);

            case "hp2":
                return Tuple.Create(6, 7);

            case "hp3":
                return Tuple.Create(8, 9);

            case "sp1_1":
                return Tuple.Create(10, 11);

            case "sp1_2":
                return Tuple.Create(12, 13);

            case "sp1_3":
                return Tuple.Create(14, 15);

            case "sp2_1":
                return Tuple.Create(16, 17);

            case "sp2_2":
                return Tuple.Create(18, 19);

            case "sp2_3":
                return Tuple.Create(20, 21);

            default:
                return Tuple.Create(0, 0);
        }
    }

    public static Dictionary<string, string> tutorialMessages = new Dictionary<string, string>
    {
        { "1" , "press ride to speed up!" },
        { "2", "rotate your phone to do flips!" },
        { "3" , "hold spin when in air to spin!"},
        { "4" , "the longer you hold, the more you will spin!" },
        { "5" , "try to do a 360!"},
        { "6" , "Now you can see a joystick in the bottom left corner" },
        { "7" , "move it in the air to do different tricks!" },
        { "8" , "now try doing a 360 with some grab!" },
        { "9" , "hold joystick down when approaching a rail to grind!" },
        { "10", "you can spin while grinding! Try to spin on next rail" },
        { "11", "before every rail there will be a warning sign so you can be prepared!"},
        { "good" , "well done!" },
        { "bad" , "looks like you didn't get it :( you can restart the tutorial!" },

    };
}
