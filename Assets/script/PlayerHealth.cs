using UnityEngine;
using UnityEngine.UI; // Wajib dipanggil untuk bisa mengendalikan komponen UI (seperti Image/Bar Darah)

public class PlayerHealth : MonoBehaviour
{
    // --- DEKLARASI VARIABEL (Pengaturan Nyawa & UI) ---

    // Mengatur jumlah total nyawa maksimal yang dimiliki Player di awal game (100 poin)
    public float nyawaMaksimal = 100f;

    // Menyimpan angka nyawa Player yang sedang berjalan saat game dimainkan
    public float nyawaSekarang;

    // Slot untuk memasukkan gambar Health Bar (Bar Darah Merah) dari Canvas UI ke Inspector
    public Image barMerahUI;

    // --- FUNGSI START (Berjalan 1 kali di awal game dimulai) ---
    void Start()
    {
        // Saat game baru mulai, isi nyawa sekarang sampai penuh sesuai nyawa maksimal (yaitu 100)
        nyawaSekarang = nyawaMaksimal;
    }

    // --- FUNGSI UPDATE (Berjalan terus-menerus setiap frame game) ---
    void Update()
    {
        // Update visual bar merah secara real-time
        // Mengubah panjang isi bar darah (fillAmount) dengan rumus: Nyawa Sekarang dibagi Nyawa Maksimal.
        // Rentang nilainya adalah 0.0 (habis) sampai 1.0 (penuh).
        barMerahUI.fillAmount = nyawaSekarang / nyawaMaksimal;
    }

    // --- SENSOR TABRAKAN FISIK (Otomatis Aktif Saat Badan Player Mentok/Menabrak Objek Lain) ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek apakah objek padat yang barusan ditabrak oleh Player memiliki label atau Tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Kalau terbukti menabrak musuh, langsung panggil fungsi TerimaDamage di bawah sebesar 10 poin!
            TerimaDamage(10f);

            // Memunculkan tulisan teks di Console Unity untuk tanda bahwa Player terkena hit
            Debug.Log("Aduh nabrak musuh!"); // Muncul di console
        }
    }

    // --- FUNGSI BUATAN: PENGURANG NYAWA ---
    // Fungsi ini dibuat publik (public) supaya bisa dipanggil dari script lain (misal dari script jebakan atau peluru musuh)
    public void TerimaDamage(float damage)
    {
        // Kurangi angka nyawaSekarang sesuai dengan jumlah damage yang masuk
        nyawaSekarang -= damage;

        // Mengecek apakah setelah dikurangi, nyawa Player sudah habis (0 atau minus)
        if (nyawaSekarang <= 0)
        {
            // Mengunci angka nyawa di angka 0 agar tidak minus (misal jadi -10)
            nyawaSekarang = 0;

            // Mengirim pesan ke Console Unity bahwa karakter kamu sudah kalah/mati
            Debug.Log("Prajurit Gugur!");

            // Nanti bisa dipanggil animasi mati di sini
        }
    }
}