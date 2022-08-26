using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject ship, asteroid;
    [SerializeField] private float shipSpeed = 2f;
    [SerializeField] private float asteroidSpeed = 2f;
    [SerializeField] private float asteroidDelay = 2f;
    [SerializeField] private Vector2 asteroidEndPos = new Vector2(0, 0.6f);

    private void Start()
    {
        LeanTween.moveX(ship, -9.45f, shipSpeed).setEaseOutSine();
        LeanTween.move(asteroid, asteroidEndPos, asteroidSpeed).setDelay(asteroidDelay).setEaseOutSine();
    }
}
