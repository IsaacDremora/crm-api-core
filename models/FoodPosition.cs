namespace FoodPositions;

public class FoodPosition // класс позиций по еде, которая будет в приложении. 5 полей. Ничего такого
{
    public string? name { get; set;} //у полей обязательная проверка на null. Позже объясню зачем
    public int id {get; set;}
    public double price {get; set;}
    public bool isAveliable {get; set;} = false;
    public string? shDesc {get; set;}
}
