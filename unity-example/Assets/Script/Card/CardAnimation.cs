using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public Vector3 offsetFromCamera = new Vector3(0, 0, 2); // Jarak offset kartu dari kamera (maju ke depan)
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f); // Skala target kartu
    public float animationDuration = 0.5f; // Durasi animasi

    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Vector3 initialScale;

    private bool isAnimating = false;
    private bool isAtTarget = false; // Menyimpan status apakah kartu sedang di posisi target
    private float animationProgress = 0f;

    void Start()
    {
        // Simpan posisi, rotasi, dan skala awal
        initialPosition = transform.localPosition;
        initialRotation = transform.localEulerAngles;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (isAnimating)
        {
            // Progres animasi
            animationProgress += Time.deltaTime / animationDuration;

            // Hitung posisi target agar selalu di depan kamera
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 targetPosition = cameraPosition + cameraForward * offsetFromCamera.z +
                                     Camera.main.transform.right * offsetFromCamera.x +
                                     Camera.main.transform.up * offsetFromCamera.y;

            // Buat rotasi dinamis agar selalu menghadap kamera
            Quaternion targetRotation = Quaternion.LookRotation(-cameraForward, Vector3.up); // Menghadap kamera

            // Tentukan posisi, rotasi, dan skala berdasarkan status apakah kartu di posisi target atau tidak
            if (isAtTarget)
            {
                // Animasi kembali ke posisi awal
                transform.localPosition = Vector3.Lerp(targetPosition, initialPosition, animationProgress);
                transform.localRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(initialRotation), animationProgress);
                transform.localScale = Vector3.Lerp(targetScale, initialScale, animationProgress);
            }
            else
            {
                // Animasi ke posisi target
                transform.position = Vector3.Lerp(initialPosition, targetPosition, animationProgress);
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(initialRotation), targetRotation, animationProgress);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, animationProgress);
            }

            // Hentikan animasi saat sudah selesai
            if (animationProgress >= 1f)
            {
                isAnimating = false;
                isAtTarget = !isAtTarget; // Tukar status posisi kartu
            }
        }
    }

    private void OnMouseDown()
    {
        // Mulai animasi saat kartu diklik
        if (!isAnimating)
        {
            isAnimating = true;
            animationProgress = 0f; // Reset progres animasi
        }
    }
}
