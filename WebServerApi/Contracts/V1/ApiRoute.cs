namespace WebServerApi.Contracts.V1
{
    public static class ApiRoute
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public const string Swagger = "http://localhost:5000/swagger/index.html";
        
        public static class Films
        {
            public const string FilmBase = Base + "/films";
            public const string GetFilmsFromTo = "from={from}/to={to}";
            public const string GetNumberFilmsFromTo = "count";
            public const string FindListFilmByPartialActorName = "nomacteur={nomActeur}/from={from}/to={to}";
            public const string GetNumberFilmByPartialActorName = "nomacteur={nomActeur}";
            public const string FindFilmsByPartialTitle = "titre={titre}/from={from}/to={to}";
            public const string GetNumberFilmsByPartialTitle = "titre={titre}";
            public const string GetFullFilmDetailsByIdFilm = "fullfilmid={idFilm}";
            public const string GetFilmById = "filmid={idFilm}";
            
        }
        
        public static class Actors
        {
            public const string ActorBase = Base + "/actors";
            public const string getActorsFromTo = "from={from}/to={to}";
            public const string getNumberActorsFromTo = "count";
            public const string GetListActorsByIdFilm = "filmid={idFilm}/from={from}/to={to}";
            public const string GetNumberActorsByIdFilm = "filmid={idFilm}";
            public const string GetFavoriteActors = "favorite/from={from}/to={to}";
            public const string GetNumberFavoriteActors = "favorite";
            public const string GetActorById = "id={id}";
        }
        
        public static class Genres
        {
            public const string GenreBase = Base + "/genres";
            public const string GetGenresFromTo= "from={from}/to={to}";
            public const string GetNumberGenresFromTo = "count";
            public const string GetListGenreByIdFilm = "filmid={idFilm}";
        }
        
        public static class Comments
        {
            public const string CommentBase = Base + "/comments";
            public const string GetCommentsFromTo = "from={from}/to={to}";
            public const string GetNumberCommentsFromTo = "count";
            public const string GetCommentFromId = "commentid={id}";
    
            public const string InsertComment = "insert";
            public const string DeleteComment = "delete={id}";
        }
    }
}