using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    // --- TAMBAHAN: Batas Kamera ---
    // Atur angka ini di Inspector. Ini adalah titik terendah kamera boleh turun.
    public float batasBawahY = 0f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            // --- TAMBAHAN: Kunci posisi Y ---
            // Jika posisi kamera yang dituju lebih rendah dari batas bawah,
            // paksa posisi Y tetap di batas bawah.
            if (desiredPosition.y < batasBawahY)
            {
                desiredPosition.y = batasBawahY;
            }

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}