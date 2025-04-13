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
        Console.Clear();
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("1 >> Registrar Revista");
        Console.WriteLine("2 >> Visualizar Lista de Revistas");
        Console.WriteLine("3 >> Editar Revista");
        Console.WriteLine("4 >> Excluir Revista");
        Console.WriteLine("S >> Voltar");

        Console.WriteLine();
        Console.Write("Opção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Revistas");
        Console.WriteLine("--------------------------------------------");
    }
    public void RegistrarRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("\nRegistrando Revista...");
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

        RepositorioRevista.RegistrarRevista(novaRevista);
        Console.WriteLine("Revista registrada com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Revistas...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -35} | {2, -20} | {3, -20} | {4, -20} | {5, -35}",
                "Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");
        else
            Console.WriteLine(
                "{0, -35} | {1, -20} | {2, -20} | {3, -35} | {4, -20}",
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
                    "{0, -6} | {1, -35} | {2, -20} | {3, -20} | {4, -20} | {5, -35}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa, r.StatusEmprestimo);
            else
                Console.WriteLine(
                    "{0, -35} | {1, -20} | {2, -20} | {3, -35} | {4, -20}",
                    r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa, r.StatusEmprestimo);
        }
        if (quantidadeRevistas == 0)
        {
            Console.WriteLine("\nNenhuma revista registrado!");
            RepositorioRevista.ListaVazia = true;
        }
    }
    public void EditarRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("\nEditando Revista...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Revista: ");
        int idRevistaEscolhida = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

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
        if (RepositorioRevista.VerificarRegistros(dadosEditados))
        {
            Console.WriteLine("Já existe uma revista com esse Título!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarRevista();
        }

        RepositorioRevista.EditarRevista(revistaEscolhida, dadosEditados);

        Console.WriteLine();
        Console.WriteLine("Revista editada com sucesso!");
    }
    public void ExcluirRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("\nExcluindo Revista...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioRevista.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Revista: ");
        int idRevistaEscolhida = Convert.ToInt32(Console.ReadLine());


        Console.WriteLine();
        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (RepositorioRevista.VerificarRevistaEmprestada(revistaEscolhida))
        {
            Console.WriteLine($"A revista {revistaEscolhida.Titulo} ainda está com um amigo!");
            return;
        }

        RepositorioRevista.ExcluirRevista(revistaEscolhida);

        Console.WriteLine();
        Console.WriteLine("Revista excluída com sucesso!");
    }
    public void MostrarListaCaixas(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Caixas...");
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
        Console.Write("Digite o título da revista: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Digite o N° de edição da revista: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine()!);

        Console.Write("Digite o número de telefone do Responsável: ");
        string anoPublicacao = Console.ReadLine()!;

        MostrarListaCaixas(true, true);

        if (RepositorioCaixa.ListaVazia)
            return null!;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Caixa para guardar a revista: ");
        int idCaixaEscolhida = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaEscolhida);

        return revista;
    }
}
