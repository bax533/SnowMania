using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Values : MonoBehaviour {

    public int levelsCount = 5;

    public float maxGround_Speed = 25f, addGround_Speed = 1.0f, minGround_Speed, brakeGround_Speed, airSpeed, flipRotation, addSpinSpeed, maxSpinSpeed, brakeFinish_Mult;

    public enum State { Ground, Air, Finish, Grind };
    public State state = State.Ground;
    public bool END = false;
    public bool PAUSE = false;

    public int current_lvl = 0;

    public int[] highscores = new int[10];

    public string currentSki = "default", currentHelmet = "default";


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
        { "crossAir", 1100 }

    };


    public static Dictionary<string, Tuple<string, int>> achievmentsDict= new Dictionary<string, Tuple<string, int>> //skinname -> achievment_name, achievment_val
    {
        { "sdefault", new Tuple<string, int>("default", 1) },
        { "sp1_1", new Tuple<string, int>("Frontflips", 100) },
        { "sp1_2", new Tuple<string, int>("Backflips", 100) },
        { "sp1_3", new Tuple<string, int>("Levels", 11) },
        { "sp2_1", new Tuple<string, int>("Frontflips_off_rail", 50) },
        { "sp2_2", new Tuple<string, int>("Backflips_off_rail", 50) },
        { "sp2_3", new Tuple<string, int>("360s", 75) },
        { "sgold", new Tuple<string, int>("Levels", 20) },
        { "hdefault", new Tuple<string, int>("default", 1) },
        { "hf1", new Tuple<string, int>("Tutorial", 1) },
        { "hf2", new Tuple<string, int>("Levels", 3) },
        { "hp1", new Tuple<string, int>("DoubleBackflip", 1) },
        { "hp2", new Tuple<string, int>("Levels", 11) },
        { "hp3", new Tuple<string, int>("ad", 1) },
        { "hgold", new Tuple<string, int>("Levels", 20) },

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

    public Tuple<int, int> getMaterialIndex(string skinName)
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

    private static Values m_oInstance = null;

    public static Values Instance
    {
        get
        {
            if (m_oInstance == null)
            {
                m_oInstance = new Values();
            }
            return m_oInstance;
        }
    }

    private Values()
    {

    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (m_oInstance != null && m_oInstance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        m_oInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
