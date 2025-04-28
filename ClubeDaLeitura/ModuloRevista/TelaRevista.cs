using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloRevista;

public class TelaRevista : TelaBase<Revista>, ITelaCrud
{
    public IRepositorioCaixa RepositorioCaixa;
    public IRepositorioRevista RepositorioRevista;

    public TelaRevista(IRepositorioCaixa repositorioCaixa, IRepositorioRevista repositorioRevista) : base("Revista", repositorioRevista)
    {
        RepositorioCaixa = repositorioCaixa;
        RepositorioRevista = repositorioRevista;
    }
    public override bool TemRestricoesNoInserir(Revista novaRevista, out string mensagem)
    {
        mensagem = "";

        if (RepositorioRevista.VerificarTituloNovoRegistro(novaRevista))
        {
            mensagem = "\nJá existe uma revista dessa edição!";
            return true;
        }

        return false;
    }
    public override bool TemRestricoesNoEditar(Revista revistaEscolhida, Revista dadosEditados, out string mensagem)
    {
        Caixa caixaAntiga = revistaEscolhida.Caixa;
        Caixa caixaNova = dadosEditados.Caixa;

        mensagem = "";

        if (RepositorioRevista.VerificarTituloEditarRegistro(revistaEscolhida, dadosEditados))
        {
            mensagem = "\nJá existe uma revista dessa edição!";
            return true;
        }

        if (caixaAntiga != caixaNova)
        {
            caixaAntiga.RemoverRevista(revistaEscolhida);
            caixaNova.AdicionarRevista(revistaEscolhida);
        }

        return false;
    }
    public override bool TemRestricoesNoExcluir(Revista revistaEscolhida, out string mensagem)
    {
        mensagem = "";

        if (RepositorioRevista.VerificarRevistaEmprestada(revistaEscolhida))
        {
            mensagem = $"\nA revista {revistaEscolhida.Titulo} ainda está com um amigo!";
            return true;
        }

        if (RepositorioRevista.VerificarRevistaReservada(revistaEscolhida))
        {
            mensagem = $"\nA revista {revistaEscolhida.Titulo} está reservada!";
            return true;
        }

        revistaEscolhida.Caixa.RemoverRevista(revistaEscolhida);
        return false;
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
            int[] espacamentos = [3, 22, 14, 18, 20, 18];
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
                int[] espacamentos = [3, 22, 14, 18, 20, 18];
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
    public override Revista ObterDados()
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

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarRegistroPorId(idCaixaEscolhida);

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
