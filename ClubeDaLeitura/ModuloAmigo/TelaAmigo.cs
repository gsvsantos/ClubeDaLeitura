using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloAmigo;

public class TelaAmigo
{
    public RepositorioAmigo RepositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        RepositorioAmigo = repositorioAmigo;
    }
    public string ApresentarMenu()
    {
        Console.Clear();
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("1 >> Registrar Amigo");
        Console.WriteLine("2 >> Visualizar Lista de Amigos");
        Console.WriteLine("3 >> Visualizar Emprestimos do Amigo");
        Console.WriteLine("4 >> Editar Amigo");
        Console.WriteLine("5 >> Excluir Amigo");
        Console.WriteLine("S >> Voltar");

        Console.WriteLine();
        Console.Write("Opção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Amigos");
        Console.WriteLine("--------------------------------------------");
    }
    public void RegistrarAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("\nRegistrando Amigo...");
        Console.WriteLine("--------------------------------------------\n");

        Amigo novoAmigo = ObterDadosAmigo();

        string erros = novoAmigo.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarAmigo();
            return;
        }

        if (RepositorioAmigo.VerificarNovoRegistro(novoAmigo))
        {
            Console.WriteLine("Já existe um cadastro com esses dados!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarAmigo();
        }

        RepositorioAmigo.RegistrarAmigo(novoAmigo);
        Console.WriteLine("Amigo registrado com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Amigos...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Nome", "Responsável", "Telefone");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20} | {2, -20}",
                "Nome", "Responsável", "Telefone");

        Amigo[] amigosRegistrados = RepositorioAmigo.PegarListaRegistrados();

        int quantidadeAmigos = 0;

        for (int i = 0; i < amigosRegistrados.Length; i++)
        {
            Amigo a = amigosRegistrados[i];

            if (a == null)
                continue;

            quantidadeAmigos++;
            RepositorioAmigo.ListaVazia = false;
            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                    a.Id, a.Nome, a.Responsavel, a.Telefone);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20} | {2, -20}",
                    a.Nome, a.Responsavel, a.Telefone);
        }
        if (quantidadeAmigos == 0)
        {
            Console.WriteLine("\nNenhum amigo registrado!");
            RepositorioAmigo.ListaVazia = true;
        }
    }
    public void MostrarListaEmprestimos(bool exibirCabecalho, bool comId)
    {
        ExibirCabecalho();

        Console.WriteLine("\nEmprestimos Registrados...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigoEscolhido = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        Emprestimo[] emprestimosAmigoEscolhido = amigoEscolhido.ObterEmprestimos();

        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine();
        Console.WriteLine($"Visualizando Emprestimos de {amigoEscolhido.Nome}...");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                "Id", "Status", "Revista Emprestada");
        else
            Console.WriteLine(
                "{0, -20} | {1, -35} |",
                "Status", "Revista Emprestada");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                emprestimosAmigoEscolhido.Id, emprestimosAmigoEscolhido.Situacao, emprestimosAmigoEscolhido.Revista.Titulo);
        else
            Console.WriteLine(
                "{0, -20} | {1, -35}",
                emprestimosAmigoEscolhido.Situacao, emprestimosAmigoEscolhido.Revista.Titulo);

    }
    public void EditarAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("\nEditando Amigo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigoEscolhido = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        Amigo dadosEditados = ObterDadosAmigo();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarAmigo();
            return;
        }

        RepositorioAmigo.EditarAmigo(amigoEscolhido, dadosEditados);

        Console.WriteLine();
        Console.WriteLine("Amigo editado com sucesso!");
    }
    public void ExcluirAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("\nExcluindo Amigo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigoEscolhido = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (RepositorioAmigo.VerificarEmprestimosAmigo(amigoEscolhido))
        {
            Console.WriteLine($"O {amigoEscolhido.Nome} ainda tem emprestimos em aberto!");
            return;
        }

        RepositorioAmigo.ExcluirAmigo(amigoEscolhido);

        Console.WriteLine();
        Console.WriteLine("Amigo excluído com sucesso!");
    }
    public Amigo ObterDadosAmigo()
    {
        Console.Write("Digite o nome do Amigo: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o nome do Responsável: ");
        string responsável = Console.ReadLine()!;

        Console.Write("Digite o número de telefone do Responsável: ");
        string telefone = Console.ReadLine()!;

        Amigo amigo = new Amigo(nome, responsável, telefone);

        return amigo;
    }
}
