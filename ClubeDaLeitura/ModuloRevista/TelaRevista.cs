using ClubeDaLeitura.ModuloCaixa;

namespace ClubeDaLeitura.ModuloRevista;

public class TelaRevista
{
    public RepositorioRevista RepositorioRevista;
    public RepositorioCaixa RepositorioCaixa;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
    {
        RepositorioRevista = repositorioRevista;
        RepositorioCaixa = repositorioCaixa;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 >> Registrar Revista");
        Console.WriteLine("2 >> Visualizar Lista de Revistas");
        Console.WriteLine("3 >> Editar Revista");
        Console.WriteLine("4 >> Excluir Revista");
        Console.WriteLine("S >> Voltar");

        Console.Write("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Revistas");
        Console.WriteLine("--------------------------------------------\n");
    }
    public void RegistrarRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Revista...");
        Console.WriteLine("--------------------------------------------\n");

        Revista novaRevista = ObterDadosRevista();

        if (novaRevista == null)
            return;

        string erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarRevista();
            return;
        }

        if (RepositorioRevista.VerificarTituloNovoRegistro(novaRevista))
        {
            Console.WriteLine("\nJá existe uma revista dessa edição!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarRevista();
            return;
        }

        RepositorioRevista.RegistrarRevista(novaRevista);

        Console.WriteLine("\nRevista registrada com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -15} | {3, -20} | {4, -20} | {5, -20}",
                "Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");
        else
            Console.WriteLine(
                "{0, -30} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
                "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");

        Revista[] revistasRegistradas = RepositorioRevista.PegarListaRegistrados();

        int quantidadeRevistas = 0;

        for (int i = 0; i < revistasRegistradas.Length; i++)
        {
            Revista r = revistasRegistradas[i];

            if (r == null)
                continue;

            quantidadeRevistas++;
            RepositorioRevista.ListaVazia = false;

            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -15} | {3, -20} | {4, -20} | {5, -20}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo);
            else
                Console.WriteLine(
                    "{0, -30} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
                    r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo);
        }

        if (quantidadeRevistas == 0)
        {
            Console.WriteLine("\nNenhuma revista registrada!");
            RepositorioRevista.ListaVazia = true;
        }
    }
    public void EditarRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Revista...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarRevista();
                return;
            }
        } while (!idValido);

        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (revistaEscolhida == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        Revista dadosEditados = ObterDadosRevista();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        if (RepositorioRevista.VerificarTituloEditarRegistro(revistaEscolhida, dadosEditados))
        {
            Console.WriteLine("\nJá existe uma revista dessa edição!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        RepositorioRevista.EditarRevista(revistaEscolhida, dadosEditados);

        Console.WriteLine("\nRevista editada com sucesso!");
    }
    public void ExcluirRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Revista...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        bool idValido;
        int idRevistaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                ExcluirRevista();
                return;
            }
        } while (!idValido);

        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (revistaEscolhida == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            ExcluirRevista();
            return;
        }

        if (RepositorioRevista.VerificarRevistaEmprestada(revistaEscolhida))
        {
            Console.WriteLine($"\nA revista {revistaEscolhida.Titulo} ainda está com um amigo!");
            return;
        }

        RepositorioRevista.ExcluirRevista(revistaEscolhida);

        Console.WriteLine("\nRevista excluída com sucesso!");
    }
    public void MostrarListaCaixas(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Caixas...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                "Id", "Etiqueta", "Dias de Empréstimo");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20}",
                "Etiqueta", "Dias de Empréstimo");

        Caixa[] caixasRegistradas = RepositorioCaixa.PegarListaRegistrados();

        int quantidadeCaixas = 0;

        for (int i = 0; i < caixasRegistradas.Length; i++)
        {
            Caixa c = caixasRegistradas[i];

            if (c == null)
                continue;

            quantidadeCaixas++;
            RepositorioCaixa.ListaVazia = false;

            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -20}",
                    c.Id, c.Etiqueta, c.DiasEmprestimo);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20}",
                    c.Etiqueta, c.DiasEmprestimo);
        }

        if (quantidadeCaixas == 0)
        {
            Console.WriteLine("\nNenhuma caixa registrada!");
            RepositorioCaixa.ListaVazia = true;
        }
    }
    public Revista ObterDadosRevista()
    {
        Console.Write("Digite o Título da Revista: ");
        string titulo = Console.ReadLine()!;

        bool numeroValido;
        int numeroEdicao;

        do
        {
            Console.Write("Digite o N° de Edição da Revista: ");
            numeroValido = int.TryParse(Console.ReadLine(), out numeroEdicao);

            if (!numeroValido)
            {
                Console.WriteLine("\nEsse não é um número válido!");
                return null!;
            }
        } while (!numeroValido);

        Console.Write("Digite o Ano de Publicação da Revista: ");
        string anoPublicacao = Console.ReadLine()!;

        MostrarListaCaixas(true, true);

        if (RepositorioCaixa.ListaVazia)
            return null!;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa para guardar a revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                return null!;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaEscolhida);

        return revista;
    }
}
