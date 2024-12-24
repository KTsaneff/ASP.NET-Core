using LoopSocialApp.Data.DataModels;

namespace LoopSocialApp.Data.Utilities
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.ApplicationUsers.Any() && !context.Posts.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "d11679d1-9a2d-4b1d-a684-09e4aeb61b2c",
                        UserName = "nona_tsaneff",
                        FullName = "Nona Tsanev",
                        Email = "nonapetkova22@gmail.com",
                        ProfileImageUrl = "https://scontent.fsof9-1.fna.fbcdn.net/v/    t39.30808-6/425600003_7277740068952115_7247791707946506316_n.jpg?   _nc_cat=105&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=efZCc1gX5M8Q7kNvgHhkXX6&_nc_zt=23&_nc_ht=scont  ent.fsof9-1.fna&_nc_gid=ABT8JB8UMqNjC-GRLLJRVV0&oh=00_AYDEJhdBzaVY-   fGI9GNKXDFYcnFf9arb2Lv_UjD6LLf7ZQ&oe=67706B3D"
                    },
                    new ApplicationUser
                    {
                        Id = "44543460-578c-48cd-b818-795f30b2e051",
                        UserName = "mladenov_nik",
                        FullName = "Nikola Mladenov",
                        Email = "nikola_d_m@gmail.com",
                        ProfileImageUrl = "https://scontent.fsof9-1.fna.fbcdn.net/v/t39.30808-6/463314099_10220156067451944_5707979834145883031_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=6t0jCf4fS18Q7kNvgHxngTC&_nc_zt=23&_nc_ht=scontent.fsof9-1.fna&_nc_gid=AaIQpZPXewMXwl30nPmCdkI&oh=00_AYAmww8SsZLHUcnnqN1713PXZRbfCxxABB0PiF_q7z4bdA&oe=67706D4C"
                    },
                    new ApplicationUser
                    {
                        Id = "c6ff8035-76d7-42b7-a4c6-5386d1198b29",
                        UserName = "Lina_Petkova",
                        FullName = "Tsvetelina Petkova",
                        Email = "where_tsveti_fits@gmail.com",
                        ProfileImageUrl = "https://scontent.fsof9-1.fna.fbcdn.net/v/t39.30808-6/468636596_9604256032922180_8288667063593633149_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=jsB9KAmifq8Q7kNvgF53CKH&_nc_zt=23&_nc_ht=scontent.fsof9-1.fna&_nc_gid=A57odPUW0TUGMIWsHcvDDzf&oh=00_AYCryQW_L3Vnj5en7A_Iwnp5ue5O9W__TxVWaVcOJ0oE_A&oe=67707EF8"
                    },
                    new ApplicationUser
                    {
                        Id = "00c185e1-7e41-4a01-9643-28ed5c8233ba",
                        UserName = "kss_tff",
                        FullName = "Krasimir Tsanev",
                        Email = "krassytsaneff@gmail.com",
                        ProfileImageUrl = "https://scontent.fsof9-1.fna.fbcdn.net/v/t39.30808-6/471312953_10231834437782128_6505750327474522189_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=a5f93a&_nc_ohc=e7nP4_56Z4cQ7kNvgFaFad_&_nc_zt=23&_nc_ht=scontent.fsof9-1.fna&_nc_gid=AttXlcFYwSFwEdaXTKgYvM8&oh=00_AYBn-txxDnSP0Vfd__20oHGnzHDA-HVO6K4bJa9it7ElVg&oe=677069FB"
                    }

                };

                await context.ApplicationUsers.AddRangeAsync(users);
                await context.SaveChangesAsync();

                var posts = new List<Post>
                {
                    new Post
                    {
                        Content = "Отиваме към Малко Търново. Няма да чакам никой!",
                        ApplicationUserId = "44543460-578c-48cd-b818-795f30b2e051",
                        NumberOfReposrts = 2,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                    },
                    new Post
                    {
                        Content = "Next week in Viena! Who wants to join us?",
                        ApplicationUserId = "d11679d1-9a2d-4b1d-a684-09e4aeb61b2c",
                        ImageUrl = "https://blog.karat-s.com/wp-content/uploads/2016/10/Wienr.jpg",
                        NumberOfReposrts = 0,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                    },
                };

                await context.Posts.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }
        }
    }
}
