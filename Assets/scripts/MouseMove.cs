using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseMove : MonoBehaviour
{
    [Tooltip("Mesh o BoxCollider que define el volumen l�mite del movimiento")]
    public Collider boundaryCollider;

    [Tooltip("Offset de profundidad desde la c�mara (si es necesario)")]
    public float depthFromCamera = 10f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (boundaryCollider == null)
        {
            Debug.LogError("No se asign� un Boundary Collider.");
        }
    }

    void Update()
    {
        if (boundaryCollider == null) return;

        // Obtener la posici�n del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = depthFromCamera;

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePosition);

        // Ajustar la posici�n al l�mite del collider
        Vector3 closestPoint = boundaryCollider.ClosestPoint(worldPoint);

        transform.position = closestPoint;
    }
}
