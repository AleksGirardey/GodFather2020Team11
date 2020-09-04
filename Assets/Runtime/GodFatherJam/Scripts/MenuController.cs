using System;
using System.Collections;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public TextMeshPro playButton;
    public TextMeshPro quitButton;

    public Color selectedColor;
    public Color unselectedColor;

    private bool _playSelected = true;
    private Player _player;
    private bool _canSwitch = true;
    private void Awake() {
        _player = ReInput.players.GetPlayer(0);
    }

    private void Update() {
        float moveY = _player.GetAxis("MoveVertical");
        
        if ((moveY > 0.0f || moveY < 0.0f) && _canSwitch)
            Switch();

        if (_player.GetButtonDown("Interaction"))
            Application.Quit();

        if (!_player.GetButtonDown("Jump")) return;
        
        if (_playSelected)
            SceneManager.LoadScene("MainScene");
        else
            Application.Quit();
    }

    private IEnumerator WaitSwitch() {
        yield return new WaitForSeconds(.5f);
        _canSwitch = true;
    }
    
    private void Switch() {
        _canSwitch = false;
        _playSelected = !_playSelected;
        
        if (_playSelected) {
            playButton.color = selectedColor;
            quitButton.color = unselectedColor;
        } else {
            playButton.color = unselectedColor;
            quitButton.color = selectedColor;
        }

        StartCoroutine(WaitSwitch());
    }
}
