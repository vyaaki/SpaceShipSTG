using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript instance;
    private readonly bool isStarted = false;

    [SerializeField] private GameObject menu;

    private int score;
    [SerializeField] private Button startButton;
    [SerializeField] private Text text;

    public bool GetIsStarted()
    {
        return isStarted;
    }

    public void IncreaseScore(int value)
    {
        score += value;
        text.text = "Score: " + score;
    }

    private void Start()
    {
        Time.timeScale = 0;
        instance = this;
        startButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }
}