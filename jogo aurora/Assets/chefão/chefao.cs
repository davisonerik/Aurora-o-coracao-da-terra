using UnityEngine;

public class chefao : MonoBehaviour
{
    [Header("Vida do Inimigo")]
    public int vida = 50;
    private int vidaMaxima;

    [Header("Referência da Barra de Vida")]
    public BossHealthBar barraDeVida;

    [Header("Movimentação")]
    public float velocidade = 2f;
    public float distanciaDePerseguir = 10f;  
    public float distanciaDeAtaque = 8f;      

    [Header("Ataque Especial (Chuva de Fogo)")]
    public GameObject fireRainSpawnerPrefab;
    public float tempoEntreAtaques = 3f;
    private float ultimoAtaque = 0f;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        vidaMaxima = vida;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Encontra o jogador automático
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogError("⚠ ERRO: Player não tem tag 'Player'");

        // Inicializa barra de vida
        if (barraDeVida != null)
            barraDeVida.Setup(this);
        else
            Debug.LogError("⚠ ERRO: A barra de vida do chefão não foi atribuída no Inspector!");
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia > distanciaDePerseguir)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("walk", false);
            return;
        }

        if (distancia <= distanciaDeAtaque)
        {
            Atacar();
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("walk", false);

            if (player.position.x > transform.position.x)
                transform.eulerAngles = Vector3.zero;
            else
                transform.eulerAngles = new Vector3(0, 180, 0);

            return;
        }

        Perseguir();
    }

    void Perseguir()
    {
        anim.SetBool("walk", true);

        Vector2 direcao = (player.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(direcao.x * velocidade, rb.linearVelocity.y);

        if (direcao.x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void Atacar()
    {
        if (Time.time < ultimoAtaque + tempoEntreAtaques) return;

        ultimoAtaque = Time.time;

        anim.SetTrigger("attack");

        Instantiate(fireRainSpawnerPrefab, player.position, Quaternion.identity);
    }

    public void TomarDano(int dano)
    {
        vida -= dano;

        // Atualiza barra
        if (barraDeVida != null)
            barraDeVida.AtualizarBarra(vida, vidaMaxima);

        if (vida <= 0)
            Destroy(gameObject);
    }
}