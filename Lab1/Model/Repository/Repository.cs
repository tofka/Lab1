﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Model;
using Lab1.Model.Abstract;

namespace Lab1.Model
{
    /// <summary>
    /// Klass som hanterar data för applikationen
    /// </summary>
    public class Repository : IRepository
    {        
        // privata listor med User- och Post-objekt
        private List<User> users;
        private List<Post> posts;

        /// <summary>
        /// Konstruktor som genererar 36 dummy Users och 100 dummy Posts
        /// </summary>
        public Repository()
        {
            // Här inne genererar vi dummy-data. För denna lab behöver du inte veta hur denna data genereras.
            List<Guid> UserIDs;
            users = GenerateUsers(out UserIDs); // För nyckelordet 'out' se kommentar innan definitionen av GenerateUsers
            posts = GeneratePosts(UserIDs);
        }

        /// <summary>
        /// Returnerar en lista på alla User-objekt i repositoriet
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return users;
        }

        /// <summary>
        /// Lägger till en User i repositoriet
        /// </summary>
        /// <param name="newUser">Det User-objekt som skall läggas till</param>
        public void AddUser(User newUser)
        {
            if (newUser.UserID == null)
                throw new Exception("UserID is not allowed to be null!");
            if (string.IsNullOrEmpty(newUser.UserName))
                throw new Exception("UserName is not allowed to be empty!");
            if (users.Any(u => u.UserID == newUser.UserID))
                throw new Exception("A User with that UserID already Exists!");
            users.Add(newUser);
        }

        /// <summary>
        /// Tar bort en befintlig User från Repositoriet
        /// </summary>
        /// <param name="userID">UserID för User-objektet som skall tas bort</param>
        public void RemoveUser(Guid userID)
        {
            if (!users.Any(u => u.UserID == userID))
                throw new Exception("User with this UserID does not exist!");
            User userToBeRemoved = users.Where(u => u.UserID == userID).FirstOrDefault();
            users.Remove(userToBeRemoved);
        }

        /// <summary>
        /// Returnerar en lista över alla Post-objekt i Repositoriet
        /// </summary>
        /// <returns></returns>
        public List<Post> GetPosts()
        {
            return posts;
        }

        /// <summary>
        /// Läger till ett Post-objekt i repositoriet
        /// </summary>
        /// <param name="newPost">Det Post-objekt som skall läggas till</param>
        public void AddPost(Post newPost)
        {
            if (newPost.PostID == null)
                throw new Exception("PostID is not allowed to be null!");
            if (newPost.CreatedByID == null)
                throw new Exception("CreatedByID is not allowed to be null!");
            if (!users.Any(u => u.UserID == newPost.CreatedByID))
                throw new Exception("CreatedByID does not match any user!");
            if (string.IsNullOrEmpty(newPost.Body))
                throw new Exception("Body is not allowed to be empty!");
            if (newPost.CreateDate == null || newPost.CreateDate == new DateTime())
                throw new Exception("CreateDate must be set");
            if (posts.Any(p => p.PostID == newPost.PostID))
                throw new Exception("A Post with that PostID already Exists!");
            posts.Add(newPost);
        }

        /// <summary>
        /// Tar bort ett Post-objekt från Repositoriet
        /// </summary>
        /// <param name="postID">PostID för det Post-objekt som skall tas bort</param>
        public void RemovePost(Guid postID)
        {
            if (!posts.Any(p => p.PostID == postID))
                throw new Exception("Post with this PostID does not exist!");
            Post postToBeRemoved = posts.Where(p => p.PostID == postID).FirstOrDefault();
            posts.Remove(postToBeRemoved);
        }

        // Nedan följer hjälpmetoder som genererar dummy-data för posts och users i konstruktorn ovan.
        //
        // OBS! Du behöver inte bry dig om hur dessa är implementerade för denna lab.

        // #region gör att allt fram till #endregion kan minimeras till en rad i Visual Studio.
        // Detta nyckelord påverkar inte funktionen hos koden.
        #region Private Helper Methods

