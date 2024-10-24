namespace API_HangFire
{
    public static class Acao
    {
        public static void RegistrarMensagem(string tipo)
        {
            System.IO.File.AppendAllText(@"D:\Anderson\Estudos\C# Hangfire\Texto\loghang.txt", Environment.NewLine + $"EXECUTOU {tipo} {DateTime.Now}");
        }
    }
}
