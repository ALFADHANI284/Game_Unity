using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Wajib dipanggil agar kita bisa menggunakan fungsi perpindahan Scene/Level

public class PindahScene : MonoBehaviour
{
    // --- FUNGSI BUATAN: PINDAH SCENE ---
    // Fungsi ini dibuat publik (public) supaya bisa dideteksi dan dipasang ke komponen tombol (Button UI) di Unity.
    // Fungsi ini membutuhkan satu data tambahan yaitu 'int level' (angka index dari scene yang mau dituju).
    public void LoadScene(int level)
    {
        // Perintah sakti dari Unity untuk menutup scene yang sekarang dan membuka scene baru
        // sesuai dengan nomor index yang kita masukkan (misal: index 0 untuk Main Menu, index 1 untuk Level 1).
        SceneManager.LoadScene(level);
    }
}