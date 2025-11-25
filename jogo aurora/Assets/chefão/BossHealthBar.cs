using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image fillBar; // a imagem verde da barra

    private chefao boss;

    public void Setup(chefao bossRef)
    {
        boss = bossRef;
        AtualizarBarra(boss.vida, boss.vida);
    }

    public void AtualizarBarra(int vidaAtual, int vidaMaxima)
    {
        float fill = (float)vidaAtual / vidaMaxima;
        fillBar.fillAmount = fill;
    }
}