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
        ExibirCabecalho();

        Console.WriteLine("1 >> Registrar Empréstimo");
        Console.WriteLine("2 >> Visualizar Lista de Empréstimos");
        Console.WriteLine("3 >> Editar Empréstimo");
        Console.WriteLine("4 >> Excluir Empréstimo");
        Console.WriteLine("5 >> Registrar Devolução");
        Console.WriteLine("S >> Voltar");

        Console.Write("\nOpção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Empréstimos");
        Console.WriteLine("--------------------------------------------\n");
    }
    public void RegistrarEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Empréstimo...");
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

        Console.WriteLine("\nEmpréstimo registrado com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Empréstimos...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -20} | {4, -20}",
                "Id", "Amigo", "Revista", "Data de Devolução", "Situação");
        else
            Console.WriteLine(
                "{0, -20} | {1, -30} | {2, -20} | {3, -20}",
                "Amigo", "Revista", "Data de Devolução", "Situação");

        Emprestimo[] emprestimosRegistrados = RepositorioEmprestimo.PegarListaRegistrados();

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(emprestimosRegistrados);

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
                    "{0, -6} | {1, -20} | {2, -30} | {3, -20} | {4, -20}",
                    e.Id, e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -30} | {2, -20} | {3, -20}",
                    e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
        }

        if (quantidadeEmprestimos == 0)
        {
            Console.WriteLine("\nNenhum empréstimo registrado!");
            RepositorioEmprestimo.ListaVazia = true;
        }
    }
    public void EditarEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        Emprestimo dadosEditados = ObterDadosEmprestimo();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        if (RepositorioEmprestimo.VerificarDevolucao(dadosEditados))
        {
            Console.WriteLine("\nEsse empréstimo já foi concluído.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        RepositorioEmprestimo.EditarEmprestimo(emprestimoEscolhido, dadosEditados);

        Console.WriteLine("\nEmpréstimo editado com sucesso!");
    }
    public void ExcluirEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            ExcluirEmprestimo();
            return;
        }

        RepositorioEmprestimo.ExcluirEmprestimo(emprestimoEscolhido);

        Console.WriteLine("\nEmpréstimo excluído com sucesso!");
    }
    public void MostrarListaRevistas(bool exibirCabecalho, bool comId)
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
    public void MostrarListaAmigos(bool exibirCabecalho, bool comId)
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
    public void RegistrarDevolucao()
    {
        ExibirCabecalho();

        Console.WriteLine("Devolução Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                RegistrarDevolucao();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (RepositorioEmprestimo.VerificarDevolucao(emprestimoEscolhido))
        {
            Console.WriteLine("\nA devolução escolhida não está em aberto!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarDevolucao();
        }

        emprestimoEscolhido.RegistrarDevolucao();

        Console.WriteLine("\nDevolução feita com sucesso!");

    }
    public Emprestimo ObterDadosEmprestimo()
    {

        MostrarListaAmigos(true, true);

        if (RepositorioAmigo.ListaVazia)
            return null!;

        bool idAmigoValido;
        int idAmigoEscolhido;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idAmigoValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                return null!;
            }
        } while (!idAmigoValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (RepositorioEmprestimo.VerificarEmprestimoAtivo(amigoEscolhido))
        {
            Console.WriteLine("\nEsse amigo já tem um empréstimo em aberto!");
            return null!;
        }

        MostrarListaRevistas(true, true);

        if (RepositorioRevista.ListaVazia)
            return null!;

        bool idRevistaValido;
        int idRevistaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Revista: ");
            idRevistaValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idRevistaValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                return null!;
            }
        } while (!idRevistaValido);

        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (!RepositorioRevista.VerificarRevistaDisponivel(revistaEscolhida))
        {
            Console.WriteLine("\nEssa revista não está disponível!");
            return null!;
        }

        Emprestimo emprestimo = new Emprestimo(amigoEscolhido, revistaEscolhida, "Aberto");

        return emprestimo;
    }

}
