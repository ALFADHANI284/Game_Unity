using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // --- DEKLARASI VARIABEL (Pengaturan Nyawa Musuh) ---

    // Mengatur jumlah total nyawa awal yang dimiliki oleh musuh (100 poin)
    public float nyawa = 100f;

    // --- FUNGSI BUATAN: MENERIMA DAMAGE ---
    // Fungsi ini dibuat publik (public) supaya bisa dipanggil oleh script lain, 
    // contohnya dipanggil oleh script 'Player' saat pedang mengenai musuh ini.
    public void TerimaDamage(float damage)
    {
        // Kurangi angka variabel 'nyawa' musuh sesuai dengan jumlah damage yang masuk
        nyawa -= damage;

        // Mengecek apakah setelah darahnya dikurangi, nyawa musuh sudah habis (0 atau minus)
        if (nyawa <= 0)
        {
            // Memunculkan tulisan teks di Console Unity untuk tanda bahwa musuh sudah kalah
            Debug.Log("Musuh Mati Ditebas!");

            // Ini fungsi sakti untuk menghilangkan/menghapus objek musuh ini secara permanen dari layar game
            Destroy(gameObject);
        }
    }
}