        /* OBS! Dessa hjälp-metoder är inte nödvändigtvis "Snygga" implementationer.
         * 
         * T.ex. så är de inte särskilt dynamiska (De genererar alltid 36 användare och 100 poster t.ex).
         * Men pga att allting är utbrytet i små funktioner som alla har ett ansvarsområde, så skulle
         * vi snabbt och lätt kunna köra refactoring på delar av dem för att göra dem effektivare eller mer dynamiska
         * 
         * Ifall ni vill experimentera lite så skulle ni t.ex. kunna skriva om funktionerna så att de
         * genererar ett valfritt antal användare eller poster, etc.
         */

        /* Lägg märke till att parametern UserIDs föregås av nyckelordet 'out'. Detta innebär att det är en ut-parameter
         * 
         * Detta innebär att när jag tilldelar något till UserIDs i denna funktion så
         * kommer det tilldelade värdet vara tillgängligt utanför funktionen
         * 
         * I det här fallet använder jag 'out' för att jag vill initiera UserIDs i denna metoden och sedan
         * skicka med den initierade-listan till GeneratePosts för att kunna sätta giltiga UserIDs på
         * de Posts jag skapar där. 
         * 
         * Ett alternativ till detta i just det här fallet hade varit att utifrån den returnerade listan
         * av Users skapa en ny lista bestående av enbart UserIDs. (Detta skulle kunna göras med en Select
         * i ett Linq-uttryck).
         * 
         * Ifall ni vill veta mer: sök på nyckelordet 'out' eller 'pass by reference'/'call by reference'
         */
        private List<User> GenerateUsers(out List<Guid> UserIDs)
        {
            int numberOfUsers = 36;
            UserIDs = new List<Guid>();
            for (int i = 0; i < numberOfUsers; i++)
                UserIDs.Add(Guid.NewGuid());

            List<string> firstNames = GenerateFirstNames();
            List<string> lastNames = GenerateLastNames();
            List<string> userNames = GenerateUserNames();

            users = new List<User>();
            for (int i = 0; i < numberOfUsers; i++)
            {
                User.UserType type = i % 10 == 0 ? (i % 20 == 0 ? User.UserType.SuperUser : User.UserType.Admin) : User.UserType.User;
                users.Add(new User { UserID = UserIDs[i], UserName = userNames[i], FirstName = firstNames[i], LastName = lastNames[i], Type = type });
            };
            return users;
        }

        private static List<string> GenerateUserNames()
        {
            List<string> userNames = new List<string>() {
                "Ducky", "DuckRoll", "Crimson", "cheese", "BaByBlue", "DeDe",
                "TroubleT", "Shorty", "PunkerChick", "jaycee", "jc", "icebox",
                "kika", "Wokie", "DeVeL", "spike", "frooooo", "blazer",
                "rza", "lell", "LilLi", "Sheba", "varcity", "sanaz",
                "IceClimber", "Latina", "pengu", "Aztech", "DarkArcher", "DArknEt",
                "WinterMute", "Ying", "Yang", "Angel",  "juror", "N-e-a-k"
            };
            return userNames;
        }

        private static List<string> GenerateLastNames()
        {
            List<string> lastNames = new List<string>() {
                "", "Smith", "Davis", "Hill", "Cook", "Scott", "Taylor", "",
                "Adams", "Perez", "Bell", "Martin", "Ortiz", "Reed", "",
                "Garza", "Kelly", "Price", "Ross", "Knight", "Diaz", "",
                "Bailey", "Boyd", "Ramos", "Payne", "Ray", "Ruiz", "",
                "Flores", "Sutton", "Kim", "Soto", "Reid", "Lowe", "",
                "Salazar", "Cohen", "Ingram", "Cobb", "Gibbs", "Stokes", ""
            };
            return lastNames;
        }

        private static List<string> GenerateFirstNames()
        {
            List<string> firstNames = new List<string>() {
                "", "Andy", "Al", "Alan", "Bert", "Bill", "Bo", "",
                "Carl", "Chad", "Dale", "Ken", "Elmo", "Erik", "",
                "Ezra", "Fred", "Gino", "Glen", "Hai", "Omar", "",
                "Ada", "Aida", "Alex", "Beth", "Bibi", "Cara", "",
                "Deb", "Dee", "Elin", "Elke", "Gail", "Hedy", "",
                "Jodi", "Katy", "Lana", "Mimi", "Ria", "Su", ""
            };
            return firstNames;
        }

