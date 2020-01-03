﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void OnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
        AmmoText.ammoAmount = 100;
    }
}
