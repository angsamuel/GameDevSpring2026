using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class MainMenu : MonoBehaviour
{
    public Image coverImage;

    public Color fadeOutColor;
    Color clearColor;

    public float fadeOutTime = 1;

    bool playingGame = false;

    void Awake()
    {
        clearColor = fadeOutColor;
        clearColor.a = 0;
    }

    public void PlayGame(){
        if (playingGame)
        {
            return;
        }
        playingGame = true;
        StartCoroutine(PlayGameRoutine());
    }

    IEnumerator PlayGameRoutine()
    {
        float timer = 0;
        while(timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            coverImage.color = Color.Lerp(clearColor, fadeOutColor, timer/fadeOutTime);
            yield return null; //okay wait for a frame, and then resume right here
        }

        coverImage.color = fadeOutColor;
        yield return null;
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Application would quit here :3");
        Application.Quit();
    }
}
