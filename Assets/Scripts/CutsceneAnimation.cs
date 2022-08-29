using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject ship, asteroid;
    [SerializeField] private float shipSpeed = 2f;
    [SerializeField] private float asteroidSpeed = 2f;
    [SerializeField] private float asteroidDelay = 2f;
    [SerializeField] private Vector2 asteroidEndPos = new Vector2(0, 0.6f);
    
    public static bool cutsceneComplete;

    private CountdownTimer _durationTimer;

    private void Start()
    {
        _durationTimer = new CountdownTimer();
        cutsceneComplete = false;

        LeanTween.moveX(ship, -9.45f, shipSpeed).setEaseOutSine();
        LeanTween.move(asteroid, asteroidEndPos, asteroidSpeed).setDelay(asteroidDelay).setEaseOutSine();
        
        var duration = shipSpeed + asteroidSpeed + asteroidDelay;
        _durationTimer.Set(duration, () => cutsceneComplete = true);
    }

    private void Update()
    {
        _durationTimer.Run();
    }
}
