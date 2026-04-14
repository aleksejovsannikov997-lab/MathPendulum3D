using UnityEngine;

public class Pendulum3D : MonoBehaviour
{
    [Header("Параметры маятника")]
    [SerializeField] private float length = 2.5f;
    [SerializeField] private float angle = 45f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Ссылки на объекты")]
    [SerializeField] private Transform bob;
    [SerializeField] private Transform rod;

    private float angularVelocity = 0f;
    private float angularAcceleration;
    private float currentAngleRad;

    void Start()
    {
        currentAngleRad = angle * Mathf.Deg2Rad;
        UpdatePendulumPosition();
    }

    void Update()
    {
        angularAcceleration = -(gravity / length) * Mathf.Sin(currentAngleRad);
        angularVelocity += angularAcceleration * Time.deltaTime;
        currentAngleRad += angularVelocity * Time.deltaTime;

        UpdatePendulumPosition();
    }

    void UpdatePendulumPosition()
    {
        float x = Mathf.Sin(currentAngleRad) * length;
        float y = -Mathf.Cos(currentAngleRad) * length;

        bob.localPosition = new Vector3(x, y, 0f);

        if (rod != null)
        {
            Vector3 direction = bob.localPosition.normalized;
            float distance = length;

            rod.localPosition = direction * (distance / 2f);
            rod.up = direction;
            rod.localScale = new Vector3(0.1f, distance, 0.1f);
        }
    }
}