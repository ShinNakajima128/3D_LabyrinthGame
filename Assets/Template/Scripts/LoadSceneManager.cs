using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonMonoBehaviour<LoadSceneManager>
{
    /// <summary> タイトル </summary>
    [SerializeField] string m_titleScene = "Title";
    /// <summary> イージー </summary>
    [SerializeField] string m_easyScene = "";
    /// <summary> ノーマル </summary>
    [SerializeField] string m_normalScene = "";
    /// <summary> ハード </summary>
    [SerializeField] string m_hardScene = "";
    /// <summary> 各ステージ </summary>
    [SerializeField] string[] m_stageScenes = default;
    /// <summary> リザルト </summary>
    [SerializeField] string m_resultScene = "";
    /// <summary> ロードする時間 </summary>
    [SerializeField] float m_LoadTimer = 1.0f;
    /// <summary> フェードさせるImage </summary>
    [SerializeField] Image fadeImage;
    /// <summary> フェードのスピード </summary>
    [SerializeField] float fadeSpeedValue = 1.0f;
    /// <summary> 現在のScene </summary>
    string m_currentScene = "";
    /// <summary> 次に読み込むScene </summary>
    string m_nextScene = "";
    /// <summary> フェードアウトのフラグ </summary>
    public bool isFadeOut = false;
    /// <summary> フェードインのフラグ </summary>
    public bool isFadeIn = false;
    /// <summary> フェードさせるImageのRGBa </summary>
    float red, green, blue, alfa;
    /// <summary> Fade speed </summary>
    const float fadeSpeed = 0.01f;
    [SerializeField] bool m_debugMode = false;


    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        alfa = 1;
        SetAlfa();
    }

    void Start()
    {
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        SceneManager.sceneLoaded += OnSceneLoaded;

        isFadeIn = true;
    }

    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        isFadeIn = true;
        Debug.Log("fade");
    }

    void Update()
    {
        /// When the fade-in begins
        if (isFadeIn)
        {
            StartFadeIn();
        }
        ///When the fade-out begins.
        if (isFadeOut)
        {
            StartFadeOut();
        }

        if (m_debugMode)
        {
           
        }   
    }

    /// <summary> Transition to the next Scene </summary>
    public void LoadNextStage(string name)
    {
        isFadeOut = true;
        StartCoroutine(LoadScene(name, m_LoadTimer));
    }
   
    /// <summary> Transition to the TitleScene </summary>
    public void LoadTitleScene()
    {
        Time.timeScale = 1f;
        isFadeOut = true;
        StartCoroutine(LoadScene(m_titleScene, m_LoadTimer));
    }
    
    /// <summary> Transition to the  AnyScene </summary>
    public void AnyLoadScene(string loadScene)
    {
        isFadeOut = true;
        StartCoroutine(LoadScene(loadScene, m_LoadTimer));
    }


    public void Restart()
    {
        isFadeOut = true;
        StartCoroutine(LoadScene(m_currentScene, m_LoadTimer));
    }
    public void LoadCredit()
    {
        isFadeOut = true;
        StartCoroutine(LoadScene("Credit", m_LoadTimer));
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        isFadeOut = true;
        StartCoroutine(QuitScene(m_LoadTimer));
    }

    /// <summary>
    /// Fade out
    /// </summary>
    void StartFadeOut()
    {
        alfa += fadeSpeedValue * fadeSpeed;
        SetAlfa();

        if (alfa >= 1)
        {
            isFadeOut = false;
        }
    }

    /// <summary>
    /// Fade in
    /// </summary>
    void StartFadeIn()
    {
        alfa -= fadeSpeedValue * fadeSpeed;
        SetAlfa();

        if (alfa <= 0)
        {
            isFadeIn = false;
        }
    }

    void OffPanel()
    {
        isFadeIn = true;
    }

    void OnPanel()
    {
        isFadeOut = true;
    }
    /// <summary>
    /// Set the alpha value
    /// </summary>
    void SetAlfa()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    IEnumerator LoadScene(string name, float timer)
    {
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(name);
    }

    IEnumerator QuitScene(float timer)
    {
        yield return new WaitForSeconds(timer);

        Application.Quit();
    }
}
