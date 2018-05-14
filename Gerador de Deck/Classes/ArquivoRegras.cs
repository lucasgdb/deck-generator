namespace Gerador_de_Deck.Classes
{
    public static class ArquivoRegras
    {
        public static void Criar()
        {
            try
            {
                if (!System.IO.File.Exists(pathSCartas)) CriarArquivo();
                else if (System.IO.File.ReadAllLines(pathSCartas).Length < 84) ReCriar();
                else if (System.Array.IndexOf(System.IO.File.ReadAllLines(pathSCartas), string.Empty) != -1) ReCriar();
            }
            catch { }
        }

        public static void ReCriar()
        {
            try
            {
                if (System.IO.File.Exists(pathSCartas))
                    System.IO.File.SetAttributes(pathSCartas, System.IO.FileAttributes.Normal);
                System.IO.File.Delete(pathSCartas);
            }
            finally { CriarArquivo(); }
        }

        private static void CriarArquivo()
        {
            try
            {
                if (System.IO.File.Exists(pathSCartas))
                { System.IO.File.SetAttributes(pathSCartas, System.IO.FileAttributes.Normal); System.IO.File.Delete(pathSCartas); }
                System.IO.StreamWriter sw = new System.IO.StreamWriter(pathSCartas);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (byte i = 1; i < Programa._Deck.CartasInformacao.Length; i++)
                    sb.Append(string.Format("{0}|Permitido{1}", Programa._Deck.CartasInformacao[i].Split('\n')[0], i == Programa._Deck.CartasInformacao.Length - 1 ? string.Empty : System.Environment.NewLine));
                sw.Write(sb.ToString());
                sw.Close();
            }
            finally { System.IO.File.SetAttributes(pathSCartas, System.IO.FileAttributes.ReadOnly); }
        }

        public static string pathSCartas = string.Format("{0}\\Cartas.txt", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
        public static string pathDSalvos = string.Format("{0}\\Decks salvos.txt", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
        public static string pathMDecks = string.Format("{0}\\Melhores Decks.txt", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
        public static string pathBalanceamento = string.Format("{0}\\Balanceamento.txt", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
    }
}
