using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloRevista;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloReserva;

public class TelaReserva : TelaBase<Reserva>, ITelaCrud
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioEmprestimo RepositorioEmprestimo;
    public RepositorioReserva RepositorioReserva;
    public RepositorioRevista RepositorioRevista;

    public TelaReserva(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, RepositorioReserva repositorioReserva, RepositorioRevista repositorioRevista) : base("Reserva", repositorioReserva)
    {
        RepositorioAmigo = repositorioAmigo;
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioReserva = repositorioReserva;
        RepositorioRevista = repositorioRevista;
    }
    public override string ApresentarMenu()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("1 >> Registrar Reserva");
        ColorirEscrita.ComQuebraLinha("2 >> Cancelar Reserva");
        ColorirEscrita.ComQuebraLinha("3 >> Visualizar Reservas Ativas");
        ColorirEscrita.ComQuebraLinha("4 >> Pegar Revista Emprestada");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public override void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Reservas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Amigo", "Revista Reservada", "Data de Reserva", "Situação"];
            int[] espacamentos = [6, 20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Amigo", "Revista Reservada", "Data de Reserva", "Situação"];
            int[] espacamentos = [20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        List<Reserva> registros = RepositorioReserva.PegarListaRegistrados();

        int quantidadeReservas = 0;

        foreach (var r in registros)
        {
            quantidadeReservas++;
            RepositorioReserva.ListaVazia = false;

            if (comId)
            {
                string[] cabecalho = [r.Id.ToString(), r.Amigo.Nome, r.Revista.Titulo, r.DataReserva.ToShortDateString(), r.Status];
                int[] espacamentos = [6, 20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [r.Amigo.Nome, r.Revista.Titulo, r.DataReserva.ToShortDateString(), r.Status];
                int[] espacamentos = [20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }

        }

        if (quantidadeReservas == 0)
        {
            Notificador.ExibirMensagem("\nNenhuma reserva registrada!", ConsoleColor.Red);
            RepositorioReserva.ListaVazia = true;
        }
    }
    public void EmprestarRevistaReservada()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Emprestando Revista Reservada...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioReserva.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Reserva: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para tentar novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EmprestarRevistaReservada();
                return;
            }
        } while (!idValido);

        Reserva reservaEscolhida = RepositorioReserva.SelecionarRegistroPorId(idRevistaEscolhida);

        if (reservaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nReserva não encontrada!", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para tentar novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EmprestarRevistaReservada();
            return;
        }

        if (reservaEscolhida.Amigo.VerificarEmprestimos())
        {
            Notificador.ExibirMensagem("\nEsse amigo já tem um empréstimo em aberto!", ConsoleColor.Red);
            return;
        }

        if (RepositorioReserva.VerificarReservaAtiva(reservaEscolhida))
        {
            Notificador.ExibirMensagem($"\nA reserva escolhida não está 'Ativa'!", ConsoleColor.Red);
            return;
        }

        reservaEscolhida.Concluir();
        RepositorioEmprestimo.CadastrarRegistro(new Emprestimo(reservaEscolhida.Amigo, reservaEscolhida.Revista));

        Notificador.ExibirMensagem("\nRevista reservada emprestada com sucesso!", ConsoleColor.Green);
    }
    public void MostrarListaRevistas(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Revistas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status"];
            int[] espacamentos = [3, 24, 14, 18, 20, 18];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status"];
            int[] espacamentos = [24, 14, 18, 20, 18];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        List<Revista> registros = RepositorioRevista.PegarListaRegistrados();

        int quantidadeRevistas = 0;

        foreach (var r in registros)
        {
            quantidadeRevistas++;
            RepositorioRevista.ListaVazia = false;

            if (comId)
            {
                string[] cabecalho = [r.Id.ToString(), r.Titulo, r.NumeroEdicao.ToString(), r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo];
                int[] espacamentos = [3, 24, 14, 18, 20, 18];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, (ConsoleColor)r.Caixa.Cor, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [r.Titulo, r.NumeroEdicao.ToString(), r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo];
                int[] espacamentos = [24, 14, 18, 20, 18];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, (ConsoleColor)r.Caixa.Cor, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
        }

        if (quantidadeRevistas == 0)
        {
            Notificador.ExibirMensagem("\nNenhuma revista registrada!", ConsoleColor.Red);
            RepositorioRevista.ListaVazia = true;
        }
    }
    public void MostrarListaAmigos(bool exibirCabecalho, bool comId)
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

        List<Amigo> registros = RepositorioAmigo.PegarListaRegistrados();

        int quantidadeAmigos = 0;

        foreach (var a in registros)
        {
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
    public void CancelarReserva()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Cancelando Reserva...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioReserva.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Reserva: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                CancelarReserva();
                return;
            }
        } while (!idValido);

        Reserva reservaEscolhida = RepositorioReserva.SelecionarRegistroPorId(idRevistaEscolhida);

        if (reservaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            CancelarReserva();
            return;
        }

        if (RepositorioReserva.VerificarReservaAtiva(reservaEscolhida))
        {
            Notificador.ExibirMensagem($"\nA reserva escolhida não está 'Ativa'!", ConsoleColor.Red);
            return;
        }

        RepositorioReserva.ExcluirRegistro(reservaEscolhida);

        Notificador.ExibirMensagem("\nReserva cancelada com sucesso!", ConsoleColor.Green);
    }
    public override Reserva ObterDados()
    {
        MostrarListaAmigos(true, true);

        if (RepositorioAmigo.ListaVazia)
            return null!;

        bool idAmigoValido;
        int idAmigoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idAmigoValido)
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idAmigoValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarRegistroPorId(idAmigoEscolhido);

        MostrarListaRevistas(true, true);

        if (RepositorioRevista.ListaVazia)
            return null!;

        bool idRevistaValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Revista: ");
            idRevistaValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idRevistaValido)
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idRevistaValido);

        Revista revistaEscolhida = RepositorioRevista.SelecionarRegistroPorId(idRevistaEscolhida);

        Reserva reserva = new Reserva(amigoEscolhido, revistaEscolhida);

        return reserva;
    }
}
