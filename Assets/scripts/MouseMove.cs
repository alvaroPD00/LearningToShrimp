using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseMove : MonoBehaviour
{
    [Tooltip("Mesh o BoxCollider que define el volumen límite del movimiento")]
    public Collider boundaryCollider;

    [Tooltip("Offset de profundidad desde la cámara (si es necesario)")]
    public float depthFromCamera = 10f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (boundaryCollider == null)
        {
            Debug.LogError("No se asignó un Boundary Collider.");
        }
    }

    void Update()
    {
        if (boundaryCollider == null) return;

        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = depthFromCamera;

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePosition);

        // Ajustar la posición al límite del collider
        Vector3 closestPoint = boundaryCollider.ClosestPoint(worldPoint);

        transform.position = closestPoint;
    }
}
