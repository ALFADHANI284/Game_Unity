using UnityEngine;

public class Player : MonoBehaviour
{
    // --- DEKLARASI VARIABEL (Pengaturan & Komponen) ---

    // Mengatur kecepatan berjalan si Player (bisa diubah ukurannya dari Inspector Unity)
    public float kecepatan = 5f;

    // Mengatur seberapa tinggi Player bisa melompat ke atas
    public float kekuatanLompat = 7f;

    // Menampung objek kosong (Empty Object) yang ditaruh di depan pedang sebagai pusat area pukulan
    public Transform titikSerang;

    // Jari-jari atau radius area lingkaran deteksi tebasan pedang
    public float jarakSerang = 0.5f;

    // Batas maksimal jumlah lompatan yang boleh dilakukan karakter
    public int maxLompat = 5;

    // Angka pengingat untuk menghitung sisa jatah lompatan yang tersisa
    private int sisaLompat;

    // Tempat menyimpan komponen fisik 2D milik Player (buat ngatur gravitasi & kecepatan)
    private Rigidbody2D rb;

    // Tempat menyimpan komponen Animator (buat memicu animasi jalan, lompat, atau serang)
    private Animator anim;

    // Tempat menyimpan komponen Sprite Renderer (buat membalik gambar karakter kiri/kanan)
    private SpriteRenderer sprite;

    // --- FUNGSI START (Berjalan 1 kali di awal game dimulai) ---
    void Start()
    {
        // Otomatis mencari dan mengambil komponen Rigidbody2D yang nempel di tubuh Player
        rb = GetComponent<Rigidbody2D>();

        // Mengisi nilai sisaLompat sesuai dengan jumlah jatah maxLompat (yaitu 5) di awal game
        sisaLompat = maxLompat;

        // Otomatis mengambil komponen Animator yang nempel di tubuh Player
        anim = GetComponent<Animator>();

        // Otomatis mengambil komponen SpriteRenderer yang nempel di tubuh Player
        sprite = GetComponent<SpriteRenderer>();
    }

    // --- FUNGSI UPDATE (Berjalan terus-menerus setiap frame game) ---
    void Update()
    {
        // 1. LOGIKA GERAK KANAN KIRI (Pakai tombol A/D atau tombol Panah)

        // Membaca input: Kiri bernilai -1, Kanan bernilai 1, kalau diam bernilai 0
        float gerak = Input.GetAxisRaw("Horizontal");

        // Menggerakkan fisik Player (X diisi hasil input dikali kecepatan, Y dibiarkan mengikuti gravitasi bumi)
        rb.linearVelocity = new Vector2(gerak * kecepatan, rb.linearVelocity.y);

        // Mengirimkan nilai kecepatan ke parameter "Speed" di Animator. 
        // Mathf.Abs digunakan agar nilainya selalu positif (meskipun bergerak ke kiri/minus)
        anim.SetFloat("Speed", Mathf.Abs(gerak));

        // Membalik gambar ke kiri jika mendeteksi arah gerakan minus (tombol A / panah kiri)
        if (gerak < 0)
        {
            sprite.flipX = true;
        }
        // Mengembalikan posisi gambar menghadap kanan jika bergerak positif (tombol D / panah kanan)
        else if (gerak > 0)
        {
            sprite.flipX = false;
        }

        // 2. LOGIKA LOMPAT (Pakai Tombol Spasi)

        // Jika mendeteksi kamu menekan tombol Spasi ke bawah
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Memberikan sentakan gaya vertikal ke atas sesuai nilai kekuatanLompat
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, kekuatanLompat);
        }

        // 3. LOGIKA MENYERANG (Klik Kiri Mouse)

        // Angka 0 mendeteksi Klik Kiri pada Mouse kamu
        if (Input.GetMouseButtonDown(0))
        {
            // Mengaktifkan Trigger bernama "Attack" di dalam Animator untuk memulai animasi tebasan
            anim.SetTrigger("Attack");

            // Memanggil fungsi buatan kita di bawah untuk mendeteksi apakah ada musuh yang kena tebas
            SerangMusuh();
        }
    }

    // --- FUNGSI BUATAN: DETEKSI SERANGAN ---
    void SerangMusuh()
    {
        // Membuat lingkaran deteksi gaib/kasat mata di posisi 'titikSerang' sebesar radius 'jarakSerang'.
        // Semua benda yang terjebak masuk ke dalam lingkaran ini akan disimpan dalam daftar array 'bendaYangKena'
        Collider2D[] bendaYangKena = Physics2D.OverlapCircleAll(titikSerang.position, jarakSerang);

        // Memeriksa satu per satu setiap objek yang tidak sengaja terjebak di dalam lingkaran tadi
        foreach (Collider2D benda in bendaYangKena)
        {
            // Mengecek apakah objek tersebut dipasang label atau Tag "Enemy"
            if (benda.CompareTag("Enemy"))
            {
                // Jika benar musuh, cari script 'EnemyHealth' yang menempel di tubuh musuh itu, 
                // lalu panggil fungsi 'TerimaDamage' dan kurangi darahnya sebesar 50 poin!
                benda.GetComponent<EnemyHealth>().TerimaDamage(50f);

                // Memunculkan tulisan tes di Console Unity untuk memastikan kodenya bekerja
                Debug.Log("Kena Tebas!");
            }
        }
    }

    // --- FUNGSI GIZMOS (Bantuan Visual Khusus Editor Unity) ---
    private void OnDrawGizmosSelected()
    {
        // Jika kamu belum memasukkan objek ke kolom titikSerang di Inspector, abaikan fungsi ini agar tidak error
        if (titikSerang == null) return;

        // Menggambar lingkaran kawat di jendela 'Scene' Unity biar kamu tahu seberapa jauh jangkauan pedangmu
        Gizmos.DrawWireSphere(titikSerang.position, jarakSerang);
    }

    // --- FUNGSI SENSOR TABRAKAN/TEMBUS (Otomatis Aktif Saat Menyentuh Benda 'Is Trigger') ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah benda yang tidak sengaja dilewati/ditembus oleh tubuh Player punya Tag "Coin"
        if (collision.gameObject.CompareTag("Coin"))
        {
            // Jika benar koin, langsung hapus objek koin tersebut dari map game karena ceritanya sudah diambil
            Destroy(collision.gameObject);
        }
    }
}