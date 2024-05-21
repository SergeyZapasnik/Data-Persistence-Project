using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public InputField playerNameInputField;
    public Text bestScore;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance != null && bestScore != null)
        {
            bestScore.text = "Best Score: " + DataManager.Instance.PlayerName + " : " + DataManager.Instance.PlayerScore;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        DataManager.Instance.CurrentPlayerName = playerNameInputField.text;

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //MainManager.Instance.SaveColor();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); // original code to quit Unity player
#endif
    }
}
