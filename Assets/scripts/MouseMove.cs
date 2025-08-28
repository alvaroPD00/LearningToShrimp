using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseMove : MonoBehaviour
{
    [Tooltip("Mesh o BoxCollider que define el volumen límite del movimiento")]
    public Collider boundaryCollider;

    [Tooltip("Qué tan rápido el objeto sigue al mouse (valores mayores = seguimiento más ágil)")]
    [Range(0.1f, 20f)]
    public float sensitivity = 5f;

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

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Usamos la orientación del collider para definir el plano (XY frente a la cámara)
        Plane movementPlane = new Plane(boundaryCollider.transform.forward, boundaryCollider.transform.position);

        if (movementPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);

            // Limitar el movimiento dentro del collider
            Vector3 closestPoint = boundaryCollider.ClosestPoint(worldPoint);

            // Interpolación suave
            transform.position = Vector3.Lerp(
                transform.position,
                closestPoint,
                Time.deltaTime * sensitivity
            );
        }
    }
}
