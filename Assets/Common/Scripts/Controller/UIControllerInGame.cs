/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using TMPro;
using UnityEngine;


/// <summary>
/// Singleton class responsible for managing in-game UI elements and game pause functionality.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class UIControllerInGame : MonoBehaviour
{
    /// <summary>
    /// The single instance of the UIController.
    /// </summary>
    private static UIControllerInGame MY_INSTANCE;

    /// <summary>
    /// Array of audio clips for UI sounds.
    /// </summary>
    [SerializeField] private AudioClip[] myAudioClips;

    /// <summary>
    /// Custom cursor texture.
    /// </summary>
    [SerializeField] private Texture2D myCursorTexture;

    /// <summary>
    /// Audio source for UI sounds.
    /// </summary>
    [SerializeField] private AudioSource myAudioSource;

    /// <summary>
    /// Navigation popup GameObject.
    /// </summary>
    [SerializeField] private GameObject myNavPopup;

    /// <summary>
    /// Result window GameObject.
    /// </summary>
    [SerializeField] private GameObject myResultWindow;

    /// <summary>
    /// Pause menu GameObject.
    /// </summary>
    private GameObject myPauseMenu;

    /// <summary>
    /// Mini Map GameObject.
    /// </summary>
    [SerializeField] private GameObject myMiniMap;

    /// <summary>
    /// Question window controller reference.
    /// </summary>
    private QuestionWindowController myQuestionWindowControllerController;

    /// <summary>
    /// Flag indicating if the game is paused.
    /// </summary>
    private bool myIsPaused;


    /// <summary>
    /// Initializes the UIControllerInGame instance and sets up initial UI elements.
    /// </summary>
    void Start()
    {
        myPauseMenu = GameObject.Find("PauseMenu");
        myPauseMenu.SetActive(false);
        myMiniMap.SetActive(!myIsPaused);
        Cursor.SetCursor(myCursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Awake()
    {
        if (MY_INSTANCE != null && MY_INSTANCE != this)
        {
            Debug.Log("There is already an instance of the UIController in the scene!");
        }
        else
        {
            MyInstance = this;
        }
    }

    /// <summary>
    /// Update is called once per frame and handles game pause functionality.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude == 0)
            {
                myIsPaused = !myIsPaused;
                if (myIsPaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
    }

    /// <summary>
    /// Displays the win or lose window based on the result.
    /// </summary>
    /// <param name="theResult">True for a win, false for a loss.</param>
    public void SetWinOrLoseWindow(in bool theResult)
    {
        myResultWindow.SetActive(true);
        string resultText;

        if (!theResult)
        {
            resultText = "You Won! \nPlay Again?";
        }
        else
        {
            resultText = "You Lost! \nPlay Again?";
        }

        myResultWindow.GetComponentInChildren<TMP_Text>().SetText(resultText);
    }


    /// <summary>
    /// Shows or hides the navigation popup.
    /// </summary>
    /// <param name="theIsShowing">True to show the popup, false to hide it.</param>
    public void ShowNav(in bool theIsShowing)
    {
        myNavPopup.SetActive(theIsShowing);
    }

    /// <summary>
    /// Pauses the game by setting the time scale to 0.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        myPauseMenu.SetActive(true);
        myMiniMap.SetActive(false);
    }

    /// <summary>
    /// Pauses the game by setting the time scale to 0.
    /// </summary>
    public void ResumeGame()
    {
        myIsPaused = false;
        Time.timeScale = 1;
        myPauseMenu.SetActive(false);
        myMiniMap.SetActive(true);
    }


    /// <summary>
    /// Plays a UI sound using the specified audio clip index.
    /// </summary>
    public void PlayUISound(in int theAudioClipIndex)
    {
        myAudioSource.PlayOneShot(myAudioClips[theAudioClipIndex]);
    }

    /// <summary>
    /// Gets or sets the instance of the UIControllerInGame singleton class.
    /// </summary>
    public static UIControllerInGame MyInstance
    {
        get => MY_INSTANCE;
        private set => MY_INSTANCE = value;
    }
    
}
  