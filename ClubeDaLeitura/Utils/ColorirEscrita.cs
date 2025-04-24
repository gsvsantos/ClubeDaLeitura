namespace ClubeDaLeitura.Utils;

public static class ColorirEscrita
{
    public static void SemQuebraLinha(string mensagem, ConsoleColor cor = ConsoleColor.White)
    {
        Console.ForegroundColor = cor;
        Console.Write(mensagem);
        Console.ResetColor();
    }
    public static void ComQuebraLinha(string mensagem, ConsoleColor cor = ConsoleColor.White)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
    public static void PintarCabecalho(string[] cabecalho, int[] espacamentos, ConsoleColor[] cores)
    {
        for (int i = 0; i < cabecalho.Length; i++)
        {
            Console.ForegroundColor = cores[i];
            Console.Write($"| {{0,-{espacamentos[i]}}}", cabecalho[i]);
            Console.Write("| ");
        }

        Console.ResetColor();
        Console.WriteLine();
    }
    public static void PintarLinha(string[] linha, int[] espacamentos, ConsoleColor[] cores)
    {
        for (int i = 0; i < linha.Length; i++)
        {
            Console.ForegroundColor = cores[i];
            Console.Write($"| {{0,-{espacamentos[i]}}}", linha[i]);
            Console.Write("| ");
        }

        Console.ResetColor();
        Console.WriteLine();
    }
}
