namespace ClubeDaLeitura.Compartilhado;

public static class MenuPrincipal
{
    public static string ApresentarMenuPrincipal()
    {
        Console.Clear();
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");
        ColorirEscrita.ComQuebraLinha("Clube da Leitura");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        ColorirEscrita.ComQuebraLinha("1 >> Gerenciar Amigos");
        ColorirEscrita.ComQuebraLinha("2 >> Gerenciar Caixas");
        ColorirEscrita.ComQuebraLinha("3 >> Gerenciar Revistas");
        ColorirEscrita.ComQuebraLinha("4 >> Gerenciar Empréstimos");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
}
