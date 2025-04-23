using System.Collections;
using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloRevista;

public class TelaRevista : TelaBase
{
    public RepositorioCaixa RepositorioCaixa;
    public RepositorioRevista RepositorioRevista;

    public TelaRevista(RepositorioCaixa repositorioCaixa, RepositorioRevista repositorioRevista) : base("Revista", repositorioRevista)
    {
        RepositorioCaixa = repositorioCaixa;
        RepositorioRevista = repositorioRevista;
    }
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Registrando Revista...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        Revista novaRevista = (Revista)ObterDados();

        if (novaRevista == null)
            return;

        string erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        if (RepositorioRevista.VerificarTituloNovoRegistro(novaRevista))
        {
            Notificador.ExibirMensagem("\nJá existe uma revista dessa edição!", ConsoleColor.Red);
            return;
        }

        RepositorioRevista.CadastrarRegistro(novaRevista);

        Notificador.ExibirMensagem("\nRevista registrada com sucesso!", ConsoleColor.Green);
    }
    public override void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
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

        ArrayList registros = RepositorioRevista.PegarListaRegistrados();

        int quantidadeRevistas = 0;

        foreach (Revista r in registros)
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
    public override void EditarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Editando Revista...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EditarRegistro();
                return;
            }
        } while (!idValido);

        Revista revistaEscolhida = (Revista)RepositorioRevista.SelecionarRegistroPorId(idRevistaEscolhida);

        if (revistaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        Caixa caixaAntiga = revistaEscolhida.Caixa;

        Revista dadosEditados = (Revista)ObterDados();

        if (dadosEditados == null)
            return;

        Caixa caixaNova = dadosEditados.Caixa;

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        if (RepositorioRevista.VerificarTituloEditarRegistro(revistaEscolhida, dadosEditados))
        {
            Notificador.ExibirMensagem("\nJá existe uma revista dessa edição!", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        if (caixaAntiga != caixaNova)
        {
            caixaAntiga.RemoverRevista(revistaEscolhida);
            caixaNova.AdicionarRevista(revistaEscolhida);
        }

        RepositorioRevista.EditarRegistro(revistaEscolhida, dadosEditados);

        Notificador.ExibirMensagem("\nRevista editada com sucesso!", ConsoleColor.Green);
    }
    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Excluindo Revista...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                ExcluirRegistro();
                return;
            }
        } while (!idValido);

        Revista revistaEscolhida = (Revista)RepositorioRevista.SelecionarRegistroPorId(idRevistaEscolhida);

        if (revistaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            ExcluirRegistro();
            return;
        }

        if (RepositorioRevista.VerificarRevistaEmprestada(revistaEscolhida))
        {
            Notificador.ExibirMensagem($"\nA revista {revistaEscolhida.Titulo} ainda está com um amigo!", ConsoleColor.Red);
            return;
        }
        if (RepositorioRevista.VerificarRevistaReservada(revistaEscolhida))
        {
            Notificador.ExibirMensagem($"\nA revista {revistaEscolhida.Titulo} está reservada!", ConsoleColor.Red);
            return;
        }

        RepositorioRevista.ExcluirRegistro(revistaEscolhida);

        Notificador.ExibirMensagem("\nRevista excluída com sucesso!", ConsoleColor.Green);
    }
    public void MostrarListaCaixas(bool exibirCabecalho, bool comId)
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

        ArrayList registros = RepositorioCaixa.PegarListaRegistrados();

        int quantidadeCaixas = 0;

        foreach (Caixa c in registros)
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
    public override EntidadeBase ObterDados()
    {
        MostrarListaCaixas(true, true);

        if (RepositorioCaixa.ListaVazia)
            return null!;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Caixa para guardar a revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idValido);

        Caixa caixaEscolhida = (Caixa)RepositorioCaixa.SelecionarRegistroPorId(idCaixaEscolhida);

        ColorirEscrita.SemQuebraLinha("\nDigite o Título da Revista: ");
        string titulo = Console.ReadLine()!;

        bool numeroValido;
        int numeroEdicao;

        do
        {
            ColorirEscrita.SemQuebraLinha("Digite o N° de Edição da Revista: ");
            numeroValido = int.TryParse(Console.ReadLine(), out numeroEdicao);

            if (!numeroValido)
                Notificador.ExibirMensagem("\nEsse não é um número válido!", ConsoleColor.Red);
        } while (!numeroValido);

        ColorirEscrita.SemQuebraLinha("Digite o Ano de Publicação da Revista: ");
        string anoPublicacao = Console.ReadLine()!;

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaEscolhida);

        return revista;
    }
}
