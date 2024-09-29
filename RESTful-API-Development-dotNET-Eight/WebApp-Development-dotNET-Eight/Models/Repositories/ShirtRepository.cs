namespace WebApp_Development_dotNET_Eight.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> _shirts = new List<Shirt>
        {
            new Shirt { ShirtId = 1, Brand = "Nike", Color = "Blue", Gender = "Male", Price = 20.00, Size = 10 },
            new Shirt { ShirtId = 2, Brand = "Adidas", Color = "Red", Gender = "Female", Price = 25.00, Size = 7 },
            new Shirt { ShirtId = 3, Brand = "Puma", Color = "Green", Gender = "Male", Price = 30.00, Size = 12 },
            new Shirt { ShirtId = 4, Brand = "Reebok", Color = "White", Gender = "Male", Price = 15.00, Size = 9 },
            new Shirt { ShirtId = 5, Brand = "Under Armour", Color = "Purple", Gender = "Female", Price = 22.00, Size = 6 }
        };

        public static List<Shirt> GetShirts()
        {
            return _shirts;
        }

        public static bool ShirtExists(int id)
        {
            return _shirts.Any(x => x.ShirtId == id);
        }

        public static Shirt? GetShirtById(int id)
        {
            return _shirts.FirstOrDefault(x => x.ShirtId == id);
        }

        public static void AddShirt(Shirt shirt)
        {
            shirt.ShirtId = _shirts.Max(s => s.ShirtId) + 1;
           _shirts.Add(shirt);
        }

        public static Shirt? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
        {
            return _shirts.FirstOrDefault(x => 
                !string.IsNullOrWhiteSpace(brand) &&
                !string.IsNullOrWhiteSpace(x.Brand) && 
                x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(gender) &&
                !string.IsNullOrWhiteSpace(x.Gender) &&
                x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(color) &&
                !string.IsNullOrWhiteSpace(x.Color) &&
                x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
                size.HasValue &&
                x.Size.HasValue &&
                size.Value == x.Size.Value);
        } 

        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = _shirts.FirstOrDefault(x => x.ShirtId == shirt.ShirtId);

            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
        }

        public static void DeleteShirt(int id)
        {
            var shirtToDelete = GetShirtById(id);
            if(shirtToDelete != null)
            {
                _shirts.Remove(shirtToDelete);
            }
        }
    }
}
