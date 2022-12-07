using CabAPI.Controllers;
namespace CabAPI.Data
{
    public class CabManager
    {
        public List<Cab> Cabs { get; set; }

        public CabManager()
        {

            Cabs = new List<Cab>()
                {

                         new() { Id = 1, Type= "Cab 1", Model = "Model 1", Color = "red" },
                         new() { Id = 2, Type= "Cab 2", Model = "Model 2", Color = "Black" },
                         new() { Id = 3, Type= "Cab 3", Model = "Model 3", Color = "White" },
                         new() { Id = 4, Type= "Cab 4", Model = "Model 4", Color = "red" },
                         new() { Id = 5, Type= "Cab 5", Model = "Model 5", Color = "red" },
                         new() { Id = 6, Type= "Cab 6", Model = "Model 6", Color = "Gray" },
                         new() { Id = 7, Type= "Cab 7", Model = "Model 7", Color = "red" },
                         new() { Id = 8, Type= "Cab 8", Model = "Model 8", Color = "Brown" },
                         new() { Id = 9, Type= "Cab 9", Model = "Model 9", Color = "Blue" },
                         new() { Id = 10, Type = "Cab 10", Model = "Model 10", Color = "Green" },

                };
        }

            public Cab AddCab(Cab movie)
            {
                movie.Id = Cabs.Count + 1;
                Cabs.Add(movie);
                return movie;
            }

            public int DeleteCab(int id)
            {
                var movie = Cabs.FirstOrDefault(m => m.Id == id);
                if (movie == null)
                    return 0;
                Cabs.Remove(movie);
                return 1;
            }
        
    }
}
