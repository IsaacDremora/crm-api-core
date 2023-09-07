using Microsoft.EntityFrameworkCore;

namespace FoodPositions;

public class FoodPosition // класс позиций по еде, которая будет в приложении. 5 полей. Ничего такого
{
    public string? name { get; set;} //обязательная проверка на null. Иначе пизда. Во всех следующих API любые сущности должны проверяться на null у аттрибутов.
    public int id {get; set;}
    public double price {get; set;}
    public bool isAveliable {get; set;} = false;
    public string? shDesc {get; set;}

}
