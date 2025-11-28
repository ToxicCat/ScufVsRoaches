using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points; //патуркль через массив медлу двух точек
    public float speed = 2f;
    public int damageAmount = 1;

    // Индекс следующей точки назначения
    private int destPoint = 0;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        if (points.Length > 0) // Проверяем, заданы ли точки
        {

            transform.position = points[destPoint].position; // Устанавливаем начальную точку
            GoToNextPoint();
        }
    }

    void Update()
    {
        
        // Если враг достиг текущей точки назначения
        if (Vector2.Distance(transform.position, points[destPoint].position) < 0.5f)
        {
            GoToNextPoint();
        }

        // Двигаем врага к текущей точке назначения
        // Используем MoveTowards для простоты
        transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, speed * Time.deltaTime);

        // Дополнительно: поворот спрайта в зависимости от направления движения
        Vector3 flip = transform.localScale;
        if (transform.position.x < points[destPoint].position.x)
        {
            flip.x = Mathf.Abs(flip.x); // Смотрим вправо
        }
        else
        {
            flip.x = -Mathf.Abs(flip.x); // Смотрим влево
        }
        transform.localScale = flip;
    }





    void GoToNextPoint()
    {
        // Если точек нет, выходим
        if (points.Length == 0)
            return;

        // Выбираем следующую точку назначения по циклу (0, 1, 0, 1...)
        destPoint = (destPoint + 1) % points.Length;
    }

    // Отрисовка линий и сфер для отладки в редакторе Unity (необязательно)
    void OnDrawGizmos()
    {
        if (points.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i].position, 0.5f);
                if (i + 1 < points.Length)
                {
                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
                }
                else
                {
                    Gizmos.DrawLine(points[i].position, points[0].position); // Замыкаем маршрут
                }
            }
        }
    }
}