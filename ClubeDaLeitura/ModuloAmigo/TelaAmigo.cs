using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloAmigo;

public class TelaAmigo : TelaBase
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioEmprestimo RepositorioEmprestimo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo) : base("Amigo", repositorioAmigo)
    {
        RepositorioAmigo = repositorioAmigo;
        RepositorioEmprestimo = repositorioEmprestimo;
    }
    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("1 >> Registrar Amigo");
        ColorirEscrita.ComQuebraLinha("2 >> Visualizar Lista de Amigos");
        ColorirEscrita.ComQuebraLinha("3 >> Editar Amigo");
        ColorirEscrita.ComQuebraLinha("4 >> Excluir Amigo");
        ColorirEscrita.ComQuebraLinha("5 >> Visualizar Empréstimos de um Amigo");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Registrando Amigo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        Amigo novoAmigo = (Amigo)ObterDados();

        string erros = novoAmigo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            CadastrarRegistro();
            return;
        }

        if (RepositorioAmigo.VerificarTelefoneNovoRegistro(novoAmigo))
        {
            Notificador.ExibirMensagem("\nJá existe um cadastro com esse número!", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            CadastrarRegistro();
            return;
        }

        RepositorioAmigo.CadastrarRegistro(novoAmigo);

        Notificador.ExibirMensagem("\nAmigo registrado com sucesso!", ConsoleColor.Green);
    }
    public override void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Amigos...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Nome", "Responsável", "Telefone"];
            int[] espacamentos = [6, 20, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Nome", "Responsável", "Telefone"];
            int[] espacamentos = [20, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        EntidadeBase[] registros = RepositorioAmigo.PegarListaRegistrados();
        Amigo[] amigosRegistrados = new Amigo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
        {
            amigosRegistrados[i] = (Amigo)registros[i];
        }

        int quantidadeAmigos = 0;

        for (int i = 0; i < amigosRegistrados.Length; i++)
        {
            Amigo a = amigosRegistrados[i];

            if (a == null)
                continue;

            quantidadeAmigos++;
            RepositorioAmigo.ListaVazia = false;

            if (comId)
            {
                string[] linha = [a.Id.ToString(), a.Nome, a.Responsavel, a.Telefone];
                int[] espacamentos = [6, 20, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
            else
            {
                string[] linha = [a.Nome, a.Responsavel, a.Telefone];
                int[] espacamentos = [20, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
        }

        if (quantidadeAmigos == 0)
        {
            Notificador.ExibirMensagem("\nNenhum amigo registrado!", ConsoleColor.Red);
            RepositorioAmigo.ListaVazia = true;
        }
    }
    public void MostrarListaEmprestimos(bool exibirCabecalho, bool comId)
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Empréstimos Registrados...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                MostrarListaEmprestimos(true, false);
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = (Amigo)RepositorioAmigo.SelecionarRegistroPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            MostrarListaEmprestimos(true, false);
            return;
        }

        Emprestimo[] emprestimosAmigoEscolhido = amigoEscolhido.ObterEmprestimos();

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(emprestimosAmigoEscolhido);

        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"Visualizando Empréstimos de {amigoEscolhido.Nome}...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (emprestimosAmigoEscolhido.All(e => e == null))
        {
            Notificador.ExibirMensagem($"O {amigoEscolhido.Nome} ainda não fez nenhum empréstimo.", ConsoleColor.Red);
            return;
        }

        if (comId)
        {
            string[] cabecalho = ["Id", "Revista", "Data de Devolução", "Situação"];
            int[] espacamentos = [6, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Revista", "Data de Devolução", "Situação"];
            int[] espacamentos = [30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        foreach (Emprestimo e in emprestimosAmigoEscolhido)
        {
            if (e == null)
                continue;

            if (comId)
            {
                string[] linha = [e.Id.ToString(), e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao];
                int[] espacamentos = [6, 30, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
            else
            {
                string[] linha = [e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao];
                int[] espacamentos = [30, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
        }
    }
    public override void EditarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Editando Amigo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EditarRegistro();
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = (Amigo)RepositorioAmigo.SelecionarRegistroPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        Amigo dadosEditados = (Amigo)ObterDados();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            EditarRegistro();
            return;
        }

        if (RepositorioAmigo.VerificarTelefoneEditarRegistro(amigoEscolhido, dadosEditados))
        {
            Notificador.ExibirMensagem("\nJá existe um cadastro com esse número!", ConsoleColor.Red);
            return;
        }

        RepositorioAmigo.EditarRegistro(amigoEscolhido, dadosEditados);

        Notificador.ExibirMensagem("\nAmigo editado com sucesso!", ConsoleColor.Green);
    }
    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Excluindo Amigo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                ExcluirRegistro();
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = (Amigo)RepositorioAmigo.SelecionarRegistroPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            ExcluirRegistro();
            return;
        }

        if (RepositorioAmigo.VerificarEmprestimosAmigo(amigoEscolhido))
        {
            Notificador.ExibirMensagem($"\nO {amigoEscolhido.Nome} ainda possui empréstimos em aberto e não pode ser excluído.", ConsoleColor.Red);
            return;
        }

        if (amigoEscolhido.Reserva != null)
        {
            Notificador.ExibirMensagem($"\nO {amigoEscolhido.Nome} ainda possui uma reservas ativa e não pode ser excluído.", ConsoleColor.Red);
            return;
        }

        if (amigoEscolhido.Multas.Any(m => m != null && m.Status != "Quitada"))
        {
            Notificador.ExibirMensagem($"\nO {amigoEscolhido.Nome} ainda possui multas pendentes e não pode ser excluído.", ConsoleColor.Red);
            return;
        }

        RepositorioAmigo.ExcluirRegistro(amigoEscolhido);

        Notificador.ExibirMensagem("\nAmigo excluído com sucesso!", ConsoleColor.Green);
    }
    public override EntidadeBase ObterDados()
    {
        ColorirEscrita.SemQuebraLinha("Digite o Nome do Amigo: ");
        string nome = Console.ReadLine()!;

        ColorirEscrita.SemQuebraLinha("Digite o Nome do Responsável: ");
        string responsável = Console.ReadLine()!;

        ColorirEscrita.SemQuebraLinha("Digite o Número de Telefone do Responsável: ");
        string telefone = Console.ReadLine()!;

        Amigo amigo = new Amigo(nome, responsável, telefone);

        return amigo;
    }
}
