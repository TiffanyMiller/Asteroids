using UnityEngine;

namespace Ship
{
    [CreateAssetMenu(fileName = "New Spaceship", menuName = "Scriptable Objects/New Spaceship")]
    internal class Spaceship : ScriptableObject
    {
        [SerializeField] internal Sprite sprite;
        [SerializeField] internal float moveSpeed = 10f;
        [Tooltip("Acceleration applied to movement before reaching moveSpeed")]
        [SerializeField] internal float moveDamp = 10f;
        [Tooltip("Acceleration applied to movement before reaching rotationSpeed")]
        [SerializeField] internal float rotationSpeed = 1f;
        [SerializeField] internal float rotationDamp = 10f;
        [SerializeField] internal int startHealth = 1;
    }
}