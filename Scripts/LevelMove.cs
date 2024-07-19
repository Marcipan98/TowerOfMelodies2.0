using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && GameData.doesPlayerOwnKey())
        {
            print("Switching Scene to " + sceneBuildIndex);
            SoundEffectManager.Play("OpenDoor");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}


