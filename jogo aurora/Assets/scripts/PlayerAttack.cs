using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;
    public float velocidadeDoTiro = 10f;
    public float tempoEntreTiros = 0.8f;

    [Header("Configurações Opcionais")]
    public float tempoDestruicaoProjetil = 3f;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip somAtaque;

    private Animator anim;
    private float ultimoTiro;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > ultimoTiro + tempoEntreTiros)
        {
            ultimoTiro = Time.time;
            Atacar();
        }
    }

    void Atacar()
    {
        // 🔊 Som de ataque
        audioSource.PlayOneShot(somAtaque);

        // Animação
        if (anim != null)
        {
            anim.SetBool("IsAttacking", true);
            Invoke(nameof(ResetarAnimacao), 0.4f);
        }

        // Instancia o projetil
        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.identity);

        float direcao = (transform.eulerAngles.y == 180f) ? -1f : 1f;

        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = new Vector2(direcao * velocidadeDoTiro, 0f);

        Vector3 escala = projetil.transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        projetil.transform.localScale = escala;

        Destroy(projetil, tempoDestruicaoProjetil);
    }

    void ResetarAnimacao()
    {
        if (anim != null)
            anim.SetBool("IsAttacking", false);
    }
}