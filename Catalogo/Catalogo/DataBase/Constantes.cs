using System;
using System.IO;

namespace Catalogo.DataBase
{
    public static class Constants
    {
        public const string NomeDoArquivo = "dbCatalogo.db3";

        public static string CaminhoDoBanco
        {
            get
            {
                var caminhoBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(caminhoBase, NomeDoArquivo);
            }
        }
    }
}

