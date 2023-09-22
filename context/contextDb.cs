using FoodPositions;
using Microsoft.EntityFrameworkCore;

namespace ContextDb;

class FoodPositionDb : DbContext // класс контекста. Нужен, как инструкция для EF. Шото типа сеанса, за который сохраняются аттрибуты из класса выше.
{
    public FoodPositionDb(DbContextOptions options) : base(options) 
    {
        
    }
    public DbSet<FoodPosition> positions {get; set;} = null!;
}