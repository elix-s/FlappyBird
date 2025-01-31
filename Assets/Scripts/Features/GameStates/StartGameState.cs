using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameState : IGameState
{
    private SceneLoader _sceneLoader;
    
    public StartGameState(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void Enter()
    {
        Debug.Log("StartGameState Enter");
        _sceneLoader.LoadScene("GameScene");
    }
    public void Update() {}
    public void Exit() {}
}