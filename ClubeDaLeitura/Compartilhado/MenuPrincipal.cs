namespace ClubeDaLeitura.Compartilhado;

public static class MenuPrincipal
{
    public static string ApresentarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Clube da Leitura");
        Console.WriteLine("--------------------------------------------\n");

        Console.WriteLine("1 >> Gerenciar Amigos");
        Console.WriteLine("2 >> Gerenciar Caixas");
        Console.WriteLine("3 >> Gerenciar Revistas");
        Console.WriteLine("4 >> Gerenciar Empréstimos");
        Console.WriteLine("S >> Voltar");

        Console.Write("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
}
