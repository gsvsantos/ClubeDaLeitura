using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class TelaEmprestimo
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioRevista RepositorioRevista;
    public RepositorioEmprestimo RepositorioEmprestimo;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista)
    {
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioAmigo = repositorioAmigo;
        RepositorioRevista = repositorioRevista;
    }
    public string ApresentarMenu()
    {
        Console.Clear();
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("1 >> Registrar Empréstimo");
        Console.WriteLine("2 >> Visualizar Lista de Empréstimos");
        Console.WriteLine("3 >> Editar Empréstimo");
        Console.WriteLine("4 >> Excluir Empréstimo");
        Console.WriteLine("S >> Voltar");

        Console.WriteLine();
        Console.Write("Opção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Empréstimos");
        Console.WriteLine("--------------------------------------------");
    }
    public void RegistrarEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("\nRegistrando Empréstimo...");
        Console.WriteLine("--------------------------------------------\n");

        Emprestimo novoEmprestimo = ObterDadosEmprestimo();

        if (novoEmprestimo == null)
            return;

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarEmprestimo();
            return;
        }

        RepositorioEmprestimo.RegistrarEmprestimo(novoEmprestimo);
        Console.WriteLine("Empréstimo registrado com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Empréstimos...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -35} | {3, -20} | {4, -20}",
                "Id", "Amigo", "Revista", "Data de Devolução", "Situação");
        else
            Console.WriteLine(
                "{0, -20} | {1, -35} | {2, -20} | {3, -20}",
                "Amigo", "Revista", "Data de Devolução", "Situação");

        Emprestimo[] emprestimosRegistrados = RepositorioEmprestimo.PegarListaRegistrados();

        int quantidadeEmprestimos = 0;

        for (int i = 0; i < emprestimosRegistrados.Length; i++)
        {
            Emprestimo e = emprestimosRegistrados[i];

            if (e == null)
                continue;

            quantidadeEmprestimos++;
            RepositorioEmprestimo.ListaVazia = false;
            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -35} | {3, -20} | {4, -20}",
                    e.Id, e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -35} | {2, -20} | {3, -20}",
                    e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
        }
        if (quantidadeEmprestimos == 0)
        {
            Console.WriteLine("\nNenhum empréstimo registrado!");
            RepositorioEmprestimo.ListaVazia = true;
        }
    }
    public void ExcluirEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("\nExcluindo Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Emprestimo: ");
        int idEmprestimoEscolhido = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        RepositorioEmprestimo.ExcluirEmprestimo(emprestimoEscolhido);

        Console.WriteLine();
        Console.WriteLine("Empréstimo excluído com sucesso!");
    }
    public void MostrarListaRevistas(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Revistas...");
        Console.WriteLine("--------------------------------------------\n");
        /// FALTA TERMINAR
    }
    public void MostrarListaAmigos(bool exibirCabecalho, bool comId)
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
    public void RegistrarDevolucao()
    {
        ExibirCabecalho();

        Console.WriteLine("\nDevolução Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Empréstimo: ");
        int idEmprestimoEscolhido = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);


        if (RepositorioEmprestimo.VerificarDevolucao(emprestimoEscolhido))
        {
            Console.WriteLine("A devolução escolhida não esta em aberto!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarDevolucao();
        }

        emprestimoEscolhido.RegistrarDevolucao();

        Console.WriteLine();
        Console.WriteLine("Devolução feita com sucesso!");

    }
    public Emprestimo ObterDadosEmprestimo()
    {

        MostrarListaAmigos(true, true);

        if (RepositorioAmigo.ListaVazia)
            return null!;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigoEscolhido = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (RepositorioEmprestimo.VerificarEmprestimoAtivo(amigoEscolhido))
        {
            Console.WriteLine("Esse amigo já tem um empréstimo em aberto!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarEmprestimo();
        }

        MostrarListaRevistas(true, true);

        if (RepositorioRevista.ListaVazia)
            return null!;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Revista: ");
        int idRevistaEscolhida = Convert.ToInt32(Console.ReadLine());

        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (RepositorioRevista.VerificarRevistaDisponivel(revistaEscolhida))
        {
            Console.WriteLine("Essa revista não está disponível!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarEmprestimo();
        }

        Emprestimo emprestimo = new Emprestimo(amigoEscolhido, revistaEscolhida, "Aberto");
        return emprestimo;
    }
}
