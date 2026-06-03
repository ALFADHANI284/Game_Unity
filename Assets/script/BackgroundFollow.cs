using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    // Masukkan Main Camera ke sini di Inspector
    public Transform kameraUtama;

    void LateUpdate()
    {
        if (kameraUtama != null)
        {
            // Background HANYA mengikuti pergerakan kamera ke kiri dan kanan (sumbu X).
            // Ketinggian (Y) dan kedalaman (Z) background akan tetap di posisinya sendiri.
            transform.position = new Vector3(kameraUtama.position.x, transform.position.y, transform.position.z);
        }
    }
}