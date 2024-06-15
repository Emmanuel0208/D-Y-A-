using UnityEngine;

public class FloatingProp : MonoBehaviour
{
    public float floatAmplitude = 0.5f;  // Amplitud de la flotación
    public float floatSpeed = 1f;  // Velocidad de la flotación
    public float rotationSpeed = 50f;  // Velocidad de la rotación
    public Axis rotationAxis = Axis.Y;  // Eje de rotación

    private Vector3 startPosition;

    public enum Axis
    {
        X,
        Y,
        Z
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Flotación
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotación
        Vector3 rotationVector = Vector3.zero;
        switch (rotationAxis)
        {
            case Axis.X:
                rotationVector = Vector3.right;
                break;
            case Axis.Y:
                rotationVector = Vector3.up;
                break;
            case Axis.Z:
                rotationVector = Vector3.forward;
                break;
        }
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
