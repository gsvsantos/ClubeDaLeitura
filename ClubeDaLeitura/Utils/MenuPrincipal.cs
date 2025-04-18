using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloReserva;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.Utils;

public class MenuPrincipal
{
    public string OpcaoPrincipal;
    private RepositorioAmigo RepositorioAmigo;
    private RepositorioCaixa RepositorioCaixa;
    private RepositorioEmprestimo RepositorioEmprestimo;
    private RepositorioMulta RepositorioMulta;
    private RepositorioReserva RepositorioReserva;
    private RepositorioRevista RepositorioRevista;

    public MenuPrincipal()
    {
        RepositorioAmigo = new RepositorioAmigo();
        RepositorioCaixa = new RepositorioCaixa();
        RepositorioEmprestimo = new RepositorioEmprestimo();
        RepositorioMulta = new RepositorioMulta();
        RepositorioReserva = new RepositorioReserva();
        RepositorioRevista = new RepositorioRevista();
    }
    public void ApresentarMenuPrincipal()
    {
        Console.Clear();
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");
        ColorirEscrita.ComQuebraLinha("Clube da Leitura");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        ColorirEscrita.ComQuebraLinha("1 >> Gerenciar Amigos");
        ColorirEscrita.ComQuebraLinha("2 >> Gerenciar Caixas");
        ColorirEscrita.ComQuebraLinha("3 >> Gerenciar Revistas");
        ColorirEscrita.ComQuebraLinha("4 >> Gerenciar Empréstimos");
        ColorirEscrita.ComQuebraLinha("5 >> Gerenciar Multas");
        ColorirEscrita.ComQuebraLinha("6 >> Gerenciar Reservas");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        OpcaoPrincipal = Console.ReadLine()!;
    }
    public TelaBase ObterTela()
    {
        if (OpcaoPrincipal == "1")
            return new TelaAmigo(RepositorioAmigo, RepositorioEmprestimo);

        else if (OpcaoPrincipal == "2")
            return new TelaCaixa(RepositorioCaixa);

        else if (OpcaoPrincipal == "3")
            return new TelaRevista(RepositorioCaixa, RepositorioRevista);

        else if (OpcaoPrincipal == "4")
            return new TelaEmprestimo(RepositorioAmigo, RepositorioEmprestimo, RepositorioMulta, RepositorioRevista);

        else if (OpcaoPrincipal == "5")
            return new TelaMulta(RepositorioAmigo, RepositorioEmprestimo, RepositorioMulta);

        else if (OpcaoPrincipal == "6")
            return new TelaReserva(RepositorioAmigo, RepositorioEmprestimo, RepositorioReserva, RepositorioRevista);

        else if (OpcaoPrincipal == "S")
            return null!;

        return null!;
    }
}
