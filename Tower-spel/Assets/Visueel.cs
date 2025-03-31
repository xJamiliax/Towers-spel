using UnityEngine;

[ExecuteAlways]
public class DrawWallVectors : MonoBehaviour
{
    public Vector3 wallPosition = new Vector3(0, 0, 5); // muur staat 5 eenheden voor je
    public Vector3 wallNormal = new Vector3(0, 0, -1);  // muurNormal wijst naar jou toe
    public float vectorLength = 2f;

    private void OnDrawGizmos()
    {
        Vector3 origin = wallPosition; // we tekenen vanaf de muurpositie
        Vector3 up = Vector3.up;
        Vector3 alongWall = Vector3.Cross(wallNormal, up).normalized * vectorLength;

        // Teken muurpositie
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(wallPosition, new Vector3(4, 4, 0.1f));

        // Teken wallNormal
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + wallNormal * vectorLength);
        Gizmos.DrawSphere(origin + wallNormal * vectorLength, 0.05f);
        DrawLabel(origin + wallNormal * vectorLength, "wallNormal", Color.red);

        // Teken up
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + up * vectorLength);
        Gizmos.DrawSphere(origin + up * vectorLength, 0.05f);
        DrawLabel(origin + up * vectorLength, "up", Color.green);

        // Teken alongWall
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin, origin + alongWall);
        Gizmos.DrawSphere(origin + alongWall, 0.05f);
        DrawLabel(origin + alongWall, "alongWall", Color.blue);

        // Teken jouw transform.forward vanaf je eigen positie
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * vectorLength);
        Gizmos.DrawSphere(transform.position + transform.forward * vectorLength, 0.05f);
        DrawLabel(transform.position + transform.forward * vectorLength, "transform.forward", Color.magenta);
    }

    void DrawLabel(Vector3 position, string text, Color color)
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.Label(position + Vector3.up * 0.1f, text);
#endif
    }
}
