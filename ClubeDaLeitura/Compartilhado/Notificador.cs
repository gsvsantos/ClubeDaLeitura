namespace ClubeDaLeitura.Compartilhado;

public static class Notificador
{
    public static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.Write(mensagem);
        Console.ResetColor();
    }
}
