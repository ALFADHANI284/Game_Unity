using UnityEngine;
using TMPro; // Wajib dipanggil untuk bisa mengendalikan komponen UI TextMeshPro (Teks generasi baru di Unity)

public class PlayerCoin : MonoBehaviour
{
    // --- DEKLARASI VARIABEL (Pengaturan Skor Koin & UI) ---

    [Header("Pengaturan Koin")] // Membuat judul kecil di jendela Inspector Unity agar terlihat rapi
    public int jumlahKoin = 0; // Variabel angka untuk menyimpan total koin yang sudah dikumpulkan
    public TextMeshProUGUI teksUIKoin; // Slot untuk memasukkan objek teks UI TextMeshPro dari Canvas ke Inspector

    // --- FUNGSI START (Berjalan 1 kali di awal game dimulai) ---
    void Start()
    {
        // Pas game baru mulai, jalankan fungsi UpdateUITeks agar angka di layar langsung sinkron (misal: "Koin: 0")
        UpdateUITeks();
    }

    // --- SENSOR TEMBUS (Otomatis Aktif Saat Player Melewati Objek Ber-Collider yang Dicentang 'Is Trigger') ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah benda tembus pandang yang barusan disentuh oleh Player memiliki label atau Tag "Coin"
        if (collision.gameObject.CompareTag("Coin"))
        {
            jumlahKoin += 1; // Menambahkan angka total koin yang dimiliki dengan 1

            UpdateUITeks(); // Langsung panggil fungsi di bawah untuk memperbarui tulisan koin yang ada di layar

            // Memunculkan pesan teks di Console Unity untuk mempermudah pengecekan selama masa pembuatan game
            Debug.Log("Dapat Koin! Total uang sekarang: " + jumlahKoin);

            // Hancurkan atau hapus objek koin yang barusan disentuh dari map game agar tidak bisa diambil lagi
            Destroy(collision.gameObject);
        }
    }

    // --- FUNGSI BUATAN: UPDATE TULISAN DI LAYAR ---
    void UpdateUITeks()
    {
        // Pengaman: Cek dulu apakah kamu sudah menyeret objek teks ke kolom teksUIKoin di Inspector Unity
        if (teksUIKoin != null)
        {
            // Jika sudah diisi, ubah teks di layar menjadi kata "Koin: " digabung dengan angka jumlahKoin saat ini
            teksUIKoin.text = "Koin: " + jumlahKoin;
        }
        else
        {
            // Jika kolomnya masih kosong, munculkan peringatan warna kuning di Console biar kamu tidak bingung kenapa teksnya tidak berubah
            Debug.LogWarning("Teks UI Koin belum dimasukkan ke Inspector prajurit!");
        }
    }
}