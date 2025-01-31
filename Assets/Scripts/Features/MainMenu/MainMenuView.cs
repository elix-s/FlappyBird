using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MainMenuView : MonoBehaviour
{
   [SerializeField] private Button _playButton;
   private GameStateService _gameStateService;
   private UIService _uiService;

   [Inject]
   private void Construct(GameStateService gameStateService, UIService uiService)
   {
      _gameStateService = gameStateService;
      _uiService = uiService;
   }

   private void Awake()
   {
      _playButton.onClick.AddListener(PlayGame);
   }

   private void PlayGame()
   {
      _uiService.HideUI().Forget();
      _gameStateService.ChangeState<StartGameState>();
   }
}
