using System;
using System.Runtime.CompilerServices;
using FilmsBLL;
using FilmsDal;
using FilmsDTO;
using NUnit.Framework;

namespace TestBLL
{
    [TestFixture]
    public class testInsertDeleteComment
    {
        private static BllManager _bllManager = new BllManager();
        private static FullFilmDTO _starWars = _bllManager.GetFullFilmDetailsByIdFilm(11);
            private static CommentDTO _comment = new CommentDTO()
            {
                Content = "test de content pour le commentaire",
                Date = DateTime.Now,
                Film = _bllManager.GetFilmById(11),//star wars#1#,
                Rate = 19,
                Username = "jojo"
            };
        [Test]
        public void testInsertComment()
        {
            Console.WriteLine("insert comment...");
            _bllManager.InsertComment(_comment);
            Console.WriteLine("insert comment ok : affichage à partir de la lecture de la bd :");

            FullFilmDTO film = _bllManager.GetFullFilmDetailsByIdFilm(_comment.Film.Id);
            int idComment = 0;
            foreach (CommentDTO filmComment in film.Comments)
            {
                idComment = filmComment.Id;
            }
        
            Console.WriteLine("Film : \n" + film.ToStringAll());
        
            _comment = _bllManager.getCommentFromId(idComment);
            Console.WriteLine("Comment : \n" + _comment.ToStringWithFilm());

            Assert.Pass();
        }

        [Test]
        public void testDeleteComment()
        {
            Console.WriteLine("Delete comment...");
            
            //NE PAS OUBLIER DE SET L'ID !!!
            _comment.Id = 24;
            //--------------------------
            
            _bllManager.DeleteComment(_comment);
            Console.WriteLine("Apres Delete comment...");
        
            FullFilmDTO film = _bllManager.GetFullFilmDetailsByIdFilm(11);
            Console.WriteLine("Film : \n" + film.ToStringAll());

            CommentDTO commentFromBd = _bllManager.getCommentFromId(_comment.Id);
            if(commentFromBd == null) Console.WriteLine("commentaire bien supprimé");
            else Console.WriteLine("commentaire pas supprimé : " + commentFromBd);
            
            Assert.Pass();
        }
    }
}