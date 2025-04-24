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
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Registrando Caixa...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        Caixa novaCaixa = (Caixa)ObterDados();

        string erros = novaCaixa.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        if (RepositorioCaixa.VerificarEtiquetasNovoRegistro(novaCaixa))
        {
            Notificador.ExibirMensagem("\nJá existe uma caixa com essa etiqueta!", ConsoleColor.Red);
            return;
        }

        RepositorioCaixa.CadastrarRegistro(novaCaixa);

        Notificador.ExibirMensagem("\nCaixa registrada com sucesso!", ConsoleColor.Green);
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
    public override void EditarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Editando Caixa...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EditarRegistro();
                return;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarRegistroPorId(idCaixaEscolhida);

        if (caixaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        Caixa dadosEditados = (Caixa)ObterDados();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        if (RepositorioCaixa.VerificarEtiquetasEditarRegistro(caixaEscolhida, dadosEditados))
        {
            Notificador.ExibirMensagem("\nJá existe uma caixa com essa etiqueta!", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
        }

        RepositorioCaixa.EditarRegistro(caixaEscolhida, dadosEditados);

        Notificador.ExibirMensagem("\nCaixa editada com sucesso!", ConsoleColor.Green);
    }
    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Excluindo Caixa...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                ExcluirRegistro();
                return;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarRegistroPorId(idCaixaEscolhida);

        if (caixaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            ExcluirRegistro();
            return;
        }

        if (caixaEscolhida.VerificarRevistasCaixa())
        {
            Notificador.ExibirMensagem($"\nA caixa {caixaEscolhida.Etiqueta} ainda possui revistas e não pode ser excluída.", ConsoleColor.Red);
            return;
        }

        RepositorioCaixa.ExcluirRegistro(caixaEscolhida);

        Notificador.ExibirMensagem("\nCaixa excluída com sucesso!", ConsoleColor.Green);
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
