using UnityEngine;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "Scriptable Objects/New Spaceship")]
public class Spaceship : ScriptableObject
{
    public Sprite sprite;
    public float moveSpeed = 10f;
    [Tooltip("Acceleration applied to movement before reaching moveSpeed")]
    public float moveDamp = 10f;
    [Tooltip("Acceleration applied to movement before reaching rotationSpeed")]
    public float rotationSpeed = 1f;
    public float rotationDamp = 10f;
    public int startHealth = 1;
}
