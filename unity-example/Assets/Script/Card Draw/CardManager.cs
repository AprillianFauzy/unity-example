using UnityEngine;

public class CardGridSpawner : MonoBehaviour
{
    public GameObject cardPrefab;  // Prefab kartu yang akan ditampilkan
    public int rows = 2;           // Jumlah baris
    public int columns = 5;        // Jumlah kolom
    public float xOffset = 1.5f;   // Jarak antar kartu secara horizontal
    public float yOffset = 2.0f;   // Jarak antar kartu secara vertikal
    public Vector3 cameraOffset = new Vector3(0, 0, 5); // Offset grid dari kamera
    public Vector3 cardRotation = new Vector3(-70, 180, 0); // Rotasi kartu dalam derajat

    private GameObject[] spawnedCards; // Array untuk menyimpan kartu yang di-spawn
    private bool isGridVisible = false; // Untuk memeriksa apakah grid terlihat

    void Start()
    {
        SpawnMainCard();
    }

    // Spawn kartu utama
    void SpawnMainCard()
    {
        // Ambil kamera utama
        Camera mainCamera = Camera.main;
        Vector3 centerPosition = mainCamera.transform.position + mainCamera.transform.forward * cameraOffset.z;
        
        // Buat kartu utama di tengah layar
        GameObject mainCard = Instantiate(cardPrefab, centerPosition, Quaternion.Euler(cardRotation));
        mainCard.AddComponent<Card>().Initialize(this, true); // Tambahkan komponen Card dan inisialisasi sebagai kartu utama
    }

    // Spawn grid 10 kartu
    public void SpawnGrid()
    {
        if (isGridVisible) return; // Cegah jika grid sudah terlihat

        // Ambil kamera utama
        Camera mainCamera = Camera.main;
        Vector3 centerPosition = mainCamera.transform.position + mainCamera.transform.forward * cameraOffset.z;

        // Hitung posisi awal kartu pertama (pojok kiri atas grid)
        Vector3 startPosition = centerPosition
                               - mainCamera.transform.right * ((columns - 1) * xOffset / 2)
                               + mainCamera.transform.up * ((rows - 1) * yOffset / 2);

        spawnedCards = new GameObject[rows * columns];
        int index = 0;

        // Loop untuk membuat grid
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 cardPosition = startPosition
                                     + mainCamera.transform.right * (col * xOffset)
                                     - mainCamera.transform.up * (row * yOffset);

                GameObject card = Instantiate(cardPrefab, cardPosition, Quaternion.Euler(cardRotation));
                card.AddComponent<Card>().Initialize(this, false); // Tambahkan komponen Card dan inisialisasi sebagai kartu grid
                spawnedCards[index++] = card;
            }
        }

        isGridVisible = true; // Set grid menjadi terlihat
    }

    // Fungsi untuk menghapus grid dan kembali ke kartu utama
    public void ReturnToMainCard()
    {
        if (!isGridVisible) return; // Cegah jika grid tidak terlihat

        foreach (var card in spawnedCards)
        {
            Destroy(card);
        }

        isGridVisible = false; // Set grid menjadi tidak terlihat
        SpawnMainCard(); // Tampilkan kembali kartu utama
    }
}
