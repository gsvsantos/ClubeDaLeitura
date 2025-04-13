namespace ClubeDaLeitura.Compartilhado;

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
}
