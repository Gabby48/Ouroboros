using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;

public class CycleComplete : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cyclecompleteperkText;
    [SerializeField] private Button nextCycle;

    private const string perkText = "You are faster - Movement Speed *1.5x";


    private void Awake()
    {
        nextCycle.onClick.AddListener(() =>
        {
            RestartGame();

        });

    }


    // Start is called before the first frame update
    void Start()
    {
        Hide();
        Movement.OnCycleComplete += Movement_OnCycleComplete;
    }


    private void Movement_OnCycleComplete(object sender, System.EventArgs e)
    {
        
        cyclecompleteperkText.text = perkText;
        Show();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
