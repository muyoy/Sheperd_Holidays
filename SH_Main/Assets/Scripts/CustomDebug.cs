using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomDebug
{
    public static void DrawRectange(Vector2 top_right_corner, Vector2 bottom_left_corner)
    {
        Vector2 displacement_vector = top_right_corner - bottom_left_corner;
        float x = Vector2.Dot(displacement_vector, Vector2.right);
        float y = Vector2.Dot(displacement_vector, Vector2.up);

        Vector2 top_left_corner = new Vector2(-x * 0.5f, y * 0.5f);
        Vector2 bottom_right_corner = new Vector2(x * 0.5f, -y * 0.5f);

        Gizmos.DrawLine(top_right_corner, top_left_corner);
        Gizmos.DrawLine(top_left_corner, bottom_left_corner);
        Gizmos.DrawLine(bottom_left_corner, bottom_right_corner);
        Gizmos.DrawLine(bottom_right_corner, top_right_corner);
    }
}
