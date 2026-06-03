using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk urusan pindah Scene

public class FinishLine : MonoBehaviour
{
    [Header("Masukkan Panel UI")]
    public GameObject panelFinish;

    [Header("UI Gameplay (Yang Mau Dihilangkan)")]
    // Pakai array (tanda []) biar kamu bisa masukin banyak objek sekaligus
    public GameObject[] uiGameplay;
    [Header("Pengaturan Nama Scene")]
    // Pastikan nama scene diketik sama persis (huruf besar/kecilnya)
    public string namaSceneMenu = "mainscreen";
    public string namaSceneLevelSelanjutnya = "level2";

    void Start()
    {
        // Memastikan panel sembunyi saat level baru dimulai
        if (panelFinish != null)
        {
            panelFinish.SetActive(false);
        }
    }

    // Fungsi ini berjalan otomatis saat Player menyentuh kotak Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memastikan yang menyentuh adalah objek dengan tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Tampilkan panel finish
            panelFinish.SetActive(true);

            // (Opsional) Pause game agar player dan musuh berhenti bergerak
            Time.timeScale = 0f;
        }
    }

    // --- FUNGSI UNTUK DIKLIK OLEH TOMBOL ---

    public void KeMenuUtama()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal
        SceneManager.LoadScene(namaSceneMenu);
    }

    public void KeLevelSelanjutnya()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal
        SceneManager.LoadScene(namaSceneLevelSelanjutnya);
    }
}