using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 4f; 
    private float smoothTime = 0.1f; 
    private float maxHeight = 5f; 
    private float minHeight = -5f; 

    private Rigidbody2D rb;
    private Vector2 targetVelocity; 
    private Vector2 currentVelocity; 
    
    private ScoreManager _scoreManager;
    private Logger _logger;
    private UIService _uiService;
    private GameSessionService _gameSessionService;

    [Inject]
    private void Construct(Logger logger, ScoreManager scoreManager, UIService uiService, GameSessionService gameSessionService)
    {
        _logger = logger;
        _scoreManager = scoreManager;
        _uiService = uiService;
        _gameSessionService = gameSessionService;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetVelocity = rb.velocity;
        _gameSessionService.GameRunning = true;
    }

    void Update()
    {
        if (_gameSessionService.GameRunning)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                targetVelocity = Vector2.up * jumpForce;
            }
            
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
            targetVelocity = Vector2.Lerp(targetVelocity, Vector2.down * 2f, Time.deltaTime);
            
            LimitHeight();
        }
    }

    void LimitHeight()
    {
        Vector2 currentPosition = transform.position;
        
        if (currentPosition.y > maxHeight)
        {
            currentPosition.y = maxHeight;
            transform.position = currentPosition;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            targetVelocity = new Vector2(targetVelocity.x, 0);
        }
        
        if (currentPosition.y < minHeight)
        {
            GameOver();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            _scoreManager.IncreaseScore();
        }
    }

    void GameOver()
    {
        _gameSessionService.GameRunning = false;
        _logger.Log("Game Over");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _uiService.ShowReloadLevelWindow().Forget();
    }
}