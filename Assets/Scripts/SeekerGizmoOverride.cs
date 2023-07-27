using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class SeekerGizmoOverride : MonoBehaviour
{
    private Seeker seeker;

    [Header("Gizmo")]
    public Vector3 gizmoLineOffset = new Vector3(0f, -1.5f, 0f);

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    private void OnDrawGizmosSelected()
    {
        if (seeker != null && seeker.IsDone() && seeker.GetCurrentPath() != null)
        {
            var path = seeker.GetCurrentPath();
            Gizmos.color = Color.green;

            for (int i = 0; i < path.vectorPath.Count; i++)
            {
                Vector3 position = path.vectorPath[i] + gizmoLineOffset;
                Gizmos.DrawWireSphere(position, 0.2f);

                if (i > 0)
                {
                    Vector3 prevPosition = path.vectorPath[i - 1] + gizmoLineOffset;
                    Gizmos.DrawLine(prevPosition, position);
                }
            }
        }
    }
}
