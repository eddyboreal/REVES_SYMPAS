using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Canvas LevelScore = default;
    public int nbEmennemiesKilled = 0;

    public int TotalEnnemies = default;
    public Canvas ScoreCanvas = default;
    public Canvas FailedCanvas = default;

    private bool canvasDisplayed = false;

    private GameObject PlayerUI = default;

    private Helper helper = default;

    private Player player = default;

    private bool levelOver = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        helper = GameObject.FindGameObjectWithTag("Helper").GetComponent<Helper>();

        helper.FadeColor(LevelScore.GetComponentInChildren<Button>().GetComponentInChildren<Text>(), Color.white, Color.red, Color.red, Color.white, 0.5f, 0.5f);
    }

    void Update() 
    {
        if (nbEmennemiesKilled == TotalEnnemies && !canvasDisplayed) 
        {
            levelOver = true;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            player.GetComponentInChildren<Blaster>().CanReceiveInputs = false;
            player.GetComponentInChildren<Blaster>().ResetBulletTime();

            player.GetComponentInChildren<MouseLook>().CanMove = false;
            player.GetComponent<PlayerMovement>().CanMove = false;

            PlayerUI.transform.GetChild(0).gameObject.SetActive(false);

            PlayerUI.GetComponent<Animator>().SetBool("displayOut", true);
            PlayerUI.GetComponent<Animator>().speed = PlayerUI.GetComponent<Animator>().speed * (1f / Time.timeScale);
            ScoreCanvas.gameObject.SetActive(true);

            StartCoroutine(TypeSentence(ScoreCanvas.transform.GetChild(1).GetComponent<Text>()));
            ScoreCanvas.transform.GetChild(2).GetComponent<Text>().text = PlayerUI.transform.GetChild(1).GetChild(3).GetComponent<Text>().text;
            canvasDisplayed = true;
        }

        if(player.health == 0 && !canvasDisplayed)
        {
            levelOver = true;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            PlayerUI.GetComponent<Animator>().SetBool("displayOut", true);
            PlayerUI.GetComponent<Animator>().speed = PlayerUI.GetComponent<Animator>().speed * (1f / Time.timeScale);
            FailedCanvas.gameObject.SetActive(true);

            StartCoroutine(TypeSentence(FailedCanvas.transform.GetChild(1).GetComponent<Text>()));
            canvasDisplayed = true;
        }
    }

    public bool IsLevelOver()
    {
        return levelOver;
    }

    public void OnHover()
    {
        FailedCanvas.GetComponentInChildren<Button>().GetComponentInChildren<Text>().GetComponent<RectTransform>().localScale = new Vector3(0.125f, 0.125f, 1f);
        LevelScore.GetComponentInChildren<Button>().GetComponentInChildren<Text>().GetComponent<RectTransform>().localScale = new Vector3(0.125f, 0.125f, 1f);
    }

    public void LeftHover()
    {
        FailedCanvas.GetComponentInChildren<Button>().GetComponentInChildren<Text>().GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 1f);
        LevelScore.GetComponentInChildren<Button>().GetComponentInChildren<Text>().GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 1f);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator TypeSentence(Text text)
    {
        string sentence = text.text;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(.025f);
        }
    }
}