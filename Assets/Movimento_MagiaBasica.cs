using UnityEngine;

public class Movimento_MagiaBasica : MonoBehaviour
{
    public Vector3 startPoint; // Ponto de partida
    public Vector3 endPoint; // Destino
    public Vector3 controlPoint; // Ponto de controle para a curva de Bezier
    public float speed = 20f; // Velocidade de movimento
    public Rigidbody rb; // Rigidbody do objeto
    public float curveHeight = 5f; // Altura da trajetória curva

    private bool atingiu = false;

    private float t = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Definindo o ponto de controle para a trajetória curva
        //controlPoint = (startPoint + endPoint) / 2 + Vector3.forward * curveHeight;
    }

    void FixedUpdate()
    {
        if (atingiu) 
        {
            return;
        }
        if (controlPoint == Vector3.zero) 
        {
            return;
        }
        // Calculando a nova posição ao longo da trajetória curva
        float step = speed * Time.fixedDeltaTime;
        Vector3 newPos = Vector3.Lerp(transform.position, endPoint, step);
        newPos.y = BezierCurve(startPoint.y, controlPoint.y, endPoint.y, (newPos.x - startPoint.x) / (endPoint.x - startPoint.x));

        // Atualizando a posição do objeto
        rb.MovePosition(newPos);

        // Se chegou perto o suficiente do ponto final, parar o movimento
        if (Vector3.Distance(transform.position, endPoint) < 0.1f)
        {
            rb.velocity = Vector3.zero;
        }
    }

    // Função para calcular a posição y ao longo da trajetória curva usando a fórmula de Bezier
    private float BezierCurve(float p0, float p1, float p2, float t)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }
}