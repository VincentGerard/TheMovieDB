using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace LibraryProperties
{
    public class Properties
    {
        public static string getDatabaseFullPath()
        {
            var parent = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (parent == null) throw new Exception("null pointer exception [getDatabaseFullPath]");
            var directoryInfo = parent.Parent;
            if (directoryInfo == null) throw new Exception("null pointer exception [getDatabaseFullPath]");
            if (directoryInfo.Parent != null)
                return Path.Combine(directoryInfo.Parent.FullName,
                    getDatabaseFileName());
            throw new Exception("null pointer exception [getDatabaseFullPath]");
        }

        public static string getDatabaseFileName()
        {
            return "filmsSqlite.db";
        }
        
        public static string getMoviesTxtFullPath()
        {
            var parent = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (parent == null) throw new Exception("null pointer exception [getMoviesTxtFullPath]");
            var directoryInfo = parent.Parent;
            if (directoryInfo == null) throw new Exception("null pointer exception [getMoviesTxtFullPath]");
            if (directoryInfo.Parent != null)
                return Path.Combine(directoryInfo.Parent.FullName,
                    getMoviesTxtFileName());
            throw new Exception("null pointer exception [getMoviesTxtFullPath]");
        }
        
        public static string getMoviesTxtFileName()
        {
            return "movies_v2.txt";
        }

        //clé sécurisé api
    }
}