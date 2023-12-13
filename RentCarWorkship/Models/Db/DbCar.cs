namespace RentCarWorkship.Models.Db;

public class DbCar
{
    public int CarId { get; set; }
    public bool CanBeRented { get; set; }
    public string TransportType { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public string identtifier { get; set; }
    public string? description { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double? minutePrice { get; set; }
    public double? dayPrice { get; set; }
}
// GET /api/Transport/{id}
// описание: Получение информации о транспорте по id
// ограничения: нет
// POST /api/Transport
// описание: Добавление нового транспорта
// body:
// {
//     "canBeRented": bool, //Можно ли арендовать транспорт?
//     "transportType": "string", //Тип транспорта [Car, Bike, Scooter]
//     "model": "string", //Модель транспорта
//     "color": "string", //Цвет транспорта
//     "identifier": "string", //Номерной знак
//     "description": "string", //Описание (может быть null)
//     "latitude": double, //Географическая широта местонахождения транспорта
//     "longitude": double, //Географическая долгота местонахождения транспорта
//     "minutePrice": double, //Цена аренды за минуту (может быть null)
//     "dayPrice": double //Цена аренды за сутки (может быть null)
// }
// ограничения: Только авторизованные пользователи
// PUT /api/Transport/{id}
// описание: Изменение транспорта оп id
// body:
// {
//     "canBeRented": bool, //Можно ли арендовать транспорт?
//     "model": "string", //Модель транспорта
//     "color": "string", //Цвет транспорта
//     "identifier": "string", //Номерной знак
//     "description": "string", //Описание (может быть null)
//     "latitude": double, //Географическая широта местонахождения транспорта
//     "longitude": double, //Географическая долгота местонахождения транспорта
//     "minutePrice": double, //Цена аренды за минуту (может быть null)
//     "dayPrice": double //Цена аренды за сутки (может быть null)
// }
// ограничения: Только владелец этого транспорта
// DELETE /api/Transport/{id}
// описание: Удаление транспорта по id