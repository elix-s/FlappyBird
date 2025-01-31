using UnityEngine;
using VContainer;

public class WallMovement : MonoBehaviour
{
    public float speed = 2f;
    private GameSessionService _gameSessionService;

    [Inject]
    private void Construct(GameSessionService gameSessionService)
    {
        _gameSessionService = gameSessionService;
    }

    void Update()
    {
        if(_gameSessionService.GameRunning)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            Vector2 currentPosition = transform.position;
            if (currentPosition.x <= -10.00f) Destroy(gameObject);
        }
    }
}
