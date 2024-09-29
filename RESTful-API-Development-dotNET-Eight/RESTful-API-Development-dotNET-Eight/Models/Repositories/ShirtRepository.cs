namespace RESTful_API_Development_dotNET_Eight.Models.Repositories
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

        public static bool ShirtExists(int id)
        {
            return _shirts.Any(x => x.ShirtId == id);
        }

        public static Shirt? GetShirtById(int id)
        {
            return _shirts.FirstOrDefault(x => x.ShirtId == id);
        }
    }
}
