using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    // Use this for initialization
    Vector3 camMainPos; //= new Vector3(9.3f, 2.4f, 9.5f);
    //Vector3 camLevelsPos = new Vector3(9.3f, 12.0f, 9.5f);
    Vector3 camLevelsPos; //= new Vector3(9.3f, 0, 9.5f);
    //BASE CAM ROTATION (3.5, -150, 0)
    public Transform Base, Levels;
    public float lerpSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    private float startTime;
    private float journeyLength;

    bool mainMenu = true;

    public Button playButton;
    public Color32 lockedCol, unlockedCol;


    public Text[] highscoresText = new Text[10];
    public Camera mainCam;
    public GameObject mainPanel;
    public GameObject levelsPanel;

    public Animator transAnim;
    public Animator chairliftAnim;

    public void PlayerClick()
    {
        chairliftAnim.Play("Touched");
    }

    public void PlayClick()
    {
        if (PlayerPrefs.GetInt("Tutorial") != 0)
        {
            mainMenu = false;
            startTime = Time.time;
            mainPanel.SetActive(false);
            levelsPanel.SetActive(true);
        }
    }

    public void ShopClick()
    {
        StartCoroutine(LoadLevelRoutine(1));
    }

    public void MainClick()
    {
        mainMenu = true;
        startTime = Time.time;
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void LoadTutorial(int nr)
    {
        if (nr == 0)
            StartCoroutine(LoadLevelRoutine("TUTOR0"));
        else
            StartCoroutine(LoadLevelRoutine("TUTOR2"));
    }

    public void LoadLevel(int nr)
    {
        if (PlayerPrefs.HasKey("LEVEL" + (nr-1).ToString()) || nr == 1)
        {
            StartCoroutine(LoadLevelRoutine("LEVEL" + nr.ToString()));
        }
    }

    IEnumerator LoadLevelRoutine(int levelBuildIndex)
    {
        transAnim.SetTrigger("endlvl");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelBuildIndex);
    }

    IEnumerator LoadLevelRoutine(string sceneName)
    {
        transAnim.SetTrigger("endlvl");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }

    void Start () {


        //Input.gyro.enabled = true;

        if (!PlayerPrefs.HasKey("Backflips"))
            PlayerPrefs.SetInt("Backflips", 0);
        

        if (!PlayerPrefs.HasKey("Frontflips"))
            PlayerPrefs.SetInt("Frontflips", 0);

        if (!PlayerPrefs.HasKey("Frontflips_off_rail"))
            PlayerPrefs.SetInt("Frontflips_off_rail", 0);

        if (!PlayerPrefs.HasKey("Backflips_off_rail"))
            PlayerPrefs.SetInt("Backflips_off_rail", 0);

        if (!PlayerPrefs.HasKey("360s"))
            PlayerPrefs.SetInt("360s", 0);

        if (!PlayerPrefs.HasKey("Levels"))
            PlayerPrefs.SetInt("Levels", 0);

        if (!PlayerPrefs.HasKey("default"))
            PlayerPrefs.SetInt("default", 1);

        if (!PlayerPrefs.HasKey("Tutorial"))
            PlayerPrefs.SetInt("Tutorial", 0);

        if (!PlayerPrefs.HasKey("DoubleBackflip"))
            PlayerPrefs.SetInt("DoubleBackflip", 0);

        if (!PlayerPrefs.HasKey("ad"))
            PlayerPrefs.SetInt("ad", 0);

        if (!PlayerPrefs.HasKey("currentHelmet"))
            PlayerPrefs.SetString("currentHelmet", "hdefault");

        if (!PlayerPrefs.HasKey("currentSki"))
            PlayerPrefs.SetString("currentSki", "sdefault");

        camMainPos = Base.position;
        camLevelsPos = Levels.position;
        SceneManager.sceneLoaded += OnSceneLoaded;
        journeyLength = Vector3.Distance(camMainPos, camLevelsPos);
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void Awake()
    {
        PlayerPrefs.SetInt("Tutorial", 11);
        if(PlayerPrefs.GetInt("Tutorial") != 0)
        {
            playButton.GetComponent<Image>().color = unlockedCol;
        }
        else
        {
            playButton.GetComponent<Image>().color = lockedCol;
        }


        for(int i=1; i <= Values.levelsCount; i++)
        {
            if(PlayerPrefs.HasKey("LEVEL"+i.ToString()))
            {
                highscoresText[i].text = PlayerPrefs.GetInt("LEVEL" + i.ToString()).ToString();
            }
            else
            {
                highscoresText[i].text = "Not set";
            }
        }
    }

    void Update()
    {
        if(mainMenu)
        {
            float distCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            mainCam.transform.position = Vector3.Lerp(camLevelsPos, camMainPos, fractionOfJourney);

            mainCam.transform.rotation = Quaternion.Lerp(Levels.rotation, Base.rotation, (Time.time - startTime) * rotationSpeed);
        }
        else
        {
            float distCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            mainCam.transform.position = Vector3.Lerp(camMainPos, camLevelsPos, fractionOfJourney);

            mainCam.transform.rotation = Quaternion.Lerp(Base.rotation, Levels.rotation, (Time.time-startTime) * rotationSpeed);
        }
    }
}
