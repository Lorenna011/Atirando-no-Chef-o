using UnityEngine;
using TMPro;

public class BossHealth : MonoBehaviour
{
    [Header("Vida do Chefão")]
    public float vidaMaxima = 1000f;
    public float vidaAtual = 1000f;

    [Header("Texto da Vida")]
    public TMP_Text textoVida;

    private bool morreu = false;

    private void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarTexto();
    }

    public void TakeDamage(float dano)
    {
        if (morreu)
            return;

        vidaAtual -= dano;

        if (vidaAtual < 0)
            vidaAtual = 0;

        AtualizarTexto();

        Debug.Log("Vida do chefão: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void AtualizarTexto()
    {
        if (textoVida != null)
        {
            textoVida.text = "Chefão - Vida: " +
                             vidaAtual.ToString("0");
        }
    }

    private void Morrer()
    {
        morreu = true;

        Debug.Log("O CHEFÃO FOI DERROTADO!");

        gameObject.SetActive(false);
    }
}