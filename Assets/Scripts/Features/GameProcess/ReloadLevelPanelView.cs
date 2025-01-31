using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class ReloadLevelPanelView : MonoBehaviour
{
    [SerializeField] private Button _reloadLevelButton;
    [SerializeField] private Button _toMenuButton;
    
    private SceneLoader _sceneLoader;
    private UIService _uiService;

    [Inject]
    private void Construct(SceneLoader sceneLoader, UIService uiService)
    {
        _sceneLoader = sceneLoader;
        _uiService = uiService;
    }

    private void Awake()
    {
        _reloadLevelButton.onClick.AddListener(() => ReloadLevel());
    }

    private void ReloadLevel()
    {
        _uiService.HideUI().Forget();
        _sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }
}
