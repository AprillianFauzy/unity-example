using UnityEngine;

public class Card : MonoBehaviour
{
    private CardGridSpawner spawner;
    private bool isMainCard; // Apakah ini kartu utama

    // Inisialisasi kartu
    public void Initialize(CardGridSpawner spawner, bool isMainCard)
    {
        this.spawner = spawner;
        this.isMainCard = isMainCard;
    }

    // Fungsi saat kartu diklik
    private void OnMouseDown()
    {
        if (isMainCard)
        {
            // Jika kartu utama diklik, spawn grid
            spawner.SpawnGrid();
            Destroy(gameObject); // Hapus kartu utama
        }
        else
        {
            // Jika kartu di grid diklik, kembali ke kartu utama
            spawner.ReturnToMainCard();
        }
    }
}
