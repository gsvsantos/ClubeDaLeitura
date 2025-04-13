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
        ExibirCabecalho();

        Console.WriteLine("1 >> Registrar Amigo");
        Console.WriteLine("2 >> Visualizar Lista de Amigos");
        Console.WriteLine("3 >> Visualizar Empréstimos de um Amigo");
        Console.WriteLine("4 >> Editar Amigo");
        Console.WriteLine("5 >> Excluir Amigo");
        Console.WriteLine("S >> Voltar");

        Console.Write("\nOpção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Amigos");
        Console.WriteLine("--------------------------------------------\n");
    }
    public void RegistrarAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Amigo...");
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

        if (RepositorioAmigo.VerificarTelefoneNovoRegistro(novoAmigo))
        {
            Console.WriteLine("\nJá existe um cadastro com esse número!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarAmigo();
            return;
        }

        RepositorioAmigo.RegistrarAmigo(novoAmigo);

        Console.WriteLine("\nAmigo registrado com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Amigos...");
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

        Console.WriteLine("Empréstimos Registrados...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                MostrarListaEmprestimos(true, false);
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        Emprestimo[] emprestimosAmigoEscolhido = amigoEscolhido.ObterEmprestimos();

        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine($"Visualizando Empréstimos de {amigoEscolhido.Nome}...");
        Console.WriteLine("--------------------------------------------\n");

        if (emprestimosAmigoEscolhido.All(e => e == null))
        {
            Console.WriteLine($"O {amigoEscolhido.Nome} ainda não fez nenhum empréstimo.");
            return;
        }

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -20}",
                "Id", "Revista", "Data de Devolução", "Situação");
        else
            Console.WriteLine(
                "{0, -30} | {1, -20} | {2, -20}",
                "Revista", "Data de Devolução", "Situação");

        foreach (Emprestimo e in emprestimosAmigoEscolhido)
        {
            if (e == null)
                continue;

            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -20} | {3, -20}",
                    e.Id, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
            else
                Console.WriteLine(
                    "{0, -30} | {1, -20} | {2, -20}",
                    e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
        }
    }
    public void EditarAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Amigo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarAmigo();
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarAmigo();
            return;
        }

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

        if (RepositorioAmigo.VerificarTelefoneEditarRegistro(amigoEscolhido, dadosEditados))
        {
            Console.WriteLine("\nJá existe um cadastro com esse número!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarAmigo();
            return;
        }

        RepositorioAmigo.EditarAmigo(amigoEscolhido, dadosEditados);

        Console.WriteLine("\nAmigo editado com sucesso!");
    }
    public void ExcluirAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Amigo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idAmigoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                ExcluirAmigo();
                return;
            }
        } while (!idValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            ExcluirAmigo();
            return;
        }

        if (RepositorioAmigo.VerificarEmprestimosAmigo(amigoEscolhido))
        {
            Console.WriteLine($"\nO {amigoEscolhido.Nome} ainda possui empréstimos em aberto e não pode ser excluído.");
            return;
        }

        RepositorioAmigo.ExcluirAmigo(amigoEscolhido);

        Console.WriteLine("\nAmigo excluído com sucesso!");
    }
    public Amigo ObterDadosAmigo()
    {
        Console.Write("Digite o Nome do Amigo: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o Nome do Responsável: ");
        string responsável = Console.ReadLine()!;

        Console.Write("Digite o Número de Telefone do Responsável: ");
        string telefone = Console.ReadLine()!;

        Amigo amigo = new Amigo(nome, responsável, telefone);

        return amigo;
    }
}