        private static List<Post> GeneratePosts(List<Guid> userIDs)
        {
            List<string> postTitles = GeneratePostTitles();

            List<string> postBodies = GeneratePostBodies();

            int numberOfPosts = 100;
            List<Post> posts = new List<Post>();
            Random rng = new Random();
            for (int i = 0; i < numberOfPosts; i++)
                posts.Add(GenerateSinglePost(rng, postTitles, postBodies, userIDs));

            return posts;
        }

        private static List<string> GeneratePostBodies()
        {
            List<string> postBodies = new List<string> {
                "Lorem ipsum dolor sit amet",
                ", consectetur adipiscing elit. ",
                "Nulla nec justo nec felis ",
                "consequat sollicitudin. Nulla facilisi",
                ". Nulla mattis vestibulum ligula ",
                "quis dapibus. Sed consectetur ",
                "vehicula velit, in vulputate ",
                "erat pellentesque eu. Duis ",
                "facilisis libero id dolor sagittis ",
                "consequat. Cras lacinia pellentesque ",
                "venenatis. Duis dictum condimentum ",
                "feugiat. Vivamus dignissim imperdiet ",
                "tellus, at elementum justo ",
                "aliquam aliquet. Etiam placerat",
                ", libero ac egestas mollis",
                ", dolor libero convallis ante",
                ", id vestibulum lorem metus ",
                "id ante. Vivamus non ",
                "libero at orci dictum mollis",
                ". Sed auctor vulputate tortor ",
                "sed auctor. In auctor ",
                "adipiscing urna, non iaculis ",
                "metus ultrices ac. Donec ",
                "massa ligula, interdum et ",
                "laoreet ut, viverra sit ",
                "amet dui. Maecenas purus ",
                "tortor, suscipit vel elementum ",
                "sed, viverra eget dolor",
                ". Cum sociis natoque penatibus ",
                "et magnis dis parturient montes",
                ", nascetur ridiculus mus. ",
                "Phasellus tempus, lectus ut ",
                "rutrum luctus, ipsum purus ",
                "molestie libero, vitae eleifend ",
                "arcu turpis ac neque. ",
                "Morbi in felis quis tortor ",
                "semper rhoncus vel in urna",
                ". Curabitur tincidunt ligula et ",
                "est interdum quis feugiat elit ",
                "faucibus. Mauris nec ante ",
                "massa, eu rhoncus lacus",
                ". Aliquam in dui turpis",
                ". Duis ultrices suscipit adipiscing",
                ". Donec elementum urna a ",
                "lectus pharetra varius. Nam ",
                "viverra tellus sed neque luctus ",
                "porta. Nam aliquet pellentesque ",
                "ante quis ultricies. Nullam ",
                "blandit pretium tellus eu molestie",
                ". Duis tincidunt sollicitudin mi",
                ", nec vestibulum velit porttitor ",
                "et. Quisque porttitor, ",
                "quam vel eleifend tempus, ",
                "felis mi vehicula turpis, ",
                "vel blandit ipsum nulla ac ",
                "felis. Nunc mauris sem",
                ", ornare ut feugiat vitae",
                ", varius ut risus. ",
                "Sed quam ante, condimentum ",
                "eu eleifend at, placerat ",
                "eu lorem. Morbi accumsan",
                ", nisl nec pretium bibendum",
                ", dui nunc tincidunt dui",
                ", sed scelerisque eros ante ",
                "eu diam. Sed cursus ",
                "ultricies facilisis. Aliquam mattis ",
                "condimentum neque sit amet lacinia",
                ". Mauris a odio bibendum ",
                "sapien porttitor tempus in ut ",
                "risus. Sed tempor, ",
                "tortor vitae pellentesque vulputate, ",
                "augue odio bibendum lectus, ",
                "in malesuada felis arcu eu ",
                "nunc. Phasellus ac diam ",
                "tortor. Nam sed varius ",
                "diam. Duis nec nisi ",
                "et augue ultrices auctor vitae ",
                "id dui. Aenean ultricies ",
                "accumsan est, et ultricies ",
                "turpis malesuada vel. Duis ",
                "adipiscing lobortis massa, quis ",
                "malesuada risus rhoncus vel. ",
                "Donec consequat vestibulum est nec ",
                "imperdiet. Vestibulum ante ipsum ",
                "primis in faucibus orci luctus ",
                "et ultrices posuere cubilia Curae",
                "; Nunc eu dui at ",
                "magna viverra dapibus id vel ",
                "ipsum. Praesent venenatis pulvinar ",
                "nulla in tristique. Sed ",
                "a mi ac dolor consequat ",
                "tincidunt nec sit amet lectus",
                ". Suspendisse ut placerat augue",
                ". Vestibulum ante ipsum primis ",
                "in faucibus orci luctus et ",
                "ultrices posuere cubilia Curae; ",
                "Nunc bibendum lacus eu leo ",
                "imperdiet euismod. Sed facilisis",
                ", dolor id dignissim adipiscing",
                ", nibh orci vehicula justo"
            };
            return postBodies;
        }

