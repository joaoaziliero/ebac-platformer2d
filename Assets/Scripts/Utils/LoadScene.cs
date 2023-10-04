using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// O script implementa transições entre as cenas,
// bem como a opção de sair do jogo.

public class LoadScene : MonoBehaviour
{
    public void Load(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadAll()
    {
        Application.Quit();
    }
}
