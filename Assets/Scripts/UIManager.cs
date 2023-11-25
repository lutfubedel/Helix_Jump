using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image white_Image;
    public TMP_Text score_text;
    public TMP_Text hight_score_text;
    public int effect_control;
    public GameObject restart_screen;

    [Header("Scripts")]
    public PlayerManager pManager;
    public ObstacleMovement movement;

    [Header("Level-Bar")]
    public GameObject level_bar;
    public Image fill_rate;
    public GameObject player;
    public GameObject finisher;
    public TMP_Text level_beganing;
    public TMP_Text level_ending;

    private void Start()
    {
        hight_score_text.text = "Best Score : " + PlayerPrefs.GetInt("HightScore").ToString();
        level_beganing.text = (PlayerPrefs.GetInt("Level")+1).ToString();
        level_ending.text = (PlayerPrefs.GetInt("Level")+2).ToString();

    }

    
    private void Update()
    {
        score_text.text = pManager.score.ToString();

        if (pManager.isDead==true)
        {
            StartCoroutine(OpenRestartScene());
        }

    }



    public void WhiteEffectFonks()
    {
        StartCoroutine(WhiteEffect());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WhiteEffect()
    {
        white_Image.gameObject.SetActive(true);

        while (effect_control == 0)
        {
            yield return new WaitForSeconds(0.00001f);
            white_Image.color += new Color(0, 0, 0, 0.25f);

            if (white_Image.color == new Color(1, 1, 1, 1))
            {
                effect_control = 1;
            }
        }

        while (effect_control == 1)
        {
            yield return new WaitForSeconds(0.00001f);
            white_Image.color -= new Color(0, 0, 0, 0.25f);

            if (white_Image.color == new Color(1, 1, 1, 0))
            {
                effect_control = 2;
            }
        }

    }

    IEnumerator OpenRestartScene()
    {
        yield return new WaitForSeconds(1);
        restart_screen.SetActive(true);
    }


}