        private static List<string> GeneratePostTitles()
        {
            List<string> postTitles = new List<string> {
                "Lorem", "ipsum", "dolor", "sit", "ame", "consectetur", "adipiscing", "eli",
                "Nulla", "ut", "massa", "vel", "justo", "elementum", "auctor", "in", "fermentum", 
                "dia", "In", "iaculi", "risus", "eget", "dapibus", "tempo", "lorem", "massa", 
                "molestie", "turpi", "ut", "iaculis", "orci", "dolor", "et", "sapie", "Etiam", 
                "a", "nibh", "blandit", "diam", "vehicula", "ornare", "eu", "vitae", "ligul",
                "Vestibulum", "mauris", "nis", "pretium", "facilisis", "u", "pellentesque", "a", 
                "turpi", "Donec", "convallis", "sapien", "ut", "lectus", "adipiscing", "vitae", 
                "malesuada", "risus", "cursu", "Ut", "vitae", "dui", "dolo", "Mauris", "adipiscing", 
                "nibh", "tincidunt", "nibh", "tempus", "quis", "sagittis", "sapien", "sodale",
                "Nulla", "facilis", "Cras", "venenatis", "fringilla", "nibh", "ac", "malesuad",
                "Integer", "pellentesque", "blandit", "rhoncu", "Proin", "iaculis", "rutrum", "era",
                "vel", "tempor", "leo", "vestibulum", "i", "Aliqua", "Lorem"
            };
            return postTitles;
        }

        // Genererar en Post med slumpade fält.
        private static Post GenerateSinglePost(Random rng, List<string> postTitles, List<string> postBodies, List<Guid> UserIDs)
        {
            int userIndex = rng.Next(UserIDs.Count - 1);
            int postTitleIndex = rng.Next(postTitles.Count - 1);
            int postBodyIndex = rng.Next(postBodies.Count - 1);
            int dayOffset = -1 * rng.Next(60);
            int hourOffset = -1 * rng.Next(23);
            int minuteOffset = -1 * rng.Next(59);
            List<Post.PostTags> tagList = GenerateTagList(rng);
            Post newPost = new Post { CreatedByID = UserIDs[userIndex], Title = postTitles[postTitleIndex], Body = postBodies[postBodyIndex], CreateDate = DateTime.Now.AddDays(dayOffset).AddHours(hourOffset).AddMinutes(minuteOffset), Tags = tagList };
            return newPost;
        }

        private static List<Post.PostTags> GenerateTagList(Random rng)
        {
            int tagIndex;
            List<Post.PostTags> tagList = new List<Post.PostTags>();
            for (int j = 0; j < 4; j++)
            {
                tagIndex = rng.Next(6);
                switch (tagIndex)
                {
                    case 1:
                        if (!tagList.Contains(Post.PostTags.Funny))
                            tagList.Add(Post.PostTags.Funny);
                        break;
                    case 2:
                        if (!tagList.Contains(Post.PostTags.Interesting))
                            tagList.Add(Post.PostTags.Interesting);
                        break;
                    case 3:
                        if (!tagList.Contains(Post.PostTags.JawDropping))
                            tagList.Add(Post.PostTags.JawDropping);
                        break;
                    case 4:
                        if (!tagList.Contains(Post.PostTags.Troll))
                            tagList.Add(Post.PostTags.Troll);
                        break;
                    case 5:
                        if (!tagList.Contains(Post.PostTags.Useful))
                            tagList.Add(Post.PostTags.Useful);
                        break;
                    default:
                        // Add nothing - We want a variable number of tags
                        break;
                }
            }

            return tagList;
        }

        #endregion
    }
}