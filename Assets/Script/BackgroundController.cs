using UnityEngine;
using UnityEngine.UIElements;


public class BackgroundController : MonoBehaviour
{
    // Переменная для хранения начальной позиции фона по оси X
    private float startPos;
    // Длина спрайта (ширина фона), чтобы знать, когда нужно его зациклить
    private float length;
    // Ссылка на объект камеры в сцене
    public GameObject cam;
    // Коэффициент параллакса, определяет скорость смещения фона относительно камеры
    public float parallaxEffect; // скорость
    // Дополнительное смещение для расширения области за пределы исходного фона
    public float extraOffset = 100f; // Можно установить в инспекторе или по умолчанию

    // Метод вызывается при инициализации сцены
    void Start()
    {
        // Запоминаем начальную позицию фона по оси X
        startPos = transform.position.x;
        // Получаем ширину спрайта фона из его компонента SpriteRenderer
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Метод вызывается каждый кадр
    void FixedUpdate()
    {
        // Вычисляем смещение фона с учетом эффекта параллакса
        float distance = cam.transform.position.x * parallaxEffect;
        // Расчет движения фона с учетом позиций камеры и эффекта
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        // Обновляем позицию фона по оси X для создания эффекта параллакса
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Расширяем область генерации фона по оси X
        float extendedLength = length + extraOffset;

        // Если камера сдвинулась достаточно далеко, чтобы перейти за пределы текущего фона
        if (movement > startPos + extendedLength)
        {
            // Сдвигаем начальную позицию фона вперед на его длину для зацикливания
            startPos += length;
        }
        // Если камера сдвинулась назад достаточно далеко
        else if (movement < startPos - extendedLength)
        {
            // Сдвигаем начальную позицию назад на длину фона для зацикливания
            startPos -= length;
        }
    }
}