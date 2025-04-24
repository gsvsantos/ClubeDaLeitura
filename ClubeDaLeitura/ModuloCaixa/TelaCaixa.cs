using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloCaixa;

public class TelaCaixa : TelaBase<Caixa>, ITelaCrud
{
    public RepositorioCaixa RepositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa) : base("Caixa", repositorioCaixa)
    {
        RepositorioCaixa = repositorioCaixa;
    }

    public override bool TemRestricoesNoInserir(Caixa novaCaixa, out string mensagem)
    {
        mensagem = "";

        if (RepositorioCaixa.VerificarEtiquetasNovoRegistro(novaCaixa))
        {
            mensagem = "\nJá existe uma caixa com essa etiqueta!";
            return true;
        }

        return false;
    }
    public override bool TemRestricoesNoEditar(Caixa caixaEscolhida, Caixa dadosEditados, out string mensagem)
    {
        mensagem = "";

        if (RepositorioCaixa.VerificarEtiquetasEditarRegistro(caixaEscolhida, dadosEditados))
        {
            mensagem = "\nJá existe uma caixa com essa etiqueta!";
            return true;
        }

        return false;
    }
    public override bool TemRestricoesNoExcluir(Caixa caixaEscolhida, out string mensagem)
    {
        mensagem = "";

        if (caixaEscolhida.VerificarRevistasCaixa())
        {
            mensagem = $"\nA caixa {caixaEscolhida.Etiqueta} ainda possui revistas e não pode ser excluída.";
            return true;
        }

        return false;
    }
    public override void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Caixas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Etiqueta", "Dias de Empréstimo", "Revistas na Caixa"];
            int[] espacamentos = [6, 20, 20, 20,];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Etiqueta", "Dias de Empréstimo", "Revistas na Caixa"];
            int[] espacamentos = [20, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        List<Caixa> registros = RepositorioCaixa.PegarListaRegistrados();

        int quantidadeCaixas = 0;

        foreach (var c in registros)
        {
            quantidadeCaixas++;
            RepositorioCaixa.ListaVazia = false;

            if (comId)
            {
                string[] linha = [c.Id.ToString(), c.Etiqueta, c.DiasEmprestimo.ToString(), c.Revistas.Count.ToString()];
                int[] espacamentos = [6, 20, 20, 20,];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, (ConsoleColor)c.Cor, ConsoleColor.Blue, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresCabecalho);
            }
            else
            {
                string[] linha = [c.Etiqueta, c.DiasEmprestimo.ToString(), c.Revistas.Count.ToString()];
                int[] espacamentos = [20, 20, 20];
                ConsoleColor[] coresCabecalho = [(ConsoleColor)c.Cor, ConsoleColor.Blue, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresCabecalho);
            }
        }

        if (quantidadeCaixas == 0)
        {
            Notificador.ExibirMensagem("\nNenhuma caixa registrada!", ConsoleColor.Red);
            RepositorioCaixa.ListaVazia = true;
        }
    }
    public override Caixa ObterDados()
    {
        ColorirEscrita.SemQuebraLinha("Digite o Nome da Etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine()!;

        int cor = PegarCorPaletaCores();

        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Registrando Caixa...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        bool numeroValido;
        int diasEmprestimo;

        do
        {
            ColorirEscrita.SemQuebraLinha("Digite o número de Dias de Empréstimo das Revistas nesta Caixa (3 para revistas comuns e 7 para revistas raras): ");
            numeroValido = int.TryParse(Console.ReadLine(), out diasEmprestimo);

            if (!numeroValido)
                Notificador.ExibirMensagem("\nEsse não é um número válido!", ConsoleColor.Red);
        } while (!numeroValido);

        Caixa caixa = new Caixa(etiqueta, cor, diasEmprestimo);

        return caixa;
    }
    public int PegarCorPaletaCores()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Paleta de Cores");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        for (int i = 0; i <= 15; i++)
        {
            Console.ForegroundColor = (ConsoleColor)i;
            Console.WriteLine($"{i} >> {(ConsoleColor)i}");
        }
        Console.ResetColor();

        bool idValido;
        int cor;

        do
        {
            ColorirEscrita.ComQuebraLinha("--------------------------------------------");

            ColorirEscrita.SemQuebraLinha("Selecione uma cor da paleta: ");
            idValido = int.TryParse(Console.ReadLine(), out cor);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nEssa não é uma opção da paleta de cores.", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                PegarCorPaletaCores();
                break;
            }
            break;
        } while (!idValido);

        return cor;
    }
}